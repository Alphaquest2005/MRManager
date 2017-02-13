using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IResponseOptionInfo))]
    public partial class ResponseOptionInfo : EntityView<IResponseOptions>, IResponseOptionInfo
    {
        public int? ResponseId { get; set; }
        public int? PatientResponseId { get; set; }
        public string Value { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int ResponseNumber { get; set; }
        public int PatientId { get; set; }
        public int PatientVisitId { get; set; }
        public int QuestionResponseTypeId { get; set; }
    }
}