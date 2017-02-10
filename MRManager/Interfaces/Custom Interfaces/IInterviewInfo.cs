using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IInterviewInfo :IEntityView<IInterviews>
    {
        String Interview { get; }
        String Category { get; }
        String Phase { get; }
        int PhaseId { get; }
        int CategoryId { get; }

        string System { get; }
        int SystemId { get; }
        
    }
}