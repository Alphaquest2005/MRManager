using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
   public class PatientHistoryInfo: EntityView<IPatients>, IPatientHistoryInfo
    {
       private static IPatientDetailsInfo _patientDetails;

       public IPatientDetailsInfo PatientDetails
       {
           get { return _patientDetails; }
           set { _patientDetails = value; }
       }

       private static List<IPatientVisitInfo> _patientVisits = new List<IPatientVisitInfo>();

       public List<IPatientVisitInfo> PatientVisits
       {
           get { return _patientVisits; }
           set { _patientVisits = value; }
       }

       private static List<ISyntomInfo> _synptoms = new List<ISyntomInfo>();

       public List<ISyntomInfo> Synptoms
       {
           get { return _synptoms; }
           set { _synptoms = value; }
       }
    }

    public class SyntomInfo : EntityView<ISyntoms>, ISyntomInfo
    {
        public string SyntomName { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public List<IMedicalSystemInfo> MedicalSystems { get; set; }
    }

    public class MedicalSystemInfo : EntityView<IMedicalSystems>, IMedicalSystemInfo
    {
        public string Name { get; set; }
        private List<IInterviewInfo> _interviews = new List<IInterviewInfo>();

        public List<IInterviewInfo> Interviews
        {
            get { return _interviews; }
            set  { _interviews = value; }
        }
    }
}
