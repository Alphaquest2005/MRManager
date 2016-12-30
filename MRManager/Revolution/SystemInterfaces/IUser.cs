using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IUser
    {
       string UserId { get; }
    }

  
}
