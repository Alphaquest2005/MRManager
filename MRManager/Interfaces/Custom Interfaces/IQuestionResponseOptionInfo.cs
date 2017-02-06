using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IQuestionResponseOptionInfo : IEntityView<IQuestions>
    {
        string Category { get;  }
        string Question { get; }
        string Interview { get; }
        int InterviewId { get; }
        IList<IResponseOptionInfo> ResponseOptions { get;  }
        IList<IResponseOptionInfo> PatientResponses { get; }
    }
}