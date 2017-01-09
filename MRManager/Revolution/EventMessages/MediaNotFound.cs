using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    
    public class MediaNotFound : ProcessSystemMessage
    {
        public MediaNotFound(List<int> mediaIdList, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            MediaIdList = mediaIdList;
        }

        public List<int> MediaIdList { get; }

    }
}
