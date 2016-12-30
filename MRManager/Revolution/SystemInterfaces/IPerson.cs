
using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IPerson:IEntity
    {
        string Name { get;  }
    }
}
