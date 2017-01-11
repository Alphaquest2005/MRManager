using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class Entity
    {
        public class Events
        {
            public static IStateEvent EntityCreated => new StateEvent("EntityCreated","Entity Created","", Commands.CreateEntity);
            public static IStateEvent EntityUpdated => new StateEvent("EntityUpdated", "Entity Updated", "", Commands.CreateEntity);
            public static IStateEvent EntityDeleted => new StateEvent("EntityDeleted", "Entity Deleted", "", Commands.CreateEntity);
            public static IStateEvent EntityFound => new StateEvent("EntityFound", "Entity Found", "", Commands.CreateEntity);
            public static IStateEvent EntitySetLoaded => new StateEvent("EntitySetLoaded", "Entity Set Loaded", "", Commands.CreateEntity);
        }

        public class Commands
        {
            public static IStateCommand CreateEntity => new StateCommand("CreateEntity", "Create Entity",Events.EntityCreated);
            public static IStateCommand UpdateEntity => new StateCommand("UpdateEntity", "Update Entity", Events.EntityCreated);
            public static IStateCommand FindEntity => new StateCommand("FindEntity", "Find Entity", Events.EntityCreated);
            public static IStateCommand DeleteEntity => new StateCommand("DeleteEntity", "Delete Entity", Events.EntityCreated);
            public static IStateCommand LoadEntitySet => new StateCommand("LoadEntitySet", "Load Entity Set", Events.EntityCreated);
        }
    }
}