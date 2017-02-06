using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPatientResponseInfo))]
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
}