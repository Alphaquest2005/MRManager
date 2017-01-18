using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntityViewRequest<out TEntityView>: IEntityViewRequest where TEntityView: IEntityView
    {
    }


    [InheritedExport]
    public interface IEntityViewRequest:IProcessSystemMessage
    {
    }
}