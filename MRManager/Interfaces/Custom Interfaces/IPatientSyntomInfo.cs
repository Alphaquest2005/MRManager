using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPatientSyntomInfo : IEntityView<IPatientSyntoms>
    {
        string Syntom { get;  }
        string Priority { get;  }
        string Status { get; }
        int PriorityId { get;  }
        int StatusId { get;  }
        IList<ISyntomMedicalSystemInfo> Systems { get; }
    }
}