using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;
using Actor.Interfaces;

namespace EventMessages.Commands
{
   

    public class CreateProcessActor:ProcessSystemMessage, ICreateProcessActor
    {
        public CreateProcessActor(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo,process, source)
        {
           
        }
    }
}
