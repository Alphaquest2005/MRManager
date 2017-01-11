﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class EntityView
    {
        public class Commands
        {
            public static IStateCommand GetEntityView => new StateCommand("GetEntityView", "Get Entity View Item", new StateEvent("EntityViewFound", "Entity View Item Found", ""));
        }

        public class Events
        {
            public static IStateEvent EntityViewFound => new StateEvent("EntityViewFound", "Entity View Item Found", "", new StateCommand("GetEntityView", "Get Entity View Item"));
        }
    }
}
