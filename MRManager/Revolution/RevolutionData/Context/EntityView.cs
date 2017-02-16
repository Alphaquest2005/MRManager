using System;
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
            public static IStateCommand GetEntityView => new StateCommand("GetEntityView", "Get Entity View Item", Events.EntityViewFound );
            public static IStateCommand LoadEntityViewSetWithChanges = new StateCommand("LoadEntityViewSetWithChanges", "Load EntityView Set with Changes", Events.EntityViewSetLoaded );
        }

        public class Events
        {
            public static IStateEvent EntityViewFound => new StateEvent("EntityViewFound", "Entity View Item Found", "");

            public static IStateEvent EntityViewSetLoaded => new StateEvent("EntityViewSetLoaded", "Entity ViewSet Loaded", "");
            public static IStateEvent EntityViewUpdated => new StateEvent("EntityViewUpdated", "Entity View Updated", "");
        }
    }
}
