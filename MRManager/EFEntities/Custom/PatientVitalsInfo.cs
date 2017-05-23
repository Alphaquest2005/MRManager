using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPatientVitalsInfo))]
    public partial class PatientVitalsInfo : EntityView<IPatients>, IPatientVitalsInfo
    {
        public int Temperature { get; set; }
        public int Pulse { get; set; }
        public int Respiratory { get; set; }
        public string BloodPressure { get; set; }
        public string SaO2 { get; set; }
    }
}