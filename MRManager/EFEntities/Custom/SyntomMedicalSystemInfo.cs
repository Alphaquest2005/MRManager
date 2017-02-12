using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(ISyntomMedicalSystemInfo))]
    public class SyntomMedicalSystemInfo:EntityView<ISyntomMedicalSystems>, ISyntomMedicalSystemInfo
    {
        public int MedicalSystemId { get; set; }
        public string System { get; set; }
        public string SyntomName { get; set; }
        public IList<IInterviewInfo> Interviews { get; set; }
        public int SyntomId { get; set; }
    }
}