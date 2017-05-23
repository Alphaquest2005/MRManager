﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-ViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using CommonMessages;
using JB.Collections.Reactive;
using RevolutionEntities.Process;
using ViewModel.Interfaces;


namespace ViewModels
{

    [Export(typeof(IScreenModel))]
    public partial class ScreenModel : BaseViewModel<ScreenModel>, IScreenModel
    {
        private dynamic _slider;

        [ImportingConstructor]
	    public ScreenModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(process,viewInfo, eventSubscriptions, eventPublications,commandInfo, orientation, priority)
        {
            this.WireEvents();
        }
    
	    
        public ObservableList<IViewModel> HeaderViewModels { get; } = new ObservableList<IViewModel>();
        public ObservableList<IViewModel> LeftViewModels { get; } = new ObservableList<IViewModel>();
        public ObservableList<IViewModel> RightViewModels { get; } = new ObservableList<IViewModel>();
        public ObservableList<IViewModel> BodyViewModels { get; } = new ObservableList<IViewModel>();
        public ObservableList<IViewModel> FooterViewModels { get; } = new ObservableList<IViewModel>();
        public ObservableList<IViewModel> CacheViewModels { get; } = new ObservableList<IViewModel>();

        public dynamic Slider
        {
            get { return _slider; }
            set
            {
                _slider = value;
                AppSlider.Slider = _slider;
            }
        }
    }

   


}
