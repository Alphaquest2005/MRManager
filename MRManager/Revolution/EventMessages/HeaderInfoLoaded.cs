using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class HeaderInfoLoaded<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<IHeaderInfo<T>> HeaderInfo { get; }

        public HeaderInfoLoaded(IList<IHeaderInfo<T>> headerInfo, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            HeaderInfo = headerInfo;
        }
    }
}
