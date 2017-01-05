using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class Process<TEntity> : Process, IProcess<TEntity> where TEntity : class, IEntity, new()
    {
        public Process(TEntity entity, int id, int parentProcessId, string name, string description, string symbol, IUser user) : base(id, parentProcessId, name, description, symbol, user)
        {
            Entity = entity;
        }

        public TEntity Entity { get; } 
    }
}