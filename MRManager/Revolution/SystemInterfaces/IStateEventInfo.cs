using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IStateEventInfo : IProcessStateInfo
    {
        new IStateEvent State { get; }
        
    }
}