using System;
using CommonMessages;

namespace EventMessages
{
    public class ProcessEventFailure: SystemProcessMessage
    {
        public Type FailedEventType { get; set; }
        public SystemProcessMessage FailedEventMessage { get; set; }
        public Type ExpectedEventType { get; set; }
        public Exception Exception { get; set; }
        public MessageSource MsgSource { get; set; }

        public ProcessEventFailure(Type failedEventType, SystemProcessMessage failedEventMessage, Type expectedEventType,Exception exception,  MessageSource msgSource):base( failedEventMessage.Process,msgSource)
        {
            FailedEventType = failedEventType;
            FailedEventMessage = failedEventMessage;
            ExpectedEventType = expectedEventType;
            Exception = exception;
            MsgSource = msgSource;
        }
    }
}
