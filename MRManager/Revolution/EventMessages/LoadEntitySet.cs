using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySet<T> : SystemProcessMessage where T : IEntity
    {
        public LoadEntitySet(ISystemProcess process, MessageSource source) : base(process, source)
        {
            
        }
    }
}
