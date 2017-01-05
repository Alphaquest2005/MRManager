using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntity:IEntityId
    {
        RowState RowState { get; set; }

        dynamic ComputedProperties { get; }
    }
}