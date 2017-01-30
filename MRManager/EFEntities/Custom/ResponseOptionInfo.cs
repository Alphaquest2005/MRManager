using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    public partial class ResponseOptionInfo : EntityView<IResponseOptions>, IResponseOptionInfo
    {
        private int? _responseId;
        private int? _patientResponseId;
        private string _value;
        private int _responseNumber;
        private string _type;
        private string _description;
        private int _questionId;

        public int? ResponseId
        {
            get { return _responseId; }
            set { this.RaiseAndSetIfChanged(ref _responseId,value); }
        }

        public int? PatientResponseId
        {
            get { return _patientResponseId; }
            set { this.RaiseAndSetIfChanged(ref _patientResponseId, value); }
        }

        public string Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public int QuestionId
        {
            get { return _questionId; }
            set { this.RaiseAndSetIfChanged(ref _questionId, value); }
        }

        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }

        public string Type
        {
            get { return _type; }
            set { this.RaiseAndSetIfChanged(ref _type, value); }
        }

        public int ResponseNumber
        {
            get { return _responseNumber; }
            set { this.RaiseAndSetIfChanged(ref _responseNumber,value); }
        }
    }
}