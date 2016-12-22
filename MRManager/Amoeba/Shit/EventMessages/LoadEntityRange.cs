using System.Collections.Generic;
using System.ComponentModel;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class LoadEntityRange<T> : BaseMessage where T : IEntity
    {
        public int StartIndex { get;  }
        public int Count { get;  }
        public string FilterExpression { get; }
        public Dictionary<string, string> NavExp { get;  }
        public IEnumerable<string> IncludesLst { get;  }

        public object SortDescriptions { get; }

        
        public LoadEntityRange(int startIndex, int count, string filterExpression, Dictionary<string, string> navExp, IEnumerable<string> includesLst, MessageSource source) : base(source)
        {
            StartIndex = startIndex;
            Count = count;
            FilterExpression = filterExpression;
            NavExp = navExp;
            IncludesLst = includesLst;
            
        }

        public LoadEntityRange(int startIndex, int count, SortDescriptionCollection sortDescriptions, MessageSource source):base(source)
        {
            StartIndex = startIndex;
            Count = count;
            SortDescriptions = sortDescriptions;
            
        }

        
    }
}
