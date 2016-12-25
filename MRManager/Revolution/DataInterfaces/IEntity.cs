using System.ComponentModel.Composition;

namespace DataInterfaces
{
    public enum RowState
    {
        Loaded, Added, Modified, Deleted,
        Unchanged
    }

    [InheritedExport]
    public interface IEntity 
    {
        int Id { get; }
        RowState RowState { get; set; }

        dynamic ComputedProperties { get; }
    }
}
