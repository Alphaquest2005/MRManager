using DataInterfaces;

namespace SystemInterfaces
{
    public interface ISystemEntityProcess<out TEntity> : IEntityProcess<TEntity>, ISystemProcess where TEntity : IEntity
    {
       
    }
}