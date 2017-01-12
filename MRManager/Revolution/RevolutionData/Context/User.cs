using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class User
    {
        public class Commands
        {
            public static IStateCommand FindUserName => new StateCommand("FindUserName", "Find User Name", Events.UserNameFound);
            public static IStateCommand FindUser => new StateCommand("FindUser", "Find User", Events.UserFound);
        }

        public class Events
        {
           public static IStateEvent UserNameFound => new StateEvent("UserNameFound", "User Name Found", "Just UserName found");
           public static IStateEvent UserFound => new StateEvent("UserFound", "User Name & Password Found", "Both UserName and PassWord"); 
        }
        
        
    }
}
