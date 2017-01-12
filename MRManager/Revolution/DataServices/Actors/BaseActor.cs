﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using Akka.Persistence;
using Common;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using Utilities;

namespace DataServices.Actors
{
    public class BaseActor<T>: ReceivePersistentActor, IAgent, IProcessSource
    {
        public ISystemSource Source => new Source(Guid.NewGuid(), "PersistentActor" + typeof(T).GetFriendlyName(),new SourceType(typeof(BaseActor<T>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public ImmutableList<IProcessSystemMessage> OutMessages = ImmutableList<IProcessSystemMessage>.Empty;

        internal void PublishProcesError(IProcessSystemMessage msg, Exception ex, Type expectedMessageType)
        {
            var outMsg = new ProcessEventFailure(failedEventType: msg.GetType(),
                failedEventMessage: msg,
                expectedEventType: expectedMessageType,
                exception: ex,
                source: Source, processInfo: new StateEventInfo(msg.Process.Id, RevolutionData.Context.Process.Events.Error));
            
            EventMessageBus.Current.Publish(outMsg, Source);
            EventMessageBus.Current.Publish(new RequestProcessLog(new StateCommandInfo(msg.Process.Id, RevolutionData.Context.Process.Commands.CreateLog), msg.Process,Source), Source);
            OutMessages = OutMessages.Add(outMsg);
        }

        internal void Publish(IProcessSystemMessage msg)
        {
           
            EventMessageBus.Current.Publish(msg, Source);
            OutMessages = OutMessages.Add(msg);
        }
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