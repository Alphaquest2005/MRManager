﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Persistence;
using Common;
using CommonMessages;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class BaseActor<T>: ReceivePersistentActor, IAgent, IProcessSource
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), "PersistentActor" + typeof(T).GetFriendlyName(), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        //internal Func<IList<IProcessExpectedEvent>, IList<IProcessSystemMessage>, bool> CheckExpectedEvents {
        //    get; } = (expectedEvents, eventSource) =>
        //{
        //    if (!expectedEvents.Any()) return false;
        //    //ToDo: Convert to Visitor to keep immutability
        //    foreach (var expectedEvent in expectedEvents)
        //    {
        //        var events = eventSource.Where(x => x.GetType() == expectedEvent.EventType).ToList();
        //        if (!events.Any()) return false;
        //        if (events.Any(x => expectedEvent.Validate(x) != true)) return false;
        //    }
        //    return true;
        //};


        public override string PersistenceId
        {
            get
            {
                var path = Context.Self.Path.ToStringWithUid();
                var res =  path.Substring(path.LastIndexOf("#") + 1);
                return "Actor-" + typeof (T).GetFriendlyName() + "-" + res;
            }
        }
        protected override void OnPersistRejected(Exception cause, object @event, long sequenceNr)
        {
            base.OnPersistRejected(cause, @event, sequenceNr);
            Debugger.Break();
        }

        protected override void OnPersistFailure(Exception cause, object @event, long sequenceNr)
        {
            base.OnPersistFailure(cause, @event, sequenceNr);
            Debugger.Break();
        }

        public string UserId => this.Source.SourceName;
        
    }

  
}