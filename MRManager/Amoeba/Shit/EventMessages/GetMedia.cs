using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CommonMessages;

namespace EventMessages
{
    
    public class GetMedia : BaseMessage 
    {
        public List<int> MediaIdList { get; }
        
        public GetMedia(List<int> mediaIdList,MessageSource source) : base(source)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaIdList = mediaIdList;
        }

    }
}
