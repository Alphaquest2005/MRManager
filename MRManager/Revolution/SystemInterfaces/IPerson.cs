using DataInterfaces;

namespace SystemInterfaces
{
    public interface IPerson:IEntity
    {
        string Name { get;  }
    }
    public interface IAgent
    {
    }
}
