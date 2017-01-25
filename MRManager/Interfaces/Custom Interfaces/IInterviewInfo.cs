using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface IInterviewInfo :IEntityView<IInterviews>
    {
        String Interview { get; }
        String Category { get; }
        String Phase { get; }
    }
}