using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class HeaderInfoLoaded<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<IHeaderInfo<T>> HeaderInfo { get; }

        public HeaderInfoLoaded(IList<IHeaderInfo<T>> headerInfo, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            HeaderInfo = headerInfo;
        }
    }
}
