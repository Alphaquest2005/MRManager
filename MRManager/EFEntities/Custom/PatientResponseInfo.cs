using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    public partial class PatientResponseInfo : EntityView<IPatientResponses>, IPatientResponseInfo
    {
        public int PatientVisitId { get; set; }
        public int QuestionId { get; set; }
        public int InterviewId { get; set; }
        public int PatientSyntomId { get; set; }
        public string Question { get; set; }
        public string Interview { get; set; }
        public string Category { get; set; }
        public IList<IResponseOptionInfo> ResponseOptions { get; set; }
        
        public IList<IResponseImage> ResponseImages { get; set; }
        public int PatientId { get; set; }
    }

    public partial class ResponseOptionInfo : EntityView<IResponseOptions>, IResponseOptionInfo
    {
        private int? _responseId;
        private int? _patientResponseId;
        private string _value;

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

        public int QuestionId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}