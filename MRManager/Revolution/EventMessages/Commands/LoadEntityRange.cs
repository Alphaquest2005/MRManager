using System.Collections.Generic;
using System.ComponentModel;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
   

    public class LoadEntityRange<T> : ProcessSystemMessage where T : IEntity
    {
        public int StartIndex { get;  }
        public int Count { get;  }
        public string FilterExpression { get; }
        public Dictionary<string, string> NavExp { get;  }
        public IEnumerable<string> IncludesLst { get;  }

        public object SortDescriptions { get; }

        
        public LoadEntityRange(int startIndex, int count, string filterExpression, Dictionary<string, string> navExp, IEnumerable<string> includesLst, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            StartIndex = startIndex;
            Count = count;
            FilterExpression = filterExpression;
            NavExp = navExp;
            IncludesLst = includesLst;
            
        }

        public LoadEntityRange(int startIndex, int count, SortDescriptionCollection sortDescriptions, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            StartIndex = startIndex;
            Count = count;
            SortDescriptions = sortDescriptions;
            
        }

        
    }
}
