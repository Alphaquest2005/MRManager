using SystemInterfaces;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    public partial class QuestionInfo : EntityView<IQuestions>, IQuestionInfo
    {
        private int _interviewId;
        private string _description;
        private int _entityAttributeId;
        private string _interview;
        private string _phase;
        private string _category;
        private string _entity;
        private string _attribute;
        private string _type;
        private int _questionNumber;

        public int InterviewId
        {
            get { return _interviewId; }
            set { this.RaiseAndSetIfChanged(ref _interviewId, value ) ; }
        }

        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }

        public int EntityAttributeId
        {
            get { return _entityAttributeId; }
            set { this.RaiseAndSetIfChanged(ref _entityAttributeId, value); }
        }

        public string Interview
        {
            get { return _interview; }
            set { this.RaiseAndSetIfChanged(ref _interview, value); }
        }

        public string Phase
        {
            get { return _phase; }
            set { this.RaiseAndSetIfChanged(ref _phase, value); }
        }

        public string Category
        {
            get { return _category; }
            set { this.RaiseAndSetIfChanged(ref _category, value); }
        }

        public string Entity
        {
            get { return _entity; }
            set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        public string Attribute
        {
            get { return _attribute; }
            set { this.RaiseAndSetIfChanged(ref _attribute, value); }
        }

        public string Type
        {
            get { return _type; }
            set { this.RaiseAndSetIfChanged(ref _type, value); }
        }

        public int QuestionNumber
        {
            get { return _questionNumber; }
            set { this.RaiseAndSetIfChanged(ref _questionNumber, value); }
        }
    }
}