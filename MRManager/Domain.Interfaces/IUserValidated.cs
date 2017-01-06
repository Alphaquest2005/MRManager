using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Interfaces;

namespace Domain.Interfaces
{
    public interface IUserValidated : IProcessSystemMessage
    {
        ISignInInfo UserSignIn { get; }
    }
}
