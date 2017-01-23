using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface ICleanUpSystemProcess : IProcessSystemMessage
    {
        int ProcessToBeCleanedUpId { get; }
    }
}