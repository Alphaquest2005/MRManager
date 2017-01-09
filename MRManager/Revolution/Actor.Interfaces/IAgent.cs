using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IAgent : IUser
    {
       
    }
}