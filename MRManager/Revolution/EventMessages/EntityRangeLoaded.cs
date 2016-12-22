using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityRangeLoaded<T> : SystemProcessMessage where T : IEntity
    {
        public int OverAllCount { get;  }

        public EntityRangeLoaded(IList<T> entities,int startIndex, int overAllCount, ISystemProcess process, MessageSource source) : base(process, source)
        {
            StartIndex = startIndex;
            OverAllCount = overAllCount;
            Entities = entities;
        }

        public IList<T> Entities { get; }
        public int StartIndex { get;  }
    }
}
