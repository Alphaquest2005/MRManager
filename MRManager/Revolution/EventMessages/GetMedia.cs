using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    
    public class GetMedia : SystemProcessMessage
    {
        public List<int> MediaIdList { get; }
        
        public GetMedia(List<int> mediaIdList, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaIdList = mediaIdList;
        }

    }
}
