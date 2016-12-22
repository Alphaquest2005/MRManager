using System.Collections.Generic;
using CommonMessages;

namespace EventMessages
{
    
    public class MediaNotFound : BaseMessage 
    {
        public MediaNotFound(List<int> mediaIdList,MessageSource source) :base(source)
        {
            MediaIdList = mediaIdList;
        }

        public List<int> MediaIdList { get; }

    }
}
