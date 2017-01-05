using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class Process : IProcess
    {
        public Process(int id,int parentProcessId, string name, string description, string symbol, IUser user)
        {
            Id = id;
            ParentProcessId = parentProcessId;
            Name = name;
            Description = description;
            Symbol = symbol;
            User = user;
        }

        public Process(IProcessInfo processInfo, IUser user)
        {
            Id = processInfo.Id;
            ParentProcessId = processInfo.ParentProcessId;
            Name = processInfo.Name;
            Description = processInfo.Description;
            Symbol = processInfo.Symbol;
            User = user;
           
        }

 

        public int Id { get; }
        public int ParentProcessId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public string Symbol { get; }
        public IUser User { get; }
        
    }
}
