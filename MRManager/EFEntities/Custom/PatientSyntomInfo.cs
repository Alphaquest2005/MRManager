using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    public class PatientSyntomInfo : EntityView<IPatientSyntoms>, IPatientSyntomInfo
    {
        private string _syntom;
        private string _priority;
        private string _status;
        private IList<ISyntomMedicalSystemInfo> _systems;

        public string Syntom
        {
            get { return _syntom; }
            set { this.RaiseAndSetIfChanged(ref _syntom, value); }
        }

        public string Priority
        {
            get { return _priority; }
            set { this.RaiseAndSetIfChanged(ref _priority, value); }
        }

        public string Status
        {
            get { return _status; }
            set { this.RaiseAndSetIfChanged(ref _status, value); }
        }

        public IList<ISyntomMedicalSystemInfo> Systems
        {
            get { return _systems; }
            set { this.RaiseAndSetIfChanged(ref _systems, value); }
        }
    }
}