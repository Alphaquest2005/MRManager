using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    public class QuestionResponseOptionInfo : EntityView<IQuestions>, IQuestionResponseOptionInfo
    {
        private string _category;
        private string _question;
        private string _interview;
        private int _interviewId;
        private IList<IResponseOptionInfo> _responseOptions;
        private IList<IResponseOptionInfo> _patientResponses;

        public string Category
        {
            get { return _category; }
            set { this.RaiseAndSetIfChanged(ref _category, value); }
        }

        public string Question
        {
            get { return _question; }
            set { this.RaiseAndSetIfChanged(ref _question, value); }
        }

        public string Interview
        {
            get { return _interview; }
            set { this.RaiseAndSetIfChanged(ref _interview, value); }
        }

        public int InterviewId
        {
            get { return _interviewId; }
            set { this.RaiseAndSetIfChanged(ref _interviewId, value); }
        }

        public IList<IResponseOptionInfo> ResponseOptions
        {
            get { return _responseOptions; }
            set { this.RaiseAndSetIfChanged(ref _responseOptions, value); }
        }

        public IList<IResponseOptionInfo> PatientResponses
        {
            get { return _patientResponses; }
            set { this.RaiseAndSetIfChanged(ref _patientResponses, value); }
        }
    }
}