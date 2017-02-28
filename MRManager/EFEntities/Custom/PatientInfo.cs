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
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? Age => DateTime.Now.Year - BirthDate.Year;
        public string Sex { get; set; }
        public string BirthCountry { get; set; }
        public string Email { get; set; }
        public int? MediaId { get; set; }
        public DateTime BirthDate { get; set; }
    }

    
}