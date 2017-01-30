using SystemInterfaces;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public partial class InterviewInfo : EntityView<IInterviews>, IInterviewInfo
    {
        public string Interview { get; set; }
        public string Category { get; set; }
        public string Phase { get; set; }
    }


}