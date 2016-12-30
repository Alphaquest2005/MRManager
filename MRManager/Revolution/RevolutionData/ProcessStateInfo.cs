using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData
{
    public static class ProcessStateInfo
    {
        public static IProcessStateDetailedInfo WaitingOnUserName { get; } = new ProcessStateDetailedInfo("Waiting for User Name", "Please Enter your User Name. If this is your First Time Login In please Contact the Receptionist for your user info.");
        //public static ProcessStateDetailedInfo<EntityFound<IUserSignIn>> UserFound { get; } = new ProcessStateDetailedInfo<EntityFound<UserSignIn>>(msg, Func<EntityFound<UserSignIn>,string>, "Please Enter your User Name. If this is your First Time Login In please Contact the Receptionist for your user info.");

    }
}
