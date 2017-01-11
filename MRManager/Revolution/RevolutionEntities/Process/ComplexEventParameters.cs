using System.Collections.Generic;
using SystemInterfaces;
using Actor.Interfaces;

namespace RevolutionEntities.Process
{
    public class ComplexEventParameters : IComplexEventParameters
    {
        public ComplexEventParameters(IComplexEventService actor, Dictionary<string, dynamic> messages)
        {
            Actor = actor;
            Messages = messages;
            
        }

        public IComplexEventService Actor { get; }
        public Dictionary<string, dynamic> Messages { get; }
     
    }
}