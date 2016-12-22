using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class ResultsLoaded<TResults> : SystemProcessMessage where TResults : class,IEntity 
    {
        public IList<TResults> Entities { get; }

        public ResultsLoaded(IList<TResults> entities, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Entities = entities;
        }
    }
}
