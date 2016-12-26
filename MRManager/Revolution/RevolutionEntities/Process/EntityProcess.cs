using SystemInterfaces;
using DataInterfaces;

namespace RevolutionEntities.Process
{
    public class EntityProcess<TEntity> : Process, IEntityProcess<TEntity> where TEntity : class, IEntity, new()
    {
        public EntityProcess(TEntity entity, int id, int parentProcessId, string name, string description, string symbol, IUser user) : base(id, parentProcessId, name, description, symbol, user)
        {
            Entity = entity;
        }

        public TEntity Entity { get; } 
    }
}