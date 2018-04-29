using System.Linq;

using RMSDataAccessLayer;

using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Windows.Data;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Ink;
using SUT.PrintEngine.Utils;
using System.Windows.Media;
using Common.Core.Logging;
using log4netWrapper;
using QuickBooks;
using SalesRegion.Messages;
using SimpleMvvmToolkit;
using TrackableEntities;
using TrackableEntities.Client;
using TrackableEntities.Common;
using TrackableEntities.EF6;

namespace SalesRegion
{
    public class SalesVM : ViewModelBase<SalesVM>
    {


        private static readonly SalesVM _instance;

        static SalesVM()
        {
            _instance = new SalesVM();
        }

        public static SalesVM Instance
        {
            get { return _instance; }
        }


        private static Cashier _cashier;

        public Cashier CashierEx
        {
            get { return _cashier; }
            set
            {
                if (_cashier != value)
                {
                    _cashier = value;
                    NotifyPropertyChanged(x => x.CashierEx);
                }
            }
        }

        public SalesVM()
        {

        }


        public void CloseTransaction()
        {
            try
            {
                Logger.Log(LoggingLevel.Info, "Close Transaction");
                if (batch == null)
                {
                    Logger.Log(LoggingLevel.Warning, "Batch is null");
                    MessageBox.Show("Batch is null");
                    return;
                }
                if (TransactionData != null && TransactionData.OpenClose == true)
                {
                    TransactionData.CloseBatchId = Batch.BatchId;
                    TransactionData.OpenClose = false;

                    SaveTransaction();

                }
                TransactionData = null;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        //public void CreateNewPrescription()
        //{
        //    try
        //    {

        //        Logger.Log(LoggingLevel.Info, "Create New Prescription");
        //        if (doctor == null)
        //        {
        //            Logger.Log(LoggingLevel.Warning, "Doctor is Missing");
        //            this.Status = "Doctor is Missing";
        //            return;
        //        }

        //        if (patient == null)
        //        {
        //            Logger.Log(LoggingLevel.Warning, "Patient is Missing");
        //            this.Status = "Patient is Missing";
        //            return;
        //        }

        //        if (Store == null)
        //        {
        //            Logger.Log(LoggingLevel.Warning, "Store is Missing");
        //            this.Status = "Store is Missing";
        //            return;
        //        }

        //        if (Batch == null)
        //        {
        //            Logger.Log(LoggingLevel.Warning, "Batch is Missing");
        //            this.Status = "Batch is Missing";
        //            return;
        //        }

        //        if (CashierEx == null)
        //        {
        //            Logger.Log(LoggingLevel.Warning, "Cashier is Missing");
        //            this.Status = "CashierEx is Missing";
        //            return;
        //        }

        //        if (Station == null)
        //        {
        //            Logger.Log(LoggingLevel.Warning, "Station is Missing");
        //            this.Status = "Station is Missing";
        //            return;
        //        }
        //        Prescription txn = new Prescription()
        //        {
        //            BatchId = Batch.BatchId,
        //            StationId = Station.StationId,
        //            Time = DateTime.Now,
        //            CashierId = CashierEx.Id,
        //            PharmacistId = (CashierEx.Role == "Pharmacist" ? CashierEx.Id : null as int?),
        //            StoreCode = Store.StoreCode,
        //            OpenClose = true,
        //            DoctorId = doctor.Id,
        //            PatientId = patient.Id,
        //            Patient = patient,
        //            Doctor = doctor,
        //            Cashier = CashierEx,
        //            Pharmacist = CashierEx.Role == "Pharmacist" ? CashierEx : null,
        //            TrackingState = TrackingState.Added
        //        };
        //        txn.StartTracking();
        //        Logger.Log(LoggingLevel.Info, "Prescription Created");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
        //        throw ex;
        //    }
        //}


        //+ ToDo: Replace this with your own data fields

        private Doctor doctor = null;

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    NotifyPropertyChanged(x => x.Doctor);
                }
            }
        }

        private Patient patient = null;

        public Patient Patient
        {
            get { return patient; }
            set
            {
                if (patient != value)
                {
                    patient = value;
                    NotifyPropertyChanged(x => x.Patient);
                }
            }
        }

        private Cashier transactionCashier = null;

        public Cashier TransactionCashier
        {
            get { return transactionCashier; }
            set
            {
                if (transactionCashier != value)
                {
                    transactionCashier = value;
                    NotifyPropertyChanged(x => x.TransactionCashier);
                }
            }
        }

        private Cashier _transactionPharmacist = null;

        public Cashier TransactionPharmacist
        {
            get { return _transactionPharmacist; }
            set
            {
                if (_transactionPharmacist != value)
                {
                    _transactionPharmacist = value;
                    if (TransactionData != null)
                    {
                        if (value != null)
                        {
                            TransactionData.PharmacistId = value.Id;
                            TransactionData.Pharmacist = value;
                        }
                    }
                    NotifyPropertyChanged(x => x.TransactionPharmacist);
                }
            }
        }

        private string status = null;

        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    NotifyPropertyChanged(x => x.Status);
                }
            }
        }


        public TransactionBase transactionData;

        public TransactionBase TransactionData
        {
            get { return transactionData; }
            set
            {
                if (!object.Equals(transactionData, value))
                {
                    Set_TransactionData(value);

                }
            }
        }

        private void Set_TransactionData(TransactionBase value)
        {
            transactionData = value;

            SendMessage(MessageToken.TransactionDataChanged,
                new NotificationEventArgs<TransactionBase>(MessageToken.TransactionDataChanged, transactionData));
            if (transactionData != null) transactionData.PropertyChanged += TransactionData_PropertyChanged;

            NotifyPropertyChanged(x => x.TransactionData);
        }

        private void TransactionData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentTransactionEntry")
            {
                if (transactionData != null)
                    if (transactionData.CurrentTransactionEntry != null)
                        if (transactionData.CurrentTransactionEntry.TransactionEntryItem != null)
                            SetCurrentItemDosage(transactionData.CurrentTransactionEntry.TransactionEntryItem);
            }
        }

        private ObservableCollection<object> _csv;

        public ObservableCollection<object> SearchList
        {
            get { return _csv; }

        }

        private ObservableCollection<Cashier> _pharmacists = null;

        public ObservableCollection<Cashier> Pharmacists
        {
            get
            {
                if (_pharmacists == null)
                {
                    using (var ctx = new RMSModel())
                    {
                        _pharmacists =
                            new ObservableCollection<Cashier>(
                                ctx.Persons.OfType<Cashier>().Where(x => x.Role == "Pharmacist"));
                        _pharmacists.ToList().ForEach(x => x.StartTracking());
                    }
                }
                return _pharmacists;
            }
        }


        private Cashier _currentPharmacist = null;

        public Cashier CurrentPharmacist
        {
            get { return _currentPharmacist; }
            set
            {
                if (_currentPharmacist != value)
                {
                    _currentPharmacist = value;
                    NotifyPropertyChanged(x => CurrentPharmacist);
                }
            }
        }


        public void UpdateSearchList(string filterText)
        {
            try
            {
                Logger.Log(LoggingLevel.Info,
                    string.Format("Update SearchList -filter Text [{0}] - StartTime:{1}", filterText, DateTime.Now));
                var lst = CreateSearchList(filterText);


                _csv = new ObservableCollection<Object>();
                foreach (var item in lst)
                {
                    _csv.Add(item);
                }
                NotifyPropertyChanged(x => x.SearchList);
                Logger.Log(LoggingLevel.Info,
                    string.Format("Finish Update SearchList - filter Text [{0}] - EndTime:{1}", filterText,
                        DateTime.Now));
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void GetSearchResults(string filterText)
        {
            UpdateSearchList(filterText);
        }



        private List<dynamic> CreateSearchList(string filterText)
        {
            try
            {
                //todo: make parallel
                Logger.Log(LoggingLevel.Info,
                    string.Format("Start Create SearchList -filter Text [{0}] - StartTime:{1}", filterText,
                        DateTime.Now));

                var searchItemsBag = new List<dynamic>();
                var patientsBag = new List<dynamic>();
                var doctorsBag = new List<dynamic>();
                var inventoryBag = new List<dynamic>();
                var transactionBag = new List<dynamic>();
                var qtransactionBag = new List<dynamic>();
                var taskLst = new List<Task>();
                taskLst.Add(Task.Run(() =>
                {
                        searchItemsBag.AddRange(AddSearchItems());
                   
                }));

                taskLst.Add(Task.Run(() =>
                {
                    patientsBag.AddRange(GetPatients(filterText));
                   
                }));

                taskLst.Add(Task.Run(() =>
                {

                    doctorsBag.AddRange(GetDoctors(filterText));
                   
                }));
                taskLst.Add(Task.Run(() =>
                {
                   
                        inventoryBag.AddRange(AddInventory(filterText));
                    
                }));


                double t = 0;
                if (double.TryParse(filterText, out t))
                {
                    taskLst.Add(Task.Run(() =>
                    {
                        
                            transactionBag.AddRange(AddTransactions(filterText));
                        
                    }));

                    taskLst.Add(Task.Run(() =>
                    {
                       
                        qtransactionBag.AddRange(AddQuickTransactions(filterText));
                        
                    }));

                }
                Task.WaitAll(taskLst.ToArray());

                Logger.Log(LoggingLevel.Info,
                    string.Format("Finish Create SearchList -filter Text [{0}] - StartTime:{1}", filterText,
                        DateTime.Now));
                transactionBag.AddRange(qtransactionBag);
                inventoryBag.AddRange(transactionBag);
                doctorsBag.AddRange(inventoryBag);
                patientsBag.AddRange(doctorsBag);
                searchItemsBag.AddRange(patientsBag);

                return searchItemsBag;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        private List<dynamic> AddSearchItems()
        {
            try
            {
                var cc = new List<dynamic>();

                SearchItem p = new SearchItem();
                p.SearchObject = null;
                p.SearchCriteria = "Add Patient";
                p.DisplayName = "Add Patient";
                cc.Add(p);

                SearchItem d = new SearchItem();
                d.SearchObject = null;
                d.SearchCriteria = "Add Doctor";
                d.DisplayName = "Add Doctor";
                cc.Add(d);


                return cc;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        private List<Prescription> AddTransactions(string filterText)
        {

            try
            {
                using (var ctx = new RMSModel())
                {
                    return
                        ctx.TransactionBase.OfType<Prescription>()
                            .Where(x => x.TransactionId.ToString().StartsWith(filterText))
                            .OrderBy(t => t.Time)
                            .Take(listCount).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private List<QuickPrescription> AddQuickTransactions(string filterText)
        {

            try
            {
                using (var ctx = new RMSModel())
                {
                    // right now any Transactions
                    return
                        ctx.TransactionBase.OfType<QuickPrescription>()
                            .Where(x => x.TransactionId.ToString().StartsWith(filterText))
                            .OrderBy(t => t.Time)
                            .Take(listCount).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }






        private List<Doctor> GetDoctors(string filterText)
        {
            try
            {
                using (var ctx = new RMSModel())
                {
                    return ctx.Persons.OfType<Doctor>()
                        .Where(
                            x =>
                                ("Dr. " + " " +
                                 x.FirstName.Trim().Replace(".", "").Replace(" ", "").Replace("Dr", "Dr. ") + " " +
                                 x.LastName.Trim() +
                                 " " + x.Code).Contains(filterText))
                        .Take(listCount).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private List<Patient> GetPatients(string filterText)
        {
            try
            {
                using (var ctx = new RMSModel())
                {
                    return ctx.Persons.OfType<Patient>()
                        .Where(x => (x.FirstName.Trim() + " " + x.LastName.Trim()).Contains(filterText))
                        .Take(listCount).ToList();

                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private int listCount = 10;
        private List<Medicine> AddInventory(string filterText)
        {
            try
            {
                //todo: make parallel
                using (var ctx = new RMSModel())
                {

                    return ctx.Item.OfType<Medicine>().Where(x =>
                            ((x.ItemName ?? x.Description).Contains(filterText) ||
                             (x.ItemNumber.ToString().StartsWith(filterText)))
                            && x.QBItemListID != null
                            // && x.Quantity > 0                           && 
                            && x.QBActive == true
                            && (x.Inactive == null ||
                                (x.Inactive != null && x.Inactive == _showInactiveItems)))
                        .OrderBy(x => x.ItemName)
                        .Take(listCount*2).ToList();


                }


            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }


        }


        private bool _showInactiveItems = false;

        public bool ShowInactiveItems
        {
            get { return _showInactiveItems; }
            set
            {
                _showInactiveItems = value;
                NotifyPropertyChanged(x => x.ShowInactiveItems);
            }

        }

        




        public void ProcessSearchListItem(object SearchItem)
        {
            try
            {
                if (SearchItem == null) return;
                if (TransactionData != null && TransactionData.ChangeTracker == null) TransactionData.StartTracking();
                if (typeof(RMSDataAccessLayer.SearchItem) == SearchItem.GetType())
                {
                    DoSearchItem(SearchItem as RMSDataAccessLayer.SearchItem);
                }

                if (typeof(RMSDataAccessLayer.Doctor) == SearchItem.GetType())
                {
                    AddDoctorToTransaction(SearchItem as Doctor);
                }

                if (typeof(RMSDataAccessLayer.Patient) == SearchItem.GetType())
                {
                    AddPatientToTransaction(SearchItem as Patient);
                }

                var searchItem = SearchItem as Item;
                if (searchItem != null)
                {

                    var itm = searchItem;
                    //  if (CheckDuplicateItem(itm)) return;
                    if (itm.Quantity < 0)
                    {
                        var res = MessageBox.Show("Item may not be in stock! Do you want to continue?",
                            "Negative Stock",
                            MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.No) return;
                    }
                    SetCurrentItemDosage(itm);

                    if (TransactionData != null)
                    {
                        InsertItemTransactionEntry(itm);
                    }
                    else
                    {
                        NewItemTransaction(itm);
                    }

                }
                var trn = SearchItem as TransactionBase;
                if (trn != null)
                {
                    GoToTransaction(trn);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void SetCurrentItemDosage(TransactionEntryItem itm)
        {
            if (itm.Item == null)
            {
                using (var ctx = new RMSModel())
                {
                    itm.Item = ctx.Item.FirstOrDefault(x => x.ItemId == itm.ItemId);
                    itm.TrackingState = TrackingState.Unchanged;
                }
            }
            SetCurrentItemDosage(itm.Item);
        }

        public void SetCurrentItemDosage(Item itm)
        {
            if (itm == null) return;
            using (var ctx = new RMSModel())
            {
                itm.DosageList =
                    ctx.ItemDosages.Where(x => x.ItemId == itm.ItemId)
                        .OrderByDescending(x => x.Count)
                        .Take(5)
                        .Select(x => x.Dosage)
                        .ToList();
                itm.TrackingState = TrackingState.Unchanged;
            }
        }

        //private bool CheckDuplicateItem(Item itm)
        //{
        //    if (TransactionData != null &&
        //        TransactionData.TransactionEntries.FirstOrDefault(x => x.TransactionEntryItem.ItemId == itm.ItemId) != null)
        //    {
        //        MessageBox.Show("Can't add same item twice!");
        //        return true;
        //    }
        //    return false;
        //}

        private void DoSearchItem(SearchItem searchItem)
        {
            throw new NotImplementedException();
        }


        private void AddPatientToTransaction(Patient patient)
        {
            if (patient == null) return;
            Patient = patient;
            if (TransactionData is Prescription == false)
            {
                var t = NewPrescription();
                CopyTransactionDetails(t, TransactionData);
                DeleteTransactionData();
                TransactionData = t;
            }
            var prescription = (Prescription) TransactionData;
            if (prescription != null)
            {
                prescription.PatientId = patient.Id;
                prescription.Patient = patient;
                prescription.Patient.StartTracking();
            }

        }



        private void AddDoctorToTransaction(Doctor doctor)
        {
            if (doctor == null) return;
            Doctor = doctor;
            if (TransactionData is Prescription == false)
            {
                var t = NewPrescription();
                CopyTransactionDetails(t, TransactionData);
                DeleteTransactionData();
                TransactionData = t;
            }

            var prescription = TransactionData as Prescription;
            if (prescription != null)
            {
                prescription.DoctorId = doctor.Id;
                prescription.Doctor = doctor;
                prescription.Doctor.StartTracking();
            }

        }


        private void GoToTransaction(TransactionBase trn)
        {
            GoToTransaction(trn.TransactionId);
        }


        public void GoToTransaction(int TransactionId)
        {
            try
            {
                if (TransactionId == 0) return;
                using (var ctx = new RMSModel())
                {
                    TransactionBase ntrn;
                    ntrn = (from t in ctx.TransactionBase
                            .Include(x => x.TransactionEntries)

                            .Include(x => x.Cashier)
                            .Include("TransactionEntries.TransactionEntryItem")
                            .Include("TransactionEntries.TransactionEntryItem.Item")
                            .Include("ParentTransaction.TransactionEntries.TransactionEntryItem.Item")
                            .Include(x => x.ParentTransaction.Transactions)

                        //.Include("TransactionEntries.Item.ItemDosages")
                        where t.TransactionId == TransactionId
                        orderby t.Time descending
                        select t).FirstOrDefault();
                    ntrn.StartTracking();

                    if (ntrn != null)
                    {
                        IncludePrecriptionProperties(ctx, ntrn);
                        Item = null;
                        NotifyPropertyChanged(x => x.Item.DosageList);
                        TransactionData = ntrn;
                    }



                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        public void GoToPreviousTransaction()
        {
            try
            {
                using (var ctx = new RMSModel())
                {
                    TransactionBase ptrn;

                    if (TransactionData == null || TransactionData.TransactionId == 0)
                    {
                        ptrn = GetDBTransaction(ctx).FirstOrDefault();
                    }
                    else
                    {
                        ptrn = GetDBTransaction(ctx)
                            .FirstOrDefault(t => t.TransactionId < TransactionData.TransactionId);
                    }
                    ptrn.StartTracking();
                    if (ptrn != null)
                    {
                        IncludePrecriptionProperties(ctx, ptrn);
                        Item = null;
                        NotifyPropertyChanged(x => x.Item.DosageList);

                        //  IncludeInventoryProperties(ctx, ptrn);
                        TransactionData = ptrn;
                        this.Item = null;
                    }
                    else
                    {
                        MessageBox.Show("No previous transaction");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        private IOrderedQueryable<TransactionBase> GetDBTransaction(RMSModel ctx)
        {
            try
            {
                TransactionBase ptrn;
                return (from t in ctx.TransactionBase
                        .Include(x => x.TransactionEntries)
                        .Include(x => x.Cashier)
                        //  .Include(x => x.OldPrescription)
                        //  .Include("OldPrescription.TransactionEntries")
                        // .Include(x => x.Repeats)
                        //  .Include("Repeats.TransactionEntries")
                        .Include("TransactionEntries.TransactionEntryItem")
                        .Include("TransactionEntries.TransactionEntryItem.Item")
                        .Include("ParentTransaction.TransactionEntries.TransactionEntryItem.Item")
                        .Include(x => x.ParentTransaction.Transactions)
                            //.Include("TransactionEntries.Item.ItemDosages")
                        orderby t.Time descending
                    select t);

                //return ptrn;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void IncludePrecriptionProperties(TransactionBase ptrn)
        {
            try
            {
                using (var ctx = new RMSModel())
                {
                    IncludePrecriptionProperties(ctx, ptrn);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void IncludePrecriptionProperties(RMSModel ctx, TransactionBase ptrn)
        {
            try
            {
                var tasklst = new List<Task>();
                if (ptrn is Prescription pc)
                {
                    tasklst.Add(Task.Run(() => { SetOtherFields(pc); }));
                    tasklst.Add(Task.Run(() => { SetTransactions(pc); }));
                   // tasklst.Add(Task.Run(() => { SetParentTransactions(pc); }));
                }
                tasklst.Add(Task.Run(() => { SetOtherTransactionFields(ptrn); }));
                Task.WaitAll(tasklst.ToArray());
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error,
                    GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private void SetOtherTransactionFields(TransactionBase ptrn)
        {
            using (var ctx = new RMSModel())
            {
                this.TransactionCashier = ctx.Persons.OfType<Cashier>().FirstOrDefault(x => x.Id == ptrn.CashierId);

                this.TransactionPharmacist =
                    ctx.Persons.OfType<Cashier>().FirstOrDefault(x => x.Id == ptrn.PharmacistId);
            }
            

        }
    

    private static void SetTransactions(Prescription pc)
        {
            using (var ctx = new RMSModel())
            {
                pc.Transactions = ctx.TransactionBase.OfType<Prescription>().Include(x => x.Transactions)
                    .Include("Transactions.TransactionEntries.TransactionEntryItem")
                    .Include(x => x.TransactionEntries)
                    .First(x => x.TransactionId == pc.TransactionId).Transactions;
            }
        }

        private static void SetParentTransactions(Prescription pc)
        {
            using (var ctx = new RMSModel())
            {
                pc.ParentTransaction = ctx.TransactionBase.OfType<Prescription>()
                    .Include(x => x.Transactions)
                    .Include("Transactions.TransactionEntries.TransactionEntryItem")
                    .Include("TransactionEntries.TransactionEntryItem")
                    .FirstOrDefault(x => x.TransactionId == pc.ParentTransactionId);
            }
        }
        private static void SetOtherFields(Prescription pc)
        {
            using (var ctx = new RMSModel())
            {
                pc.Doctor = ctx.Persons.OfType<Doctor>().FirstOrDefault(x => x.Id == pc.DoctorId);
                pc.Doctor.StartTracking();
                pc.Pharmacist = ctx.Persons.OfType<Cashier>().FirstOrDefault(x => x.Id == pc.PharmacistId);
                pc.Pharmacist?.StartTracking();
                pc.Patient = ctx.Persons.OfType<Patient>().FirstOrDefault(x => x.Id == pc.PatientId);
                pc.Patient.StartTracking();
            }
        }





        private void InsertItemTransactionEntry(RMSDataAccessLayer.Item itm)
        {
            try
            {
                var medicine = itm as Medicine;
                if (TransactionData.CurrentTransactionEntry == null)
                {
                   
                        PrescriptionEntry p = new PrescriptionEntry()
                        {
                            StoreID = Store.StoreId,
                            TransactionId = TransactionData.TransactionId,
                            TransactionEntryItem = CreateTransactionEntryItem(itm),
                            
                            Price = itm.Price,
                            Dosage = medicine == null?"":medicine.SuggestedDosage,
                            Taxable = itm.SalesTax != 0,
                            SalesTaxPercent = itm.SalesTax.GetValueOrDefault(),
                            TransactionTime = DateTime.Now,
                            EntryNumber =
                                TransactionData.TransactionEntries == null
                                    ? 1
                                    : (short?)TransactionData.TransactionEntries.Count,
                            // Transaction = TransactionData,
                            
                            TrackingState = TrackingState.Added
                        };
                        p.StartTracking();
                    
                        TransactionData.TransactionEntries.Add(p);
                        NotifyPropertyChanged(x => TransactionData.TransactionEntries);
                        this.TransactionData.CurrentTransactionEntry = p;
                    
                }
                else
                {
                    var item = this.TransactionData.CurrentTransactionEntry;
                    if (item != null)
                    {
                        SetTransactionEntryItem(itm, item);

                        item.Price = itm.Price;
                       
                        //if (medicine != null) item.Dosage = medicine.SuggestedDosage;
                        
                        this.TransactionData.UpdatePrices();
                    }
                    
                    
                    this.Item = itm;
                }





                NotifyPropertyChanged(x => x.TransactionData);
                //NotifyPropertyChanged(x => x.CurrentTransactionEntry);
                NotifyPropertyChanged(x => x.Item);
                return;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private TransactionEntryItem CreateTransactionEntryItem(Item itm)
        {
            if (itm == null) return null;
            return new TransactionEntryItem()
            {
                ItemId = itm.ItemId,
                ItemName = itm.ItemName ?? itm.Description,
                ItemNumber = itm.ItemNumber,
            QBItemListID = itm.QBItemListID,
                Item = itm,
                TrackingState = TrackingState.Added
            };
        }

        private TransactionEntryItem CreateTransactionEntryItem(TransactionEntryItem itm)
        {
            var newitm = GetCurrentQBInventoryItem(itm);
            if (newitm == null)
            {
                MessageBox.Show(
                    $"Item --'{itm.ItemName}' Not Found In current Inventory. Please create this Prescription Entry Manually!");
                return null;
            }
            var ti = new TransactionEntryItem()
            {
                ItemId = newitm.ItemId,
                ItemName = newitm.ItemName,
                ItemNumber = newitm.ItemNumber,
                QBItemListID = newitm.QBItemListID,
                Item = newitm.Item,
                TrackingState = TrackingState.Added
            };
            
            return ti;
        }

        private TransactionEntryItem GetCurrentQBInventoryItem(TransactionEntryItem oldEntryItem)
        {
            using (var ctx = new RMSModel())
            {
                //if item exist and is qbactive return it.
               var eitm = ctx.QBInventoryItems
                    .FirstOrDefault(
                        x => x.ListID.ToString() == oldEntryItem.QBItemListID && x.Items.Any(z => z.ItemNumber == oldEntryItem.ItemNumber && z.QBActive.Value == true));
                if (eitm != null) return oldEntryItem;

                var res =
                    ctx.QBInventoryItems
                    .Include(x => x.Items)
                    .FirstOrDefault(
                        x => x.ItemNumber.ToString() == oldEntryItem.ItemNumber && x.Items.Any(z =>z.ItemNumber == oldEntryItem.ItemNumber && z.QBActive.Value == true));
               
                if (res != null)
                {
                    var itm = res.Items.OrderByDescending(x => x.Inactive).FirstOrDefault();
                    MessageBox.Show(
                        $"Existing Item {oldEntryItem.ItemName} don't exist in QBInventory, it will be replaced with {itm.ItemName}");
                    return new TransactionEntryItem() {Item = itm, ItemNumber = itm.ItemNumber, ItemName = itm.ItemName, QBItemListID = res.ListID, ItemId = itm.ItemId, TrackingState = oldEntryItem.TrackingState, TransactionEntryId = oldEntryItem.TransactionEntryId, TransactionEntryBase = oldEntryItem.TransactionEntryBase};  

                }
                return null;
            }
        }

        private static void SetTransactionEntryItem(Item itm, PrescriptionEntry item)
        {
            if (itm == null) return;
            if (item?.TransactionEntryItem == null) return;
            item.TransactionEntryItem.TransactionEntryId = item.TransactionEntryId;
            item.TransactionEntryItem.TrackingState = TrackingState.Modified;
            item.TransactionEntryItem.ItemId = itm.ItemId;
            item.TransactionEntryItem.ItemName = itm.ItemName ?? itm.Description;
            item.TransactionEntryItem.ItemNumber = itm.ItemNumber;
            item.TransactionEntryItem.QBItemListID = itm.QBItemListID;
            item.TransactionEntryItem.Item = itm;
        }


        private bool AutoCreateOldTransactions()
        {
            try
            {
                if (TransactionData == null) return false;
                if (TransactionData.Time.Date != DateTime.Now.Date)
                {
                      MessageBox.Show(
                            "Modifying old transactions is not allowed! Do you want to create a New Transaction?",
                            "Can't Modify Old Transaction", MessageBoxButton.OK);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        public void DeleteTransactionEntry<T>(TransactionEntryBase dtrn) where T : TransactionEntryBase
        {
            try
            {
                if (dtrn == null)
                {

                    return;
                }
                if (AutoCreateOldTransactions() == false) return;

                using (var ctx = new RMSModel())
                {
                    var d = ctx.TransactionEntryBase.FirstOrDefault(x => x.TransactionEntryId == dtrn.TransactionEntryId);
                    if (d != null)
                    {
                        d.TrackingState = TrackingState.Deleted;
                        ctx.ApplyChanges(d);
                        ctx.SaveChanges();
                        d.AcceptChanges();
                    }
                    //else
                    //{
                    //    TransactionData.TransactionEntries.Remove(dtrn);
                    //}

                    //NotifyPropertyChanged(x => TransactionData.TransactionEntries);
                    //NotifyPropertyChanged(x => TransactionData);
                    //TransactionData.UpdatePrices();

                }
                GoToTransaction(TransactionData.TransactionId);
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


        public void DeleteCurrentTransaction()
        {
            try
            {


                Logger.Log(LoggingLevel.Info,
                    string.Format("Start DeleteCurrentTransaction: StartTime:{0}", DateTime.Now));
                if (
                    MessageBox.Show("Are you sure you want to delete?", "Delete Current Transaction",
                        MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    if (TransactionData != null && TransactionData.Time.Date != DateTime.Now.Date)
                    {
                        MessageBox.Show("Modifying old transactions is not allowed!",
                            "Can't Modify Old Transaction");
                        return;
                    }

                    //if (TransactionData.Repeats.Any())
                    //{
                    //    MessageBox.Show("This Prescription has been repeated! ");
                    //    return;
                    //}
                    DeleteTransactionData();
                    GoToPreviousTransaction();

                }
                Logger.Log(LoggingLevel.Info,
                    string.Format("Finish DeleteCurrentTransaction: EndTime:{0}", DateTime.Now));
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private void DeleteTransactionData()
        {
            if (TransactionData != null && TransactionData.TrackingState != TrackingState.Added)
            {
                using (var ctx = new RMSModel())
                {
                    var t = ctx.TransactionBase.FirstOrDefault(x => x.TransactionId == TransactionData.TransactionId);
                    if (TransactionData != null)
                    {
                        t.TrackingState = TrackingState.Deleted;
                        ctx.ApplyChanges(t);
                        ctx.SaveChanges();
                    }
                    TransactionData.TrackingState = TrackingState.Deleted;
                    // TransactionData.AcceptChanges();
                }
            }
            TransactionData = null;
        }


        public TransactionBase CopyCurrentTransaction(bool copydetails = true)
        {
            try
            {
                using (var t = new TransactionScope())
                {
                    dynamic newt = null;
                    if (TransactionData is Prescription)
                    {
                        var p = NewPrescription();
                        p.StartTracking();

                        var doc = ((Prescription) TransactionData).Doctor;
                        if (doc != null)
                        {
                            p.Doctor = doc;
                            p.DoctorId = p.Doctor.Id;
                            p.Doctor.StartTracking();
                        }
                        var pat = ((Prescription) TransactionData).Patient;
                        if (pat != null)
                        {
                            p.Patient = pat;
                            p.Patient.StartTracking();
                            p.PatientId = p.Patient.Id;
                        }
                        newt = p;
                    }
                    if (TransactionData is QuickPrescription) newt = CreateNewQuickPrescription();

                    newt.StartTracking();

                    if (copydetails)
                    {
                        CopyTransactionDetails(newt, TransactionData);
                    }
                    newt.UpdatePrices();
                    t.Complete();
                    return newt;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, ex.Message + " | " + ex.StackTrace);
                throw ex;
            }
        }

        private void CopyTransactionDetails(dynamic newt, TransactionBase t)
        {
            if (newt == null || t == null) return;

            var entries = t.TransactionEntries.OfType<PrescriptionEntry>();
            //VerifyInventory(entries);

            foreach (var itm in entries)
            {
                 var ti = CreateTransactionEntryItem(itm.TransactionEntryItem);
                if (ti == null) continue;
                var te = new PrescriptionEntry()
                {
                    Dosage = itm.Dosage,
                    TransactionEntryItem = ti,
                    Repeat = itm.Repeat,
                    RepeatQuantity = itm.RepeatQuantity,//?? Convert.ToInt32(itm.Quantity),
                    Quantity = itm.RepeatQuantity.GetValueOrDefault() != 0 ? itm.RepeatQuantity.GetValueOrDefault() : Convert.ToInt32(itm.Quantity),
                    SalesTaxPercent = itm.SalesTaxPercent,
                    Price = itm.Price,
                    ExpiryDate = itm.ExpiryDate,
                    Comment = itm.Comment,
                    
                    TrackingState = TrackingState.Added
                };
               

                te.StartTracking();
                
                newt.TransactionEntries.Add(te);
            }
        }

        private void VerifyInventory(IEnumerable<PrescriptionEntry> entries)
        {
            using (var ctx = new RMSModel())
            {
                foreach (var itm in entries)
                {
                    var inv = ctx.Item.FirstOrDefault(x => x.QBItemListID == itm.TransactionEntryItem.QBItemListID && x.QBActive == true);
                    if (inv == null)
                        MessageBox.Show(
                            $"{itm.TransactionEntryItem.ItemName}-{itm.TransactionEntryItem.ItemNumber} is not Availible in QuickBooks! please Re-Create item.");
                }
            }
        }


        public void AutoRepeat(TransactionBase data = null)
        {
            var myTransactionData = data ?? TransactionData;
            try
            {
                //var pres = myTransactionData as Prescription;
                //if (pres == null)
                //{
                //    MessageBox.Show("Only Transactions can be repeated.");
                //    return;
                //}

                var pres = myTransactionData;
                if (pres.ParentTransactionId != null)
                {
                    GoToTransaction(pres.ParentTransactionId.GetValueOrDefault());
                }
                

                var newt = CopyCurrentTransaction();

                
                    newt.ParentTransactionId = pres.ParentTransactionId ?? pres.TransactionId;
                    newt.ParentTransaction = pres;
                


                foreach (PrescriptionEntry item in newt.TransactionEntries.ToList())
                {
                    item.Transaction = newt;
                    if (item.Remaining == 0 && pres is Prescription)
                    {
                        newt.TransactionEntries.Remove(item);
                        continue;
                    }
                   // item.Quantity = item.Remainder > 0 ? item.Remainder : item.RepeatQuantity.GetValueOrDefault();


                }
                if (newt.TransactionEntries.Any())
                {
                    TransactionData = newt;

                    if (!SaveTransaction()) return;
                    SalesVM.Instance.GoToTransaction(newt.TransactionId);
                }
                else
                {
                    MessageBox.Show("Prescription is completely filled.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ;
            }

        }


        private void NewItemTransaction(Item SearchItem)
        {
            try
            {
              //  if (CheckDuplicateItem(SearchItem)) return;
                if (TransactionData == null)
                {
                    TransactionData = CreateNewQuickPrescription();
                    TransactionData.StartTracking();
                }
                InsertItemTransactionEntry(SearchItem as Item);
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private QuickPrescription CreateNewQuickPrescription()
        {
            try
            {
                return new QuickPrescription()
                {
                    BatchId = Batch.BatchId,
                    StationId = Station.StationId,
                    Time = DateTime.Now,
                    CashierId = CashierEx.Id,
                    PharmacistId = (CashierEx.Role == "Pharmacist" ? CashierEx.Id : null as int?),
                    StoreCode = Store.StoreCode,
                    OpenClose = true,
                    TrackingState = TrackingState.Added
                };
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }


       

        public void Print(ref FrameworkElement fwe, PrescriptionEntry prescriptionEntry)
        {
            try
            {
              
                
                //LocalPrintServer printserver = new LocalPrintServer();
                PrintServer printserver = new PrintServer(Station.PrintServer);


                Size visualSize;

                visualSize = new Size(288, 2 * 96); // paper size

                DrawingVisual visual = PrintControlFactory.CreateDrawingVisual(fwe, fwe.ActualWidth, fwe.ActualHeight);


                SUT.PrintEngine.Paginators.VisualPaginator page = new SUT.PrintEngine.Paginators.VisualPaginator(
                    visual, visualSize, new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 0));
                page.Initialize(false);

                PrintDialog pd = new PrintDialog();
                pd.PrintQueue = printserver.GetPrintQueue(Station.ReceiptPrinterName);

                pd.PrintDocument(page, "");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Print error! Please check prints and reprint and also tell joseph you saw this error in SalesVM.");
                Instance.UpdateTransactionEntry(ex, prescriptionEntry);
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
              //  throw ex;
            }
        }

        


        public void PostQBSale()
        {

            try
            {

                if (TransactionData == null || string.IsNullOrEmpty(TransactionData.TransactionNumber))
                {
                    MessageBox.Show("Invalid Transaction Please Try again");
                    return;
                }
                if (TransactionData.ChangeTracker == null) TransactionData.StartTracking();
                    TransactionData.Status = "ToBePosted";
                if (!SaveTransaction())
                {
                    MessageBox.Show("Post failed to Save! Please Check that all fields are entered and try again.");
                    return;
                } 
                if (ServerMode != true)
                {
                    Post(TransactionData);
                }

            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

       

        private void Post(TransactionBase TransactionData)
        {
            try
            {

                IncludePrecriptionProperties(TransactionData);

                SalesReceipt s = new SalesReceipt();
                s.TxnDate = TransactionData.Time;
                s.TxnState = "1";
                s.Workstation = "02";
                s.StoreNumber = "1";
                s.SalesReceiptNumber = "123";
                s.Discount = "0";

                
                if (TransactionData is Prescription)
                {
                    Prescription p = TransactionData as Prescription;
                    string doctor = "";
                    string patient = "";
                    if (p.Doctor != null)
                    {
                        doctor = p.Doctor.DisplayName;
                        s.Discount = p.Doctor.Discount == null ? "" : p.Doctor.Discount.ToString();
                    }
                    if (p.Patient != null)
                    {
                        patient = p.Patient.ContactInfo;
                        s.Discount = p.Patient.Discount == null ? "" : p.Patient.Discount.ToString();
                    }
                    s.Comments = String.Format("{0} \n RX#:{1} \n Doctor:{2}", patient,
                        p.TransactionNumber, doctor);
                }
                else
                {
                    s.Comments = "RX#:" + TransactionData.TransactionNumber;
                }

                if (TransactionData != null)
                {
                    s.TrackingNumber = TransactionData.TransactionNumber;
                }
                s.Associate = "Dispensary";
                s.SalesReceiptType = "0";



                foreach (var item in TransactionData.TransactionEntries)
                {
                    if (item.TransactionEntryItem!= null)
                    {

                        s.SalesReceiptDetails.Add(new SalesReceiptDetail
                        {
                            ItemListID = item.TransactionEntryItem.QBItemListID,
                            QtySold = (Decimal)item.Quantity
                        }); //340 
                    }
                    else
                    {
                        ////MessageBox.Show("Please Link Quickbooks item to dispensary");
                        //TransactionData.Status = "Please Link Quickbooks item to dispensary";
                        //rms.SaveChanges();
                        return;
                    }
                }


                // QBPOS qb = new QBPOS();
                SalesReceiptRet result = QBPOS.Instance.AddSalesReceipt(s);
                if (result != null)
                {
                    TransactionData.ReferenceNumber = "QB#" + result.SalesReceiptNumber;
                    TransactionData.Status = "Posted";
                    SaveTransaction(TransactionData);
                    //using (var ctx = new RMSModel())
                    //{
                    //    TransactionData.ReferenceNumber = "QB#" + result.SalesReceiptNumber;
                    //    TransactionData.Status = "Posted";
                       
                    //    //ctx.TransactionBase.AddOrUpdate(TransactionData);
                    //    //ctx.SaveChanges();
                    //}
                }
                else
                {
                    // problem
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public async Task DownloadAllQBItems()
        {
            try
            {
                await Task.Run(() =>
                {
                    var t = QBPOS.Instance.GetAllInventoryQuery().Result;
                    ProcessQBItems(t);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        private async Task ProcessQBItems(List<ItemInventoryRet> itms)
        {
            try
            {
                if (itms != null)
                {
                    var itmcnt = 0;
                    List<Medicine> clst = null;
                    using (var ctx = new RMSModel())
                    {
                        clst = ctx.Item.OfType<Medicine>()
                            .Where(x => x.QBItemListID != null)
                            // .Where(x => x.ItemNumber == "6315")
                            .ToList();
                    }
                    Parallel.ForEach(itms, (item) => //.Where(x => x.ItemNumber == 6315)
                    {
                        //if (itmcnt%100 == 0)
                        //{
                        //    ctx.SaveChanges(); //SaveDatabase();
                        //}
                        using (var ctx = new RMSModel())
                        {
                            QBInventoryItem i = ctx.QBInventoryItems.FirstOrDefault(x => x.ListID == item.ListID);
                            if (i == null)
                            {
                                i = new QBInventoryItem()
                                {
                                    ListID = item.ListID,
                                    ItemName = item.Desc1,
                                    ItemDesc2 = item.Desc2,
                                    Size = item.Size,
                                    DepartmentCode = "MISC",
                                    ItemNumber = System.Convert.ToInt16(item.ItemNumber),
                                    TaxCode = item.TaxCode,
                                    Price = System.Convert.ToDouble(item.Price1),
                                    Quantity = System.Convert.ToDouble(item.QuantityOnHand),
                                    UnitOfMeasure = item.UnitOfMeasure
                                };

                                ctx.QBInventoryItems.Add(i);
                            }

                            i.ItemName = item.Desc1;
                            i.ItemDesc2 = item.Desc2;
                            i.ListID = item.ListID;
                            i.Size = item.Size;
                            i.UnitOfMeasure = item.UnitOfMeasure;
                            i.TaxCode = item.TaxCode;
                            i.ItemNumber = System.Convert.ToInt16(item.ItemNumber);
                            i.Price = System.Convert.ToDouble(item.Price1);
                            i.Quantity = System.Convert.ToDouble(item.QuantityOnHand);

                            ctx.QBInventoryItems.AddOrUpdate(i);

                            Medicine itm = clst.FirstOrDefault(x => x.QBItemListID == i.ListID);
                            if (itm == null)
                            {
                                itm = new Medicine()
                                {
                                    DateCreated = DateTime.Now,
                                    SuggestedDosage = "Take as Directed by your Doctor"
                                };

                                ctx.Item.Add(itm);
                            }

                            if (itm != null)
                            {
                                itm.Description = i.ItemDesc2;
                                itm.Price = i.Price.GetValueOrDefault();
                                itm.Quantity = Convert.ToDouble(i.Quantity);
                                itm.SalesTax = (i.TaxCode != null && i.TaxCode.ToUpper() == "VAT"
                                    ? .15
                                    : 0);
                                itm.QBItemListID = i.ListID;
                                itm.UnitOfMeasure = i.UnitOfMeasure;
                                itm.ItemName = i.ItemName;
                                itm.ItemNumber = i.ItemNumber.ToString();
                                itm.Size = i.Size;
                                ctx.Item.AddOrUpdate(itm);
                            }
                            ctx.SaveChanges();
                        }
                        // itmcnt += 1;
                    });
                    //SaveDatabase();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public void Notify(string token, object sender, NotificationEventArgs e)
        {
            MessageBus.Default.Notify(token, sender, e);
        }






        private Item item = null;

        public Item Item
        {
            get { return item; }
            set
            {
                if (item != null)
                {
                    item = value;
                    NotifyPropertyChanged(x => x.Item);
                }
            }
        }

        private ObservableCollection<TransactionsView> transactionList = null;

        public ObservableCollection<TransactionsView> TransactionList
        {
            get { return transactionList; }
            set
            {
                if (transactionList != value)
                {
                    transactionList = value;
                    NotifyPropertyChanged(x => x.TransactionList);
                }
            }
        }

        public Patient CreateNewPatient(string searchtxt)
        {
            var p = CreateNewPatient();
            p.StartTracking();
            SetNames(searchtxt, p);
            return p;
        }

        private void SetNames(string searchtxt, Person p)
        {
            var strs = searchtxt.Split(' ');
            p.FirstName = strs.FirstOrDefault();
            p.LastName = strs.LastOrDefault();
        }

        public Patient CreateNewPatient()
        {
            return new Patient(){TrackingState = TrackingState.Added};
        }

        public bool SavePerson(Person patient)
        {
            var res = false;
            try
            {
                
                using (var ctx = new RMSModel())
                {
                    ctx.ApplyChanges(patient);
                    ctx.SaveChanges();
                    patient.AcceptChanges();
                    //ctx.Persons.AddOrUpdate(patient);
                    //ctx.SaveChanges();
                }
                res = true;
                return res;
            }
            catch (DbEntityValidationException vex)
            {
                var str = vex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Aggregate("", (current, er) => current + (er.PropertyName + ","));
                MessageBox.Show("Please Check the following fields before saving! - " + str);
                return res;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public List<TransactionBase> GetPatientTransactionList(Patient p)
        {
            using (var ctx = new RMSModel())
            {
                return
                    new List<TransactionBase>(
                        ctx.TransactionBase.OfType<Prescription>().Where(x => x.PatientId == p.Id).ToList());
            }
        }

        public List<TransactionBase> GetDoctorTransactionList(Doctor d)
        {
            using (var ctx = new RMSModel())
            {
                return
                    new List<TransactionBase>(
                        ctx.TransactionBase.OfType<Prescription>().Where(x => x.DoctorId == d.Id).ToList());
            }
        }


        public Doctor CreateNewDoctor(string searchtxt)
        {
            var d = CreateNewDoctor();
            d.StartTracking();
            SetNames(searchtxt, d);
            return d;
        }
        public Doctor CreateNewDoctor()
        {
            return new Doctor() { TrackingState = TrackingState.Added }; 
        }

        public bool SaveTransaction()
        {
            var res = SaveTransaction(TransactionData);
            NotifyPropertyChanged(x => x.TransactionData);
            return res;

        }

        public bool SaveTransaction(TransactionBase trans)
        {
            try
            {

                if (trans != null && trans.GetType() == typeof(Prescription))
                {
                    var p = trans as Prescription;
                    if (p.Doctor == null)
                    {
                        MessageBox.Show("Please Select a doctor");
                        return false;
                    }

                    if (p.Patient == null)
                    {
                        MessageBox.Show("Please Select a Patient");
                        return false;
                    }

                    //if (p.TransactionEntries.OfType<PrescriptionEntry>().Any(x =>
                    //    (x.RepeatQuantity.GetValueOrDefault() > 0 && x.Repeat.GetValueOrDefault() == 0) ||
                    //    (x.RepeatQuantity.GetValueOrDefault() == 0 && x.Repeat.GetValueOrDefault() > 0)))
                    //{
                    //    MessageBox.Show("Please ensure there is both a Repeat Quantity and Repeat or both == 0!");
                    //    return false;
                    //}

                }

                return SaveTransactionToDB(trans);
                
            }
           
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw;
            }
        }

        private bool SaveTransactionToDB(TransactionBase trans)
        {
            if (trans == null) return true;
            using (var ctx = new RMSModel())
            {
                try
                {
                    try
                    {

                    
                    var sql = "";
                    int res;
                    if (trans.TransactionId == 0)
                    {
                        sql += $@"  Insert into TransactionBase (StationId, ParentTransactionId, BatchId, CloseBatchId, Time, CustomerId, PharmacistId, CashierId, Comment, ReferenceNumber, StoreCode, OpenClose, Status)
                                    OUTPUT Inserted.TransactionId
                        VALUES        ({trans.StationId},'{trans.ParentTransactionId}',{trans.BatchId},{trans.CloseBatchId},'{trans.Time}','{trans.CustomerId}',{trans.PharmacistId},{trans.CashierId},'{trans.Comment?.Replace("'","''")}','{trans.ReferenceNumber}','{trans.StoreCode}',{trans.OpenClose},'{trans.Status?.Replace("'", "''")}')";
                        sql = CleanSql(sql);
                        res = ctx.Database.SqlQuery<int>(sql).FirstOrDefault();
                        sql = "";
                        trans.TransactionId = res;
                        if (trans is Prescription p)
                        {
                            sql = $@"  INSERT INTO TransactionBase_Prescription
                                                             (DoctorId, PatientId, TransactionId)
                                    VALUES        ({p.DoctorId},{p.PatientId},{p.TransactionId})";
                        }
                        else
                        {
                            sql = $@"  INSERT INTO TransactionBase_QuickPrescription
                                                             (TransactionId)
                                    VALUES        ({trans.TransactionId})";
                        }
                    }
                    else
                    {
                        sql += $@"  Update TransactionBase Set StationId = {trans.StationId}, ParentTransactionId = '{trans.ParentTransactionId}', BatchId = {trans.BatchId}, CloseBatchId = {trans.CloseBatchId}, Time = '{trans.Time}',
                                                            CustomerId = '{trans.CustomerId}', PharmacistId = '{trans.PharmacistId}', CashierId = '{trans.CashierId}', Comment = '{trans.Comment}',
                                                            ReferenceNumber = '{trans.ReferenceNumber}', StoreCode = '{trans.StoreCode}', OpenClose = '{trans.OpenClose}', Status = '{trans.Status}'
                                 Where TransactionId = {trans.TransactionId}";      

                       
                        if (trans is Prescription p)
                        {
                            sql += $@"  Update TransactionBase_Prescription Set DoctorId = {p.DoctorId}, PatientId = {p.PatientId}
                                    Where TransactionId = {trans.TransactionId}";

                            
                        }
                        
                    }
                    foreach (var trn in trans.TransactionEntries.OfType<PrescriptionEntry>())
                    {
                        if (trn.TransactionEntryId == 0)
                        {
                            var trnsql =
                                $@"  INSERT INTO TransactionEntryBase  (StoreID,  Price, TransactionId, Quantity, Taxable, Comment, TransactionTime, SalesTaxPercent, Discount, EntryNumber)
                                        output INSERTED.TransactionEntryId
                                        VALUES        ({trn.StoreID},{trn.Price},{trans.TransactionId},{trn.Quantity},{trn.Taxable},'{trn.Comment}','{DateTime.Now}',{trn.SalesTaxPercent},{trn.Discount},'{trn.EntryNumber}')";
                            trnsql = CleanSql(trnsql);
                            var trnres = ctx.Database.SqlQuery<int>(trnsql).FirstOrDefault();
                            trn.TransactionEntryId = trnres;
                            sql += $@"  INSERT INTO TransactionEntryBase_PrescriptionEntry
                                        (Dosage, ExpiryDate, TransactionEntryId, Repeat, RepeatQuantity)
                                        VALUES        ('{trn.Dosage.Replace("'", "''")}','{trn.ExpiryDate}',{trn.TransactionEntryId},'{trn.Repeat}','{trn.RepeatQuantity}')";

                            sql += $@"  INSERT INTO TransactionEntryItem
                                                                     (TransactionEntryId, QBItemListID, ItemNumber, ItemName, ItemId)
                                            VALUES        ({trn.TransactionEntryId},'{trn.TransactionEntryItem.QBItemListID}','{trn.TransactionEntryItem.ItemNumber}','{trn.TransactionEntryItem.ItemName.Replace("'", "''")}','{trn.TransactionEntryItem.ItemId}')";
                        }
                        else
                        {
                            sql += $@"  UPDATE       TransactionEntryBase
                                      SET                StoreID = {trn.StoreID}, Price = '{trn.Price}', Quantity = {trn.Quantity}, Taxable = '{trn.Taxable}', Comment = '{trn.Comment}', TransactionTime = '{DateTime.Now}', SalesTaxPercent = '{trn.SalesTaxPercent}', Discount = '{trn.Discount}', EntryNumber = '{trn.EntryNumber}'
                                        Where TransactionEntryId = {trn.TransactionEntryId}";

                            sql += $@"  UPDATE       TransactionEntryBase_PrescriptionEntry
                                            SET                Dosage = '{trn.Dosage.Replace("'", "''")}', ExpiryDate = '{trn.ExpiryDate}', Repeat = '{trn.Repeat}', RepeatQuantity = '{trn.RepeatQuantity}'
                                        Where TransactionEntryId = {trn.TransactionEntryId}";
                            sql += $@"  UPDATE       TransactionEntryItem
                                        SET                 QBItemListID = '{trn.TransactionEntryItem.QBItemListID}', ItemNumber = '{trn.TransactionEntryItem.ItemNumber}', ItemName = '{trn.TransactionEntryItem.ItemName.Replace("'", "''")}', ItemId = '{trn.TransactionEntryItem.ItemId}'
                                        Where TransactionEntryId = {trn.TransactionEntryId}";
                        }
                    }

                    sql = CleanSql(sql);

                    ctx.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,sql);
                        foreach (var t in trans.TransactionEntries)
                        {
                            t.TransactionId = trans.TransactionId;
                        }
                        TransactionData = trans;
                        ForceTransactionEntryNumberUpdate(TransactionData);


                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                catch (DbEntityValidationException vex)
                {
                    var str = vex.EntityValidationErrors.SelectMany(x => x.ValidationErrors)
                        .Aggregate("", (current, er) => current + (er.PropertyName + ","));
                    MessageBox.Show("Please Check the following fields before saving! - " + str);
                    return false;
                }
                catch (DbUpdateConcurrencyException dce)
                {
                    // Get failed entry
                    foreach (var itm in dce.Entries)
                    {
                        if (itm.State != EntityState.Added)
                        {
                            var dv = itm.GetDatabaseValues();
                            if (dv != null) itm.OriginalValues.SetValues(dv);
                        }
                    }
                    return false;
                }
                catch (Exception ex1)
                {
                    if (!ex1.Message.Contains("Object reference not set to an instance of an object")) throw;
                }

                // trans.TransactionId = trans.TransactionId;

                if (trans != null)
                {
                    var dbEntry = ctx.Entry(trans);

                    if (dbEntry != null && dbEntry.State != EntityState.Deleted)
                    {
                        if (trans.TransactionEntries != null)
                            ForceTransactionEntryNumberUpdate(trans);
                    }
                }
                return false;
            }
        }

        private static string CleanSql(string sql)
        {
            return sql.Replace("= ,", "= NULL,")
                .Replace("= ''", "= NULL")
                .Replace(",,", ",NULL,")
                .Replace(",False,", ",0,")
                .Replace(",True,", ",1,")
                .Replace(",''", ",NULL")
                ;
        }

        private void ForceTransactionEntryNumberUpdate(TransactionBase transactionBase)
        {
            if (transactionBase == null) return;
            foreach (var te in transactionBase.TransactionEntries.OfType<PrescriptionEntry>())
            {
                var t = te.TransactionEntryNumber;
                if (transactionBase is Prescription)
                {
                    var r = te.RepeatInfo ;
                    te.RepeatInfo = "";
                }
                
                te.TransactionEntryNumber = "";
                
            }
        }

        private void CleanTransactionNavProperties(TransactionBase titm, RMSModel ctx)
        {
            try
            {
                var itm = titm as Prescription;
                if (itm != null)
                {
                    var dbEntityEntry = ctx.Entry(itm.Doctor);
                    if (dbEntityEntry != null &&
                        (dbEntityEntry.State != EntityState.Unchanged && dbEntityEntry.State != EntityState.Detached))
                    {
                        dbEntityEntry.State = EntityState.Unchanged;
                    }
                    var p = ctx.Entry(itm.Patient);
                    if (p != null && (p.State != EntityState.Unchanged && p.State != EntityState.Detached))
                    {
                        p.State = EntityState.Unchanged;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }
        
        private static Batch batch;

        public Batch Batch
        {
            get { return batch; }
            set
            {
                if (batch != value)
                {
                    batch = value;
                    NotifyPropertyChanged(x => x.Batch);
                }
            }

        }

        private static Station station;

        public Station Station
        {
            get { return station; }
            set
            {
                if (station != value)
                {
                    station = value;
                    NotifyPropertyChanged(x => x.Station);
                }
            }

        }

        private static Store store;

        public Store Store
        {
            get { return store; }
            set
            {
                if (store != value)
                {
                    store = value;
                    NotifyPropertyChanged(x => x.Store);
                }
            }

        }



        internal Prescription NewPrescription()
        {
            try
            {
                var trn = new Prescription()
                {
                    StationId = Station.StationId,
                    BatchId = Batch.BatchId,
                    Time = DateTime.Now,
                    CashierId = _cashier.Id,
                    StoreCode = Store.StoreCode,
                    TrackingState = TrackingState.Added
                };
                trn.StartTracking();
                
                return trn;
            }
            catch (Exception ex)
            {
                Logger.Log(LoggingLevel.Error, GetCurrentMethodClass.GetCurrentMethod() + ": --- :" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }

        public bool ServerMode { get; set; }

        internal void SaveMedicine(Medicine medicine)
        {
            using (var ctx = new RMSModel())
            {
                ctx.ApplyChanges(medicine);
                ctx.SaveChanges();
                medicine.AcceptChanges();
            }
       
        }


        public void UpdateTransactionEntry(Exception exception, PrescriptionEntry prescriptionEntry)
        {
            var d = TransactionData.TransactionEntries.IndexOf(prescriptionEntry) + 1;
            MessageBox.Show(($"Could Not Print No:{d} Item-'{prescriptionEntry.TransactionEntryItem.ItemName}'"));
           
        }
    }
}
