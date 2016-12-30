

namespace SystemInterfaces
{
    public interface IProcess<out TEntity>:IProcess where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}