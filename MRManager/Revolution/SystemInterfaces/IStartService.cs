using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    public interface IStartService<TService> where TService:IService<TService>
    {
    }
}