using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    public class ResultsLoaded<TResults> : ProcessSystemMessage where TResults : class,IEntity 
    {
        public IList<TResults> Entities { get; }

        public ResultsLoaded(IList<TResults> entities, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entities = entities;
        }
    }
}
