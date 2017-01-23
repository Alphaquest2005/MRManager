using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class ViewModel
    {
        public class Commands
        {
            public static IStateCommand CreateViewModel => new StateCommand("CreateViewModel","Create View Model", Events.ViewModelCreated);
            public static IStateCommand LoadViewModel => new StateCommand("LoadViewModel", "Load View Model", Events.ViewModelLoaded);
            public static IStateCommand UnloadViewModel => new StateCommand("UnloadViewModel", "Unload View Model", Events.ViewModelLoaded);
        }
        public class Events
        {
            public static IStateEvent ViewModelCreated => new StateEvent("ViewModelCreated","View Model Created", "");
            public static IStateEvent ViewModelLoaded => new StateEvent("ViewModelLoaded", "View Model Loaded", "");
        }
    }


}
