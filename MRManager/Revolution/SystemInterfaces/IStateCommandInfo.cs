using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IStateCommandInfo : IProcessStateInfo
    {
        new IStateCommand State { get; }
       
    }
}