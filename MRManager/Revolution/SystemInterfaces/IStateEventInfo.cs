using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IStateEventInfo : IProcessStateInfo
    {
        new IStateEvent State { get; }
        
    }
}