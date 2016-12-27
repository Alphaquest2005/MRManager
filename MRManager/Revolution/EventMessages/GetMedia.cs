﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    
    public class GetMedia : ProcessSystemMessage
    {
        public List<int> MediaIdList { get; }
        
        public GetMedia(List<int> mediaIdList, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Contract.Requires(mediaIdList != null && mediaIdList.Any());
            MediaIdList = mediaIdList;
        }

    }
}
