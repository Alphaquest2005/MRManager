using System.Collections.Generic;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class HeaderInfoLoaded<T> : BaseMessage where T : IEntity
    {
        public IList<IHeaderInfo<T>> HeaderInfo { get; }

        public HeaderInfoLoaded(IList<IHeaderInfo<T>> headerInfo, MessageSource source) : base(source)
        {
            HeaderInfo = headerInfo;
        }
    }
}
