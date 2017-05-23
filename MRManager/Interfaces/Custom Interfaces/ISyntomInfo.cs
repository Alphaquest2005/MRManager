using System.Collections.Generic;
using SystemInterfaces;

namespace Interfaces
{
    public interface ISyntomInfo : IEntityView<ISyntoms>
    {
        
        string SyntomName { get; }
        string Priority { get; }
        string Status { get; }
        int PriorityId { get; }
        int StatusId { get; }
       
        List<IMedicalSystemInfo> MedicalSystems { get; }

    }

    public interface IMedicalSystemInfo : IEntityView<IMedicalSystems>
    {
        string Name { get; }
        List<IInterviewInfo> Interviews { get; }
    }
}