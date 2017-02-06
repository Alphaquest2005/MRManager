using System.ComponentModel.Composition;
using SystemInterfaces;

namespace SystemInterfaces
{
    
    public interface IService<TService>: IProcessSource
    {
    }
}