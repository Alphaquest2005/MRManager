using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SystemInterfaces;
using Actor.Interfaces;
using ViewModel.Interfaces;

namespace RevolutionEntities.Process
{
    public class ComplexEventAction: IComplexEventAction
    {
        public ComplexEventAction(string key, int processId, IList<IProcessExpectedEvent> events, Type expectedMessageType, IProcessAction action, IProcessStateInfo processInfo, ActionTrigger actionTrigger = ActionTrigger.All)//Todo:get rid of optional parameters
        {
            ProcessId = processId;
            Events = events;
            ExpectedMessageType = expectedMessageType;
            ProcessInfo = processInfo;
            ActionTrigger = actionTrigger;
            Key = key;
            Action = action;
        }

        public string Key { get; }
        public IList<IProcessExpectedEvent> Events { get; }
        public int ProcessId { get; }
        public Type ExpectedMessageType { get; }
        public IProcessStateInfo ProcessInfo { get; }
        public IProcessAction Action { get; }
        public ActionTrigger ActionTrigger { get; }
    }

    public static class ComplexEventActionExtensions
    {
      
       
    }
}