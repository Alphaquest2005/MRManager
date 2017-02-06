using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    public interface IFailedMessageData : IProcessSystemMessage
    {
        dynamic Data { get; set; }
    }
}
