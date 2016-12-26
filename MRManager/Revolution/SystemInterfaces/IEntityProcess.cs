using DataInterfaces;

namespace SystemInterfaces
{
    public interface IEntityProcess<out TEntity>:IProcess where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}