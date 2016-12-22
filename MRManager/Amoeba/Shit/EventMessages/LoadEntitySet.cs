using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntitySet<T> : BaseMessage where T : IEntity
    {
        public LoadEntitySet(MessageSource source) : base(source){}
    }
}
