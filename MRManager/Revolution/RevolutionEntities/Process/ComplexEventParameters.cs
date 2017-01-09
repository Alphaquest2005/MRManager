using System.Collections.Generic;
using SystemInterfaces;
using Actor.Interfaces;

namespace RevolutionEntities.Process
{
    public class ComplexEventParameters : IComplexEventParameters
    {
        public ComplexEventParameters(IComplexEventService actor, Dictionary<string, IProcessSystemMessage> messages)
        {
            Actor = actor;
            Messages = messages;
            
        }

        public IComplexEventService Actor { get; }
        public Dictionary<string, IProcessSystemMessage> Messages { get; }
     
    }
}