using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IPatientSyntomInfo))]
    public class PatientSyntomInfo : EntityView<IPatientSyntoms>, IPatientSyntomInfo
    {
        private string _syntom;
        private string _priority;
        private string _status;
        private IList<ISyntomMedicalSystemInfo> _systems;
        private int _priorityId;
        private int _statusId;

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

        public int PriorityId
        {
            get { return _priorityId; }
            set { this.RaiseAndSetIfChanged(ref _priorityId, value); }
        }

        public int StatusId
        {
            get { return _statusId; }
            set { this.RaiseAndSetIfChanged(ref _statusId, value); }
        }

        public IList<ISyntomMedicalSystemInfo> Systems
        {
            get { return _systems; }
            set { this.RaiseAndSetIfChanged(ref _systems, value); }
        }
    }
}