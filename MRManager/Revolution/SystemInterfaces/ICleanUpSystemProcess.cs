using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface ICleanUpSystemProcess : IProcessSystemMessage
    {
        int ProcessToBeCleanedUpId { get; }
    }
}