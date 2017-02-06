using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IPatientInfo))]
    public partial class PatientInfo : EntityView<IPatients>, IPatientInfo
    {
        private DateTime _birthDate;
        private int? _mediaId;
        private string _email;
        private string _birthCountry;
        private string _sex;
        private int? _age;
        private string _phoneNumber;
        private string _address;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Address
        {
            get { return _address; }
            set { this.RaiseAndSetIfChanged(ref _address, value); }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { this.RaiseAndSetIfChanged(ref _phoneNumber, value); }
        }

        public int? Age
        {
            get { return _age; }
            set { this.RaiseAndSetIfChanged(ref _age, value); }
        }

        public string Sex
        {
            get { return _sex; }
            set { this.RaiseAndSetIfChanged(ref _sex, value); }
        }

        public string BirthCountry
        {
            get { return _birthCountry; }
            set { this.RaiseAndSetIfChanged(ref _birthCountry, value); }
        }

        public string Email
        {
            get { return _email; }
            set { this.RaiseAndSetIfChanged(ref _email, value); }
        }

        public int? MediaId
        {
            get { return _mediaId; }
            set { this.RaiseAndSetIfChanged(ref _mediaId, value); }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { this.RaiseAndSetIfChanged(ref _birthDate, value); }
        }
    }

    
}