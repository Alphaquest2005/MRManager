using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IPatientVisitInfo))]
    public class PatientVisitInfo : EntityView<IPatientVisit>, IPatientVisitInfo
    {
        private int _patientId;
        private DateTime _dateOfVisit;
        private string _attendingDoctor;
        private IList<IPatientSyntomInfo> _patientSyntoms;
        private string _purpose;
        private int _doctorId;
        private int _visitTypeId;

        public int PatientId
        {
            get { return _patientId; }
            set { this.RaiseAndSetIfChanged(ref _patientId, value); }
        }

        public DateTime DateOfVisit
        {
            get { return _dateOfVisit; }
            set { this.RaiseAndSetIfChanged(ref _dateOfVisit, value); }
        }

        public string AttendingDoctor
        {
            get { return _attendingDoctor; }
            set { this.RaiseAndSetIfChanged(ref _attendingDoctor, value); }
        }

        public int DoctorId
        {
            get { return _doctorId; }
            set { this.RaiseAndSetIfChanged(ref _doctorId, value); }
        }

        public IList<IPatientSyntomInfo> PatientSyntoms
        {
            get { return _patientSyntoms; }
            set { this.RaiseAndSetIfChanged(ref _patientSyntoms, value); }
        }

        public string Purpose
        {
            get { return _purpose; }
            set { this.RaiseAndSetIfChanged(ref _purpose, value); }
        }

        public int VisitTypeId
        {
            get { return _visitTypeId; }
            set { this.RaiseAndSetIfChanged(ref _visitTypeId, value); }
        }
    }
}