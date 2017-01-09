using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;
using Domain.Interfaces;
using Interfaces;

namespace DomainMessages
{
    public class UserValidated : ProcessSystemMessage, IUserValidated
    {
        public UserValidated(ISignInInfo userSignIn, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            UserSignIn = userSignIn;
        }

        public ISignInInfo UserSignIn { get; }
    }
}
