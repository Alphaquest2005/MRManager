using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPatientDetailsInfo))]
    public partial class PatientDetailsInfo : EntityView<IPatients>, IPatientDetailsInfo
    {
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Allergies { get; set; }
        public string BirthCountry { get; set; }
        public string Job { get; set; }
        public string Religion { get; set; }
        public string Sex { get; set; }
        public string EmailAddress { get; set; }
        public string MaritalStatus { get; set; }
        public string CountryOfResidence { get; set; }
        public int? MediaId { get; set; }
        
        public DateTime BirthDate { get; set; }
        
    }
}