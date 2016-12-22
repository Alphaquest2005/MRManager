using System.ComponentModel.Composition;

namespace DataInterfaces
{
    [InheritedExport]
    public interface IEntity
    {
        int Id { get; }
        
    }
}
