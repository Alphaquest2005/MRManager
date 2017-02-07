using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IInterviewInfo))]
    public partial class InterviewInfo : EntityView<IInterviews>, IInterviewInfo
    {
        public string Interview { get; set; }
        public string Category { get; set; }
        public string Phase { get; set; }
        public int PhaseId { get; set; }
        public int CategoryId { get; set; }
        public string System { get; set; }
        public int SystemId { get; set; }
    }


}