using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public interface IDoctorInfo : IEntityView<IPersons_Doctor>
    {
        string Name { get; }
        string Code { get; }
    }
}