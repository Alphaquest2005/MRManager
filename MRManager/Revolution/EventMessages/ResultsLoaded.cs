using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class ResultsLoaded<TResults> : ProcessSystemMessage where TResults : class,IEntity 
    {
        public IList<TResults> Entities { get; }

        public ResultsLoaded(IList<TResults> entities, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Entities = entities;
        }
    }
}
