using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Core.Common.UI;
using EF.Entities;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ViewModels
{
    [Export(typeof(IPatientDetailsViewModel))]
    public class PatientDetailsViewModel : DynamicViewModel<ObservableViewModel<IPatientDetailsInfo>>, IPatientDetailsViewModel
    {
        private IList<IPersonAddressInfo> _addresses;
        private IList<IPersonPhoneNumberInfo> _phoneNumbers;
        private IList<INextOfKinInfo> _nextOfKins;
        private INonResidentInfo _nonResidentInfo;

        [ImportingConstructor]
        public PatientDetailsViewModel(ISystemProcess process, IViewInfo viewInfo, List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublications, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, Type orientation, int priority) : base(new ObservableViewModel<IPatientDetailsInfo>(viewInfo, eventSubscriptions, eventPublications, commandInfo, process, orientation, priority))
        {
            this.WireEvents();
            
        }

        private object shit(IObservedChange<IPersonPhoneNumberInfo, string> observedChange)
        {
            throw new NotImplementedException();
        }


        public ReactiveProperty<IProcessState<IPatientDetailsInfo>> State => this.ViewModel.State;
        public ObservableDictionary<string, dynamic> ChangeTracking => this.ViewModel.ChangeTracking;

        public IPatientInfo CurrentPatient { get; set; }

        public IList<IPersonAddressInfo> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value; 
                OnPropertyChanged();
            }
        }

        public IList<IPersonPhoneNumberInfo> PhoneNumbers
        {
            get { return _phoneNumbers; }
            set { _phoneNumbers = value; OnPropertyChanged(); }
        }

        public IList<INextOfKinInfo> NextOfKins
        {
            get { return _nextOfKins; }
            set { _nextOfKins = value; OnPropertyChanged(); }
        }

        public INonResidentInfo NonResidentInfo
        {
            get { return _nonResidentInfo; }
            set { _nonResidentInfo = value; OnPropertyChanged(); }
        }

        private ReactiveProperty<IPersonPhoneNumberInfo> _currentPhoneNumber = new ReactiveProperty<IPersonPhoneNumberInfo>(new PersonPhoneNumberInfo(){Id = -1});

        public ReactiveProperty<IPersonPhoneNumberInfo> CurrentPhoneNumber => _currentPhoneNumber;

        public ReactiveProperty<IPersonAddressInfo> CurrentAddress { get; } = new ReactiveProperty<IPersonAddressInfo>(new PersonAddressInfo());
        public ReactiveProperty<INextOfKinInfo> CurrentNextOfKin { get; } = new ReactiveProperty<INextOfKinInfo>(new NextOfKinInfo());
    }
}