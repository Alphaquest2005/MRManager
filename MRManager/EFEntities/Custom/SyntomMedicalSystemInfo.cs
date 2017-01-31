using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public class SyntomMedicalSystemInfo:EntityView<ISyntomMedicalSystems>, ISyntomMedicalSystemInfo
    {
        public int MedicalSystemId { get; set; }
        public string System { get; set; }
        public IList<IInterviewInfo> Interviews { get; set; }
    }
}