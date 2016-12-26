using System;
using CommonMessages;

namespace EventMessages
{
    public class ProcessEventFailure: ProcessSystemMessage
    {
        public Type FailedEventType { get; set; }
        public ProcessSystemMessage FailedEventMessage { get; set; }
        public Type ExpectedEventType { get; set; }
        public Exception Exception { get; set; }
        public MessageSource MsgSource { get; set; }

        public ProcessEventFailure(Type failedEventType, ProcessSystemMessage failedEventMessage, Type expectedEventType,Exception exception,  ISystemMessage msg):base( failedEventMessage.Process,msg)
        {
            FailedEventType = failedEventType;
            FailedEventMessage = failedEventMessage;
            ExpectedEventType = expectedEventType;
            Exception = exception;
            
        }
    }
}
