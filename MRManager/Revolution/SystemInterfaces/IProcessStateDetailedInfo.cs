using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessStateInfo
    {
        ISystemProcess Process { get; }
        IState State { get; }
    }
}