using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
   

    public class LoadEntitySet<TEntity> : ProcessSystemMessage, ILoadEntitySet<TEntity> where TEntity : IEntity
    {
        public LoadEntitySet(ISystemProcess process, ISystemSource source) : base(process, source)
        {
            
        }
    }
}
