using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using PrismMVVMLibrary;
using RMSDataAccessLayer;
using System.ComponentModel;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Windows.Data;
using System.Printing;
using SUT.PrintEngine.Utils;
using System.Windows.Media;
using QuickBooks;

using SimpleMvvmToolkit;

//using System.Data.Entity.Validation;
//using System.Data.Objects.DataClasses;



namespace SalesRegion
{
    public class SalesVM : ViewModelBase
    {
        IUnityContainer container;
        IEventAggregator eventAggregator;
        IRegionManager regionManager;

        private static readonly SalesVM _instance;
        static SalesVM()
        {
            _instance = new SalesVM(new UnityContainer(), new EventAggregator(), new RegionManager());
        }

        public static SalesVM Instance
        {
            get { return _instance; }
        }


       
       // Batch batch;
       // Station station;
        static Cashier _cashier;
        public Cashier CashierEx
        {
            get
            {
                return _cashier;
            }
            set
            {
                if (_cashier != value)
                {
                    _cashier = value;
                    RaisePropertyChanged(() => CashierEx);
                }
            }

        }

        

        
        public SalesVM(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.container = container;
            this.eventAggregator = eventAggregator;
            
            StartUp();
         
        }

        
        private void StartUp()
        {
            SetUp();
            //UpdateSearchList();
            UpdateQBItems();
        }

        private void SetUp()
        {
            using (var ctx = new RMSModel())
            {
                //CashierEx = ctx.Persons.OfType<Cashier>().FirstOrDefault(x => x.Id == CashierEx.Id);

                //Station = ctx.Stations.FirstOrDefault(s => s.MachineName == Environment.MachineName);

                //Batch = ctx.Batches.FirstOrDefault(b => b.StationId == Station.StationId && b.Status == "Open");

                //this.Store = ctx.Stores.FirstOrDefault(x => x.StoreId == Station.StoreId);
            }
        }

        
        public void CloseTransaction() 
        {
            if (transactionData != null)
            {
                TransactionData = TransactionData.With(null, null, Batch.BatchId, null, null, null, null, null, null,
                    false,null, null);
                SaveTransaction();
                TransactionData = null;
            }
        }

        
        public void CreateNewPrescription() 
        {
            if (doctor != null)
            {
                this.Status = "Doctor is Missing";
                return;
            }

            if (patient != null)
            {
                 this.Status = "Patient is Missing";
                return;
            }

        //     private void ApplyPatientDiscount(TransactionEntryBase trn = null)
        //{
        //    if (typeof(Prescription).IsInstanceOfType(TransactionData) && ((Prescription)TransactionData).Patient != null)
        //    {
        //        var p = ((Prescription)TransactionData).Patient;

        //        if (trn == null)
        //        {
        //            foreach (var te in TransactionData.TransactionEntries)
        //            {
        //                te.Discount = Convert.ToDecimal(p.Discount);
        //            }
        //        }
        //        else
        //        {
        //            trn.Discount = Convert.ToDecimal(p.Discount);
        //        }

        //    }
        //}

            //if (TransactionData.CurrentTransactionEntry == null)
            //    TransactionData.CurrentTransactionEntry.ItemId = itm.ItemId;
            //TransactionData.CurrentTransactionEntry.Price = itm.Price;
            //if (typeof(Medicine).IsInstanceOfType(itm))
            //{
            //    Medicine med = itm as Medicine;
            //    if (med.QBInventoryItem != null)
            //    {
            //        if (med.QBInventoryItem.TaxCode.ToUpper() == "VAT")
            //        {
            //            TransactionData.CurrentTransactionEntry.SalesTaxPercent = (Decimal).15;
            //            TransactionData.CurrentTransactionEntry.Taxable = true;
            //        }
            //        else
            //        {
            //            TransactionData.CurrentTransactionEntry.SalesTaxPercent = 0;
            //            TransactionData.CurrentTransactionEntry.Taxable = false;
            //        }
            //    }
            //    else
            //    {
            //        if (itm.SalesTax == null)
            //        {
            //            TransactionData.CurrentTransactionEntry.SalesTaxPercent = (Decimal).15;
            //        }
            //        else
            //        {
            //            TransactionData.CurrentTransactionEntry.SalesTaxPercent = (Decimal)itm.SalesTax;
            //        }
            //    }
            //}
            //else
            //{
            //    TransactionData.CurrentTransactionEntry.SalesTaxPercent = (Decimal)itm.SalesTax; //(Decimal).15;
            //}

            Prescription txn = new Prescription(Station.StationId, Batch.BatchId, null, DateTime.Now, null, CashierEx.Id, null, null, Store.StoreCode, true, (CashierEx.Role == "Pharmacist" ? CashierEx.Id : null as int?),null, doctor.Id, patient.Id);
                   
        }

     
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
                    RaisePropertyChanged(() => Doctor);
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
                    RaisePropertyChanged(() => Patient);
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
                    RaisePropertyChanged(() => Status);
                }
            }
        }

        public RMSDataAccessLayer.TransactionBase transactionData;
        public RMSDataAccessLayer.TransactionBase TransactionData
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
        
            if (transactionData != null && this.regionManager != null && this.regionManager.Regions["HeaderRegion"] != null)
            {
                this.regionManager.Regions["HeaderRegion"].Context = transactionData;

            }
            RaisePropertyChanged(() => TransactionData);

        }


        private ListCollectionView _qbitems;
        
        public ListCollectionView QBItems
        {
            get { return _qbitems; }

        }

        
        public void UpdateQBItems()
        {
            using (var ctx = new RMSModel())
            {
                _qbitems =
                    new ListCollectionView(ctx.QBInventoryItems.OrderBy(itm => itm.ItemName).ToList<QBInventoryItem>());
                RaisePropertyChanged("QBItems");
            }
        }

        private  ObservableCollection<object> _csv;
        
        public ObservableCollection<object> SearchList
        {
            get { return _csv; }

        }

        public ObservableCollection<Cashier> _pharmacists = null;
        
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
                    }
                }
                return _pharmacists;
            }
        }
 

        public void UpdateSearchList(string filterText)
        {
            CompositeCollection cc = CreateSearchList(filterText);


            _csv = new ObservableCollection<Object>();
            foreach (var item in cc)
            {
                _csv.Add(item);
            }
            RaisePropertyChanged(() => SearchList);
            
        }

        public void GetSearchResults(string filterText)
        {
            UpdateSearchList(filterText);
        }

        
      
        private CompositeCollection CreateSearchList(string filterText)
        {
            //todo: make parallel

            CompositeCollection cc = new CompositeCollection();

           cc.Add(AddSearchItems());
           
            double t = 0;
            if (double.TryParse(filterText, out t))
            {
                AddTransaction(cc, filterText);
            }

            GetPatients(cc, filterText);
            GetDoctors(cc, filterText);

            AddInventory(cc, filterText);

            return cc;
        }


        private CompositeCollection AddSearchItems()
        {
            CompositeCollection cc = new CompositeCollection();
            //SearchItem b = new SearchItem();
            //b.SearchObject = new RMSDataAccessLayer.Transactionlist();
            //b.SearchCriteria = "Transaction History";
            //b.DisplayName = "Transaction History";
            //cc.Add(b);

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



        
        
        private void AddTransaction(CompositeCollection cc, string filterText)
        {
            try
            {
                using (var ctx = new RMSModel())
                {
                    // right now any prescriptions
                    foreach (
                        var trns in
                            ctx.TransactionBase.OfType<Prescription>()
                                .Where(x => x.TransactionNumber.Contains(filterText))
                                .OrderBy(t => t.Time)
                                .Take(100))
                    {
                        cc.Add(trns);
                    }
                }
                using (var ctx = new RMSModel())
                {
                    foreach (
                        var trns in
                            ctx.TransactionBase.OfType<QuickPrescription>()
                                .Where(x => x.TransactionNumber.Contains(filterText))
                                .OrderBy(t => t.Time)
                                .Take(100))
                    {
                        cc.Add(trns);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
       
        
        

        private void GetDoctors(CompositeCollection cc, string filterText)
        {
            using (var ctx = new RMSModel())
            {
                foreach (
                    var cus in
                        ctx.Persons.OfType<Doctor>()
                            .Where(
                                x =>
                                    ("Dr. " + " " + x.FirstName.Replace("Dr", "").Replace("Dr.", "") + " " + x.LastName +
                                     " " + x.Code).Contains(filterText))
                            .Take(listCount))
                {
                    cc.Add(cus);
                }
            }
        }

        private void GetPatients(CompositeCollection cc, string filterText)
        {
            using (var ctx = new RMSModel())
            {
                foreach (
                    var cus in
                        ctx.Persons.OfType<Patient>()
                            .Where(x => (x.FirstName + " " + x.LastName).Contains(filterText))
                            .Take(listCount)) //
                {
                    cc.Add(cus);
                }
            }
        }


        private  bool _showInactiveItems = false;
        private int listCount = 25;
        
        
        public bool ShowInactiveItems
        {
            get
            {
                return _showInactiveItems;
            }
            set
            {
                _showInactiveItems = value;
                RaisePropertyChanged(() => ShowInactiveItems);
            }

        }

      
        private void AddInventory(CompositeCollection cc, string filterText)
        {
            try
            {
                //todo: make parallel
                using (var ctx = new RMSModel())
                {
                 
                   var itms  = ctx.Item.OfType<Medicine>().Where(x => x.ItemName.Contains(filterText)
                                                                      && x.QBItemListID != null
                                                                      &&
                                                                      (x.Inactive == null ||
                                                                       (x.Inactive != null &&
                                                                        x.Inactive == _showInactiveItems))).Take(listCount)
                           .AsEnumerable()
                            .OrderBy(x => x.DisplayName).ToList(); 

                    foreach (var itm in itms)
                    {
                        cc.Add(itm);
                    }
                }

                using (var ctx = new RMSModel())
                {
                    foreach (
                        var itm in ctx.Item.OfType<StockItem>().Where(x => x.ItemName.Contains(filterText)).Take(listCount))
                    {
                        cc.Add(itm);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

       

        
        public void ProcessSearchListItem(object SearchItem)
        {
            if (SearchItem != null)
            {
                if (typeof (RMSDataAccessLayer.SearchItem) == SearchItem.GetType())
                {
                    DoSearchItem(SearchItem as RMSDataAccessLayer.SearchItem);
                }

                if (typeof (RMSDataAccessLayer.Doctor) == SearchItem.GetType())
                {
                    AddDoctorToTransaction(SearchItem as Doctor);
                }

                if (typeof (RMSDataAccessLayer.Patient) == SearchItem.GetType())
                {
                    AddPatientToTransaction(SearchItem as Patient);
                }

                if (SearchItem is Item)
                {
                    if (Status == null)
                    {
                        InsertItemTransactionEntry((Item) SearchItem);
                    }
                    NewItemTransaction(SearchItem as Item);
                }
                if (SearchItem is TransactionBase)
                {
                    GoToTransaction((TransactionBase) SearchItem);
                }

            }
        }

        private void DoSearchItem(SearchItem searchItem)
        {
            throw new NotImplementedException();
        }

        
        private void AddPatientToTransaction(Patient patient)
        {
            Patient = patient;
          }

     
        
        private void AddDoctorToTransaction(Doctor doctor)
        {
            Doctor = doctor;
        }

        
        private void GoToTransaction(TransactionBase trn)
        {
            TransactionData = trn;
        }

        
        public void GoToTransaction(int TransactionId)
        {
            using (var ctx = new RMSModel())
            {
                TransactionBase ntrn;
                ntrn = (from t in ctx.TransactionBase
                            .Include(x => x.TransactionEntries)
                            .Include("Doctor")
                            .Include("TransactionEntries.Item")
                    where t.TransactionId == TransactionId
                    orderby t.Time descending
                    select t).FirstOrDefault();
               TransactionData = ntrn;
            }
        }

        
        public void GoToPreviousTransaction()
        {
            using (var ctx = new RMSModel())
            {
                TransactionBase ptrn;
                
                if (TransactionData == null)
                {
                    ptrn = (from t in ctx.TransactionBase.Include(x => x.TransactionEntries).Include("TransactionEntries.Item").Include("Doctor")
                        orderby t.Time descending
                        select t).FirstOrDefault();
                }
                else
                {
                    if (TransactionData.TransactionId != 0)
                    {
                        ptrn = ctx.TransactionBase.OrderByDescending(t => t.Time).Include(x => x.TransactionEntries).Include("TransactionEntries.Item").Include("Doctor")
                                                  .FirstOrDefault(t => t.TransactionId < TransactionData.TransactionId);
                    }
                    else
                    {
                        ptrn = (from t in ctx.TransactionBase.Include(x => x.TransactionEntries).Include("TransactionEntries.Item").Include("Doctor")
                            orderby t.Time descending
                            select t).FirstOrDefault();
                    }
                }
                if (ptrn != null)
                {
                    TransactionData = ptrn;
                }
                else
                {
                    MessageBox.Show("No previous transaction");
                }
            }
        }

      
        
        private void InsertItemTransactionEntry(RMSDataAccessLayer.Item itm)
        {
            if (this.CurrentTransactionEntry == null)
            {
                this.Item = itm;
                return;
            }
        }

        
        private bool AutoCreateOldTransactions()
        {
            if (TransactionData == null) return false;
            if (TransactionData.Time.Date != DateTime.Now.Date)
            {
                var res = MessageBox.Show("Modifying old transactions is not allowed! Do you want to create a New Transaction?", "Can't Modify Old Transaction", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                   TransactionData = CopyCurrentTransaction();
                    return true;
                }
                if (res == MessageBoxResult.No) return false;
            }
            return true;
        }

        
        public void DeleteTransactionEntry<T>(TransactionEntryBase dtrn) where T : TransactionEntryBase
        {
            try
            {
                if (dtrn == null) return;
                if (AutoCreateOldTransactions() == false) return;

                using (var ctx = new RMSModel())
                {
                    var d = ctx.TransactionEntryBase.FirstOrDefault(x => x.TransactionEntryId == dtrn.TransactionEntryId);
                    this.TransactionEntries.Remove(d);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public void DeleteCurrentTransaction()
        {
            if (MessageBox.Show("Are you sure you want to delete?", "Delete Current Transaction", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                if (TransactionData.Time.Date != DateTime.Now.Date)
                {
                     MessageBox.Show("Modifying old transactions is not allowed!",
                        "Can't Modify Old Transaction");
                    return;
                }
                using (var ctx = new RMSModel())
                {

                    var t = ctx.TransactionBase.FirstOrDefault(x => x.TransactionId == TransactionData.TransactionId);
                    ctx.TransactionBase.Remove(t);
                    ctx.SaveChanges();
                    GoToPreviousTransaction();
                }
            }

        }

        
        public TransactionBase CopyCurrentTransaction()
        {
            dynamic newt = null;
            if (TransactionData == null)
            {
                MessageBox.Show("Please select transaction again to continue...");
                return null;
            }
            if (TransactionData.GetType() == typeof(Prescription))
            {
               // newt = Common.CopyEntity<Prescription>(rms, TransactionData as Prescription, false);
            }

            if (TransactionData.GetType() == typeof(QuickPrescription))
            {
               // newt = Common.CopyEntity<QuickPrescription>(rms, TransactionData as QuickPrescription, false);
            }
            //newt.TransactionNumber = CreateTxnNumber(newt);
            newt.ReferenceNumber = "";
            newt.Time = DateTime.Now;
            newt.Cashier = _cashier;
            if (_cashier.Role == "Pharmacist")
                newt.Pharmacist = _cashier;

            foreach (PrescriptionEntry item in TransactionEntries)
            {
              //  newt.TransactionEntries.Add((TransactionEntryBase)Common.CopyEntity<PrescriptionEntry>(rms, item, false));

            }

            
            //rms.TransactionBase.AddObject(newt);
            return newt;
        }

        
        public void CopyPrescription()
        {
            //TransactionBase newt = CopyCurrentTransaction();
            //foreach (var item in newt.TransactionEntries.ToList())
            //{
            //    newt.TransactionEntries.Remove(item);
            //    rms.DeleteObject(item);
            //}
            //TransactionData = newt;
            //SaveTransaction();
        }

        
        public void AutoRepeat()
        {
            //TransactionBase newt = CopyCurrentTransaction();
            //foreach (PrescriptionEntry item in newt.TransactionEntries.ToList())
            //{
            //    if (item.Repeat == 0)
            //    {
            //        //newt.TransactionEntries.Remove(item);
            //        // rms.Detach(item);
            //    }
            //    else
            //    {
            //        item.Repeat -= 1;
            //    }
            //}



            //// rms.TransactionBase.AddObject(newt);
            //TransactionData = newt;
            //SaveTransaction();

        }

        
        private void NewItemTransaction(Item SearchItem)
        {
            if (TransactionData == null)
            {
                CreateNewQuickPrescription();
            }
            InsertItemTransactionEntry(SearchItem as Item);
        }

        private void CreateNewQuickPrescription()
        {
            throw new NotImplementedException();
        }

        
        public void Print(ref Grid fwe)
        {
            FrameworkElement f = fwe;
            Print(ref f);
        }

        
        public void Print(ref FrameworkElement fwe)
        {
            if (TransactionData == null) return;
            //LocalPrintServer printserver = new LocalPrintServer();
            PrintServer printserver = new PrintServer(Station.PrintServer);


            Size visualSize;
           
            visualSize = new Size(288, 2 * 96);// paper size
           
            DrawingVisual visual = PrintControlFactory.CreateDrawingVisual(fwe, fwe.ActualWidth, fwe.ActualHeight);


            SUT.PrintEngine.Paginators.VisualPaginator page = new SUT.PrintEngine.Paginators.VisualPaginator(visual, visualSize, new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 0));
            page.Initialize(false);

            PrintDialog pd = new PrintDialog();
            pd.PrintQueue = printserver.GetPrintQueue(Station.ReceiptPrinterName);

            //  pd.PrintQueue = printserver.GetPrintQueue("TSC TDP-244");
            //  pd.PrintQueue = printServer.GetPrintQueues(new [] {EnumeratedPrintQueueTypes.Shared} );

            //if (pd.ShowDialog()==true)
            //{

            pd.PrintDocument(page, "");
            //}
        }

        
        public void PostQBSale()
        {
            if (TransactionData == null || string.IsNullOrEmpty(TransactionData.TransactionNumber))
            {
                MessageBox.Show("Invalid Transaction Please Try again");
                return;
            }

           
            TransactionData = (TransactionData as Prescription).With(null,null,null,null,null,null,null,null,null,null,null,"ToBePosted", null,null);
            

            SalesReceipt s = new SalesReceipt();
            s.TxnDate = TransactionData.Time;
            s.TxnState = "1";
            s.Workstation = "02";
            s.StoreNumber = "1";
            s.SalesReceiptNumber = "123";
            s.Discount = "0";

            if (TransactionData == null || string.IsNullOrEmpty(TransactionData.TransactionNumber))
            {
                MessageBox.Show("Invalid Transaction Please Try again");
                return;
            }

            TransPreZeroConverter tz = new TransPreZeroConverter();

            if (typeof(Prescription).IsInstanceOfType(TransactionData))
            {
                Prescription p = transactionData as Prescription;
                string doctor = "";
                string patient = "";
                if (Doctor != null)
                {
                    doctor = Doctor.DisplayName;
                }
                if (Patient != null)
                {
                    patient = Patient.ContactInfo;
                    s.Discount = Patient.Discount == null ? "" : Patient.Discount.ToString();
                }
                s.Comments = String.Format("{0} \n RX#:{1} \n Doctor:{2}", patient, tz.Convert(p.TransactionNumber, typeof(string), null, null), doctor);
            }
            else
            {
                s.Comments = "RX#:" + tz.Convert(TransactionData.TransactionNumber, typeof(string), null, null);
            }

            if (TransactionData == null || string.IsNullOrEmpty(TransactionData.TransactionNumber))
            {
               if (TransactionData == null || string.IsNullOrEmpty(TransactionData.TransactionNumber))
                {
                    MessageBox.Show("Invalid Transaction Please Try again");
                    return;
                }
            }

            if (TransactionData != null)
            {
                s.TrackingNumber = tz.Convert(TransactionData.TransactionNumber, typeof(string), null, null).ToString();
            }
            s.Associate = "Dispensary";
            s.SalesReceiptType = "0";



            foreach (var item in TransactionEntries)
            {
                if (item.Item.QBItemListID != null)
                {

                    s.SalesReceiptDetails.Add(new SalesReceiptDetail { ItemListID = item.Item.QBItemListID, QtySold = item.Quantity });//340 
                }
                else
                {
                    MessageBox.Show("Please Link Quickbooks item to dispensary");
                    return;
                }
            }


            //QBPOS qb = new QBPOS();
            //SalesReceiptRet result = QBPOS.Instance.AddSalesReceipt(s);
            //if (result != null)
            //{
            //    transactionData.ReferenceNumber = "QB#" + result.SalesReceiptNumber;
            //}
        }


        public void Notify(string token, object sender, NotificationEventArgs e)
        {
            MessageBus.Default.Notify(token, sender, e);
        }



      

        
        public Item Item { get; set; }

        public TransactionEntryBase CurrentTransactionEntry { get; set; }

        public ImmutableList<TransactionEntryBase> TransactionEntries { get; set; }

        public ImmutableList<TransactionsView> TransactionList { get; set; }

       
        public  Patient CreateNewPatient()
        {
            return new Patient(null,null,null,null,null,null,null,null,null,null,null,null);
        }

        public void SavePatient(Patient content)
        {
            using (var ctx = new RMSModel())
            {
                ctx.Persons.Attach(content);
                ctx.SaveChanges();
            }
        }

        public List<Prescription> GetPatientTransactionList(Patient p)
        {
            using (var ctx = new RMSModel())
            {
                return ctx.TransactionBase.OfType<Prescription>().Where(x => x.PatientId == p.Id).ToList();
            }
        }

        public List<Prescription> GetDoctorTransactionList(Doctor d)
        {
            using (var ctx = new RMSModel())
            {
                return ctx.TransactionBase.OfType<Prescription>().Where(x => x.DoctorId == d.Id).ToList();
            }
        }

        public Doctor CreateNewDoctor()
        {
            return new Doctor(null, null, null, null, null, null, null, null, null,null);
        }

        public void SaveTransaction()
        {
            using (var ctx = new RMSModel())
            {
                ctx.TransactionBase.Attach(TransactionData);
                ctx.SaveChanges();
            }
        }

        
        static Batch batch;
        public Batch Batch
        {
            get
            {
                return batch;
            }
            set
            {
                if (batch != value)
                {
                    batch = value;
                    RaisePropertyChanged(() => Batch);
                }
            }

        }
        
        static Station station;
        public Station Station
        {
            get
            {
                return station;
            }
            set
            {
                if (station != value)
                {
                    station = value;
                    RaisePropertyChanged(() => Station);
                }
            }

        }

        
        static Store store;
        public Store Store
        {
            get
            {
                return store;
            }
            set
            {
                if (store != value)
                {
                    store = value;
                    RaisePropertyChanged(() => Store);
                }
            }

        }
    }
}
