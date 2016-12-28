using System;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class ProcessEventFailure: ProcessSystemMessage
    {
        public Type FailedEventType { get; set; }
        public IProcessSystemMessage FailedEventMessage { get; set; }
        public Type ExpectedEventType { get; set; }
        public Exception Exception { get; set; }
        public MessageSource MsgSource { get; set; }

        public ProcessEventFailure(Type failedEventType, IProcessSystemMessage failedEventMessage, Type expectedEventType,Exception exception,  ISourceMessage SourceMsg):base( failedEventMessage.Process,SourceMsg)
        {
            FailedEventType = failedEventType;
            //TODO: need to implement serialization
            //FailedEventMessage = failedEventMessage;
            ExpectedEventType = expectedEventType;
            Exception = exception;
            
        }
    }

    public class ProcessStateMessage<TEntity> : ProcessSystemMessage where TEntity : IEntity
    {
        public ProcessStateMessage(IProcessState<TEntity> state, ISystemProcess process, ISourceMessage sourceMsg, int processId) : base(process, sourceMsg)
        {
            State = state;
        }

        public IProcessState<TEntity> State { get; }
    }
}
