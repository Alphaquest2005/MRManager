using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ISystemMessage : IMessage, ISystem
    {
    }
}
