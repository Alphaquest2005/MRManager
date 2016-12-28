using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySet<T> : ProcessSystemMessage where T : IEntity
    {
        public LoadEntitySet(ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            
        }
    }
}
