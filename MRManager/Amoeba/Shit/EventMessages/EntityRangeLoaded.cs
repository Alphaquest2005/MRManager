using System.Collections.Generic;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityRangeLoaded<T> : BaseMessage where T : IEntity
    {
        public int OverAllCount { get;  }

        public EntityRangeLoaded(IList<T> entities,int startIndex, int overAllCount,MessageSource source) : base(source)
        {
            StartIndex = startIndex;
            OverAllCount = overAllCount;
            Entities = entities;
        }

        public IList<T> Entities { get; }
        public int StartIndex { get;  }
    }
}
