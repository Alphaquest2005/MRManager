using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SystemInterfaces;
using CommonMessages;
using Interfaces;


namespace EventMessages
{
    
    public class GotMedia : ProcessSystemMessage
    {
        public List<IMedia> MediaList { get; }
        
        public GotMedia(List<IMedia> mediaIdList, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaList = mediaIdList;
        }

    }
}
