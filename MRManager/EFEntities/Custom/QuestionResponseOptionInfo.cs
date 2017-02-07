using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IQuestionResponseOptionInfo))]
    public class QuestionResponseOptionInfo : EntityView<IQuestions>, IQuestionResponseOptionInfo
    {
        public string Category { get; set; }
        public string Question { get; set; }
        public string Interview { get; set; }
        public int InterviewId { get; set; }
        public IList<IResponseOptionInfo> ResponseOptions { get; set; }
        public IList<IResponseOptionInfo> PatientResponses { get; set; }
    }
}