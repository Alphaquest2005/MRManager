using SystemInterfaces;
using DataInterfaces;

namespace CommonMessages
{
    public interface ISystemEntityProcessMessage<out TEntity> where TEntity : IEntity
    {
        ISystemEntityProcess<TEntity> Process { get; }
    }
}