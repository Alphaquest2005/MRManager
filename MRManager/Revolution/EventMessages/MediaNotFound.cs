using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    
    public class MediaNotFound : ProcessSystemMessage
    {
        public MediaNotFound(List<int> mediaIdList, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            MediaIdList = mediaIdList;
        }

        public List<int> MediaIdList { get; }

    }
}
