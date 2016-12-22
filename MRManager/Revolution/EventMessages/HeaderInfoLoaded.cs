using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class HeaderInfoLoaded<T> : SystemProcessMessage where T : IEntity
    {
        public IList<IHeaderInfo<T>> HeaderInfo { get; }

        public HeaderInfoLoaded(IList<IHeaderInfo<T>> headerInfo, ISystemProcess process, MessageSource source) : base(process, source)
        {
            HeaderInfo = headerInfo;
        }
    }
}
