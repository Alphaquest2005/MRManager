using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

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

    public partial class ResponseOptionInfo : BaseEntity, IResponseOptionInfo
    {
        public int? ResponseId { get; set; }
        public int? PatientResponseId { get; set; }
        public string Value { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }

    public partial class ResponseImage : BaseEntity, IResponseImage
    {
        public int MediaId { get; set; }
        public int PatientResponseId { get; set; }
        public byte[] Media { get; set; }
    }
}