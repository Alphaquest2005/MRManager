using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IProcessEventFailure))]
    public class ProcessEventFailure: ProcessSystemMessage, IProcessEventFailure
    {
        public ProcessEventFailure() { }
        public Type FailedEventType { get; set; }
        public IProcessSystemMessage FailedEventMessage { get; set; }
        public Type ExpectedEventType { get; set; }
        public Exception Exception { get; set; }
        

        public ProcessEventFailure(Type failedEventType, IProcessSystemMessage failedEventMessage, Type expectedEventType, Exception exception, IStateEventInfo processInfo, ISystemSource source):base( processInfo,failedEventMessage.Process,source)
        {
            FailedEventType = failedEventType;
            //TODO: need to implement serialization
            //FailedEventMessage = failedEventMessage;
            ExpectedEventType = expectedEventType;
            Exception = exception;
            
        }
    }
}
