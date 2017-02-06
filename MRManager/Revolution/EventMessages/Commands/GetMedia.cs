using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
   

    public class GetMedia : ProcessSystemMessage
    {
        public List<int> MediaIdList { get; }
        
        public GetMedia(List<int> mediaIdList, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaIdList = mediaIdList;
        }

    }
}
