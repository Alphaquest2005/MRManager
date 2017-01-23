using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SystemInterfaces;
using CommonMessages;
using Interfaces;

namespace EventMessages.Events
{
    
    public class GotMedia : ProcessSystemMessage
    {
        public List<IMedia> MediaList { get; }
        
        public GotMedia(List<IMedia> mediaIdList, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaList = mediaIdList;
        }

    }
}
