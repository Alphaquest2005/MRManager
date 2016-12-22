using System;
using System.Diagnostics;
using Akka.Persistence;
using CommonMessages;
using Utilities;

namespace DataServices.Actors
{
    public class BaseActor<T>: ReceivePersistentActor
    {
       
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


        internal MessageSource MsgSource => new MessageSource(this.ToString());


    }

}