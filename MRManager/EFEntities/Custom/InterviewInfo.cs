using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IInterviewInfo))]
    public partial class InterviewInfo : EntityView<IInterviews>, IInterviewInfo
    {
        
        public string Interview { get; set; }
        public string Category { get; set; }
        public string Phase { get; set; }
    }


}