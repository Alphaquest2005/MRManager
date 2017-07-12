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

        private IList<IForeignAddressInfo> _foreignAddresses;

        public IList<IForeignAddressInfo> ForeignAddresses
        {
            get { return _foreignAddresses; }
            set { _foreignAddresses = value; OnPropertyChanged(); }
        }

        private IList<IForeignPhoneNumberInfo> _foreignPhoneNumbers;

        public IList<IForeignPhoneNumberInfo> ForeignPhoneNumbers
        {
            get { return _foreignPhoneNumbers; }
            set { _foreignPhoneNumbers = value; OnPropertyChanged(); }
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

        private readonly ReactiveProperty<IPersonAddressInfo> _currentAddress = new ReactiveProperty<IPersonAddressInfo>(new PersonAddressInfo(){Id = -1});

        public ReactiveProperty<IPersonAddressInfo> CurrentAddress => _currentAddress;

        private readonly ReactiveProperty<INextOfKinInfo> _currentNextOfKin = new ReactiveProperty<INextOfKinInfo>(new NextOfKinInfo() { Id = -1 });

        public ReactiveProperty<INextOfKinInfo> CurrentNextOfKin => _currentNextOfKin;

        private ReactiveProperty<IForeignPhoneNumberInfo> _currentForeignPhoneNumber = new ReactiveProperty<IForeignPhoneNumberInfo>(new ForeignPhoneNumberInfo() { Id = -1 });

        public ReactiveProperty<IForeignPhoneNumberInfo> CurrentForeignPhoneNumber => _currentForeignPhoneNumber;

        private readonly ReactiveProperty<IForeignAddressInfo> _currentForeignAddress = new ReactiveProperty<IForeignAddressInfo>(new ForeignAddressInfo() { Id = -1 });
        
        public ReactiveProperty<IForeignAddressInfo> CurrentForeignAddress => _currentForeignAddress;

        private int _selectedTabIndex;
        public int SelectedTabIndex { get { return _selectedTabIndex; } set { _selectedTabIndex = value; OnPropertyChanged(); } }
    }
}