using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IPatientVitalsInfo : IEntityView<IPatients>
    {
        int Temperature { get; }
        int Pulse { get; }
        int Respiratory { get; }
        string BloodPressure { get; }
        string SaO2 { get; }

    }


}