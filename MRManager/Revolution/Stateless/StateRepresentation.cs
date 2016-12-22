﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class StateRepresentation
        {
            readonly TState _state;

            readonly IDictionary<TTrigger, ICollection<TriggerBehaviour>> _triggerBehaviours =
                new Dictionary<TTrigger, ICollection<TriggerBehaviour>>();

            internal IDictionary<TTrigger, ICollection<TriggerBehaviour>> TriggerBehaviours { get { return _triggerBehaviours; } }

            readonly ICollection<EntryActionBehavior> _entryActions = new List<EntryActionBehavior>();
            internal ICollection<EntryActionBehavior> EntryActions { get { return _entryActions; } }
            readonly ICollection<ExitActionBehavior> _exitActions = new List<ExitActionBehavior>();
            internal ICollection<ExitActionBehavior> ExitActions { get { return _exitActions; } }

            readonly ICollection<Action<Transition, object[]>> _internalActions = new List<Action<Transition, object[]>>();

            StateRepresentation _superstate; // null

            readonly ICollection<StateRepresentation> _substates = new List<StateRepresentation>();

            public StateRepresentation(TState state)
            {
                _state = state;
            }

            public bool CanHandle(TTrigger trigger)
            {
                TriggerBehaviour unused;
                return TryFindHandler(trigger, out unused);
            }

            public bool TryFindHandler(TTrigger trigger, out TriggerBehaviour handler)
            {
                return (TryFindLocalHandler(trigger, out handler, t => t.IsGuardConditionMet) ||
                    (Superstate != null && Superstate.TryFindHandler(trigger, out handler)));
            }
            
            bool TryFindLocalHandler(TTrigger trigger, out TriggerBehaviour handler, params Func<TriggerBehaviour, bool>[] filters)
            {
                ICollection<TriggerBehaviour> possible;
                if (!_triggerBehaviours.TryGetValue(trigger, out possible))
                {
                    handler = null;
                    return false;
                }

                var actual = filters.Aggregate(possible, (current, filter) => current.Where(filter).ToArray());

                if (actual.Count() > 1)
                    throw new InvalidOperationException(
                        string.Format(StateRepresentationResources.MultipleTransitionsPermitted,
                        trigger, _state));

                handler = actual.FirstOrDefault();
                return handler != null;
            }

            public bool TryFindHandlerWithUnmetGuardCondition(TTrigger trigger, out TriggerBehaviour handler)
            {
                return (TryFindLocalHandler(trigger, out handler, t => !t.IsGuardConditionMet) || 
                    (Superstate != null && Superstate.TryFindHandlerWithUnmetGuardCondition(trigger, out handler)));
            }

            public void AddEntryAction(TTrigger trigger, Action<Transition, object[]> action, string entryActionDescription)
            {
                Enforce.ArgumentNotNull(action, nameof(action));
                _entryActions.Add(
                    new EntryActionBehavior((t, args) =>
                    {
                        if (t.Trigger.Equals(trigger))
                            action(t, args);
                    },
                    Enforce.ArgumentNotNull(entryActionDescription, nameof(entryActionDescription))));
            }

            public void AddEntryAction(Action<Transition, object[]> action, string entryActionDescription)
            {
                _entryActions.Add(
                    new EntryActionBehavior(
                        Enforce.ArgumentNotNull(action, nameof(action)),
                        Enforce.ArgumentNotNull(entryActionDescription, nameof(entryActionDescription))));
            }

            public void AddExitAction(Action<Transition> action, string exitActionDescription)
            {
                _exitActions.Add(
                    new ExitActionBehavior(
                        Enforce.ArgumentNotNull(action, nameof(action)),
                        Enforce.ArgumentNotNull(exitActionDescription, nameof(exitActionDescription))));
            }
            public void AddInternalAction(Action<Transition, object[]> action)
            {
                _internalActions.Add(Enforce.ArgumentNotNull(action, "action"));
            }
            internal void AddInternalAction(TTrigger trigger, Action<Transition, object[]> action)
            {
                Enforce.ArgumentNotNull(action, "action");

                _internalActions.Add((t, args) =>
                {
                    if (t.Trigger.Equals(trigger))
                        action(t, args);
                });
            }
            public void Enter(Transition transition, params object[] entryArgs)
            {
                Enforce.ArgumentNotNull(transition, nameof(transition));

                if (transition.IsReentry)
                {
                    ExecuteEntryActions(transition, entryArgs);
                }
                else if (!Includes(transition.Source))
                {
                    if (_superstate != null)
                        _superstate.Enter(transition, entryArgs);

                    ExecuteEntryActions(transition, entryArgs);
                }
            }

            public void Exit(Transition transition)
            {
                Enforce.ArgumentNotNull(transition, nameof(transition));

                if (transition.IsReentry)
                {
                    ExecuteExitActions(transition);
                }
                else if (!Includes(transition.Destination))
                {
                    ExecuteExitActions(transition);
                    if (_superstate != null)
                        _superstate.Exit(transition);
                }
            }

            void ExecuteEntryActions(Transition transition, object[] entryArgs)
            {
                Enforce.ArgumentNotNull(transition, nameof(transition));
                Enforce.ArgumentNotNull(entryArgs, nameof(entryArgs));
                foreach (var action in _entryActions)
                    action.Action(transition, entryArgs);
            }

            void ExecuteExitActions(Transition transition)
            {
                Enforce.ArgumentNotNull(transition, nameof(transition));
                foreach (var action in _exitActions)
                    action.Action(transition);
            }
            void ExecuteInternalActions(Transition transition, object[] args)
            {
                var possibleActions = new List<Action<Transition, object[]>>();

                // Look for actions in superstate(s) recursivly until we hit the topmost superstate
                StateRepresentation aStateRep = this;
                do
                {
                    possibleActions.AddRange(aStateRep._internalActions);
                    aStateRep = aStateRep._superstate;
                } while (aStateRep != null);

                // Execute internal transition event handler
                foreach (var action in possibleActions)
                {
                    action(transition, args);
                }
            }
            public void AddTriggerBehaviour(TriggerBehaviour triggerBehaviour)
            {
                ICollection<TriggerBehaviour> allowed;
                if (!_triggerBehaviours.TryGetValue(triggerBehaviour.Trigger, out allowed))
                {
                    allowed = new List<TriggerBehaviour>();
                    _triggerBehaviours.Add(triggerBehaviour.Trigger, allowed);
                }
                allowed.Add(triggerBehaviour);
            }

            public StateRepresentation Superstate
            {
                get
                {
                    return _superstate;
                }
                set
                {
                    _superstate = value;
                }
            }

            public TState UnderlyingState
            {
                get
                {
                    return _state;
                }
            }

            public void AddSubstate(StateRepresentation substate)
            {
                Enforce.ArgumentNotNull(substate, nameof(substate));
                _substates.Add(substate);
            }

            public bool Includes(TState state)
            {
                return _state.Equals(state) || _substates.Any(s => s.Includes(state));
            }

            public bool IsIncludedIn(TState state)
            {
                return
                    _state.Equals(state) ||
                    (_superstate != null && _superstate.IsIncludedIn(state));
            }

            public IEnumerable<TTrigger> PermittedTriggers
            {
                get
                {
                    var result = _triggerBehaviours
                        .Where(t => t.Value.Any(a => a.IsGuardConditionMet))
                        .Select(t => t.Key);

                    if (Superstate != null)
                        result = result.Union(Superstate.PermittedTriggers);

                    return result.ToArray();
                }
            }
            internal void InternalAction(Transition transition, object[] args)
            {
                Enforce.ArgumentNotNull(transition, "transition");
                ExecuteInternalActions(transition, args);
            }
        }
    }
}
