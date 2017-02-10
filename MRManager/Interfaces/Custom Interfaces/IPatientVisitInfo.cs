using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPatientVisitInfo : IEntityView<IPatientVisit>
    {
        int PatientId { get; }
        DateTime DateOfVisit { get;  }
        string AttendingDoctor { get;  }
        int DoctorId { get;  }
        string Purpose { get; }
        int VisitTypeId { get; }
    }
}