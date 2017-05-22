using System;
using System.Collections.Generic;
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
        
        List<IQuestionResponseOptionInfo> Questions { get; set; }
    }
}