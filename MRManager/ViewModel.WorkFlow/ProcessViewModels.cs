using System.Collections.Generic;
using Interfaces;
using RevolutionData;
using ViewModel.Interfaces;
using ViewModel.WorkFlow.ViewModelInfo;

namespace ViewModel.WorkFlow
{
    public class ProcessViewModels
    {
        public static readonly List<IViewModelInfo> ProcessViewModelInfos = new List<IViewModelInfo>
        {
            MainWindowViewModelInfo.MainWindowViewModel,
            ScreenViewModelInfo.ScreenViewModel,
            SigninViewModelInfo.SigninViewModel,
            HeaderViewModelInfo.HeaderViewModel,
            FooterViewModelInfo.FooterViewModel,
            PatientSummaryListViewModelInfo.PatientSummaryListViewModel,
            PatientDetailsViewModelInfo.PatientDetailsViewModel,
            PatientVisitViewModelInfo.PatientVisitViewModel,
            PatientSyntomViewModelInfo.PatientSyntomViewModel,
            InterviewListViewModelInfo.InterviewListViewModel,
            QuestionListViewModelInfo.QuestionListViewModel,
            QuestionaireViewModelInfo.QuestionairenaireViewViewModel,
            
            EntityCacheViewModelInfo<ISyntomPriority>.CacheViewModel(3),
            EntityCacheViewModelInfo<ISyntomStatus>.CacheViewModel(3),
            EntityCacheViewModelInfo<ISyntoms>.CacheViewModel(3),
            EntityCacheViewModelInfo<IVisitType>.CacheViewModel(3),
            EntityCacheViewModelInfo<IPhase>.CacheViewModel(3),
            EntityCacheViewModelInfo<IMedicalCategory>.CacheViewModel(3),
            EntityCacheViewModelInfo<IMedicalSystems>.CacheViewModel(3),
            EntityCacheViewModelInfo<IQuestionResponseTypes>.CacheViewModel(3),
            EntityCacheViewModelInfo<ISex>.CacheViewModel(3),

            EntityViewCacheViewModelInfo<IDoctorInfo>.CacheViewModel(3),
            EntityViewCacheViewModelInfo<ISyntomMedicalSystemInfo>.CacheViewModel(3),
            

        };

        public static readonly List<IViewModelInfo> ProcessCache = new List<IViewModelInfo>
        {
            


        };
    }


}
