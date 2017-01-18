using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityRequest< out TEntity>:IEntityRequest where TEntity : IEntity
    {
    }

    public interface IEntityRequest:IProcessSystemMessage
    {
    }
}