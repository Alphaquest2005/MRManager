using System.Collections.Generic;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class ResultsLoaded<TResults> : BaseMessage where TResults : class,IEntity 
    {
        public IList<TResults> Entities { get; }

        public ResultsLoaded(IList<TResults> entities, MessageSource source) : base(source)
        {
            Entities = entities;
        }
    }
}
