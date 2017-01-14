using System.ComponentModel.Composition;
using SystemInterfaces;

namespace ViewModel.Interfaces
{
    [InheritedExport]
    public interface IViewEventPublicationParameter
    {
        object[] Params { get; }
        ISystemProcess Process { get; }
        ISource Source { get; }
        IStateEventInfo ProcessInfo { get; }
    }

    [InheritedExport]
    public interface IViewEventCommandParameter
    {
        object[] Params { get; }
        ISystemProcess Process { get; }
        ISource Source { get; }
        IStateCommandInfo ProcessInfo { get; }
    }
}