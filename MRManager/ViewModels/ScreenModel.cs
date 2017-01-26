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

    [Export]
    public partial class ScreenModel : BaseViewModel<ScreenModel>, IScreenModel
    {
	    public ScreenModel(ISystemProcess process, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation) : base(process, eventSubscriptions, eventPublications,commandInfo, orientation)
        {
            this.WireEvents();
        }
    
	    
        public ObservableCollection<IViewModel> HeaderViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> LeftViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> RightViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> BodyViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> FooterViewModels { get; } = new ObservableCollection<IViewModel>();

        public dynamic Slider { get; set; }





    }

   


}
