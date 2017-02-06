using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    
    public interface IViewEventPublicationParameter
    {
        object[] Params { get; }
        ISystemProcess Process { get; }
        ISystemSource Source { get; }
        IStateEventInfo ProcessInfo { get; }
    }

    
    public interface IViewEventCommandParameter
    {
        object[] Params { get; }
        ISystemProcess Process { get; }
        ISystemSource Source { get; }
        IStateCommandInfo ProcessInfo { get; }
    }
}