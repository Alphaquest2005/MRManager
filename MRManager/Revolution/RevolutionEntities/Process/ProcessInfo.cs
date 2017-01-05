using System;
using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class ProcessInfo : IProcessInfo
    {
        public ProcessInfo(int id, int parentProcessId, string name, string description, string symbol)
        {
            Id = id;
            ParentProcessId = parentProcessId;
            Name = name;
            Description = description;
            Symbol = symbol;
        }

        public int Id { get; }
        public int ParentProcessId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public string Symbol { get; }
      
    }

    public class ProcessInfo<TEntity> : IProcessInfo<TEntity> where TEntity : IEntity
    {
        public ProcessInfo(int id, int parentProcessId, string name, string description, string symbol)
        {
            Id = id;
            ParentProcessId = parentProcessId;
            Name = name;
            Description = description;
            Symbol = symbol;
        }

        public int Id { get; }
        public int ParentProcessId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public string Symbol { get; }
        public Type EntityType { get; } = typeof (TEntity);
    }
}