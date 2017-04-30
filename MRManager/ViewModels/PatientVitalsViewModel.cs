using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{
    [Export(typeof(IPatientVitalsViewModel))]
    public partial class PatientVitalsViewModel : DynamicViewModel<ObservableViewModel<IPatientVitalsInfo>>, IPatientVitalsViewModel
    {
       [ImportingConstructor]
        public PatientVitalsViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableViewModel<IPatientVitalsInfo>(viewInfo, eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            this.WireEvents();
        }

        
       public ReactiveProperty<IProcessState<IPatientVitalsInfo>> State => this.ViewModel.State;
        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public IPatientInfo CurrentPatient { get; set; }


        
    }

    //public partial class PatientVitalsViewModel : IPatientVitalsInfo
    //{
    //    private int _temperature;

    //    public int Temperature
    //    {
    //        get { return _temperature; }
    //        set { _temperature = value; OnPropertyChanged(); }
    //    }

    //    private int _pulse;

    //    public int Pulse
    //    {
    //        get { return _pulse; }
    //        set { _pulse = value; OnPropertyChanged(); }
    //    }

    //    private int _respiratory;

    //    public int Respiratory
    //    {
    //        get { return _respiratory; }
    //        set { _respiratory = value; OnPropertyChanged(); }
    //    }

    //    private string _bloodPressure;

    //    public string BloodPressure
    //    {
    //        get { return _bloodPressure; }
    //        set { _bloodPressure = value; OnPropertyChanged(); }
    //    }

    //    private string _saO2;

    //    public string SaO2
    //    {
    //        get { return _saO2; }
    //        set { _saO2 = value; OnPropertyChanged(); }
    //    }

    //    public int Id { get; set; }
    //    public DateTime EntryDateTime { get; }
    //}
}