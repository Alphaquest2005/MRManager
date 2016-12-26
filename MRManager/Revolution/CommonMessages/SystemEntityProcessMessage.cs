using SystemInterfaces;
using DataInterfaces;

namespace CommonMessages
{
    public class EntityProcessSystemMessage<TEntity> : ProcessSystemMessage, ISystemEntityProcessMessage<TEntity> where TEntity : IEntity
    {
        public EntityProcessSystemMessage(ISystemEntityProcess<TEntity> process, IProcessSystemMessage msg) : base(process, msg)
        {
            Process = process;
        }

        public new ISystemEntityProcess<TEntity> Process { get; }
    }
}