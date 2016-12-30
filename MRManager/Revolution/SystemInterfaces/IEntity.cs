using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IEntity
    {
        int Id { get; set; }
        RowState RowState { get; set; }

        dynamic ComputedProperties { get; }
    }
}