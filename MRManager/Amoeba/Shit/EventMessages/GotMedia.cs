using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CommonMessages;
using Model.Interfaces.MREntitiesQS;

namespace EventMessages
{
    
    public class GotMedia : BaseMessage 
    {
        public List<IMedium> MediaList { get; }
        
        public GotMedia(List<IMedium> mediaIdList,MessageSource source) : base(source)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaList = mediaIdList;
        }

    }
}
