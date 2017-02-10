using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPatientSyntomInfo : IEntityView<IPatientSyntoms>
    {
        int SyntomId { get; }
        string SyntomName { get;  }
        ISyntoms Syntom { get; }
        string Priority { get;  }
        string Status { get; }
        int PriorityId { get;  }
        int StatusId { get;  }
        int PatientVisitId { get; }
    }
}