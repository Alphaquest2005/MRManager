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
        private int _categoryId;
        private int _phaseId;
        private string _phase;
        private string _category;
        private string _interview;

        public string Interview
        {
            get { return _interview; }
            set { this.RaiseAndSetIfChanged(ref _interview, value); }
        }

        public string Category
        {
            get { return _category; }
            set { this.RaiseAndSetIfChanged(ref _category, value); }
        }

        public string Phase
        {
            get { return _phase; }
            set { this.RaiseAndSetIfChanged(ref _phase, value); }
        }

        public int PhaseId
        {
            get { return _phaseId; }
            set { this.RaiseAndSetIfChanged(ref _phaseId, value); }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set { this.RaiseAndSetIfChanged(ref _categoryId, value); }
        }
    }


}