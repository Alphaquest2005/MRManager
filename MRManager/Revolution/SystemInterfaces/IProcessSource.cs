using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IProcessSource
    {
        ISystemSource Source { get; }
    }
}