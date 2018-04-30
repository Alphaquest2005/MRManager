//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;

//namespace RMSDataAccessLayer

//{
//    public class Transactionlist
//    {
//       // RMSModel db = new RMSModel();
      
//        public Transactionlist(Patient p)
//        {
//            patient = p;
//            doctor = null;
//        }
//        public Transactionlist(Doctor d)
//        {
//            doctor = d;
//            patient = null;
//        }
//        Patient patient;
//        Doctor doctor;
//        public ObservableCollection<RMSDataAccessLayer.TransactionBase> TransactionList
//        {
//            get
//            {
//                if (patient != null)
//                {
//                    return new ObservableCollection<TransactionBase>(db.TransactionBase.OfType<Prescription>().Where(t => t.PatientId == patient.Id));
//                }
//                if (doctor != null)
//                {
//                    return new ObservableCollection<TransactionBase>(db.TransactionBase.OfType<Prescription>().Where(t => t.DoctorId == doctor.Id));
//                }
//                return new ObservableCollection<TransactionBase>(db.TransactionBase);
//            }
//        }
        
//    }
//}
