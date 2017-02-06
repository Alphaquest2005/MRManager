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
        private int _medicalSystemId;
        private string _system;
        private IList<IInterviewInfo> _interviews;

        public int MedicalSystemId
        {
            get { return _medicalSystemId; }
            set { this.RaiseAndSetIfChanged(ref _medicalSystemId, value); }
        }

        public string System
        {
            get { return _system; }
            set { this.RaiseAndSetIfChanged(ref _system, value); }
        }

        public IList<IInterviewInfo> Interviews
        {
            get { return _interviews; }
            set { this.RaiseAndSetIfChanged(ref _interviews, value); }
        }
    }
}