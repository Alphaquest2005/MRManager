using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Core.Common.UI;
using DataGrid2Excel;
using MNIB_Distribution_Manager.Properties;


namespace MNIB_Distribution_Manager
{
    public class LabelViewModel : INotifyPropertyChanged
    {
        private static readonly LabelViewModel instance;
        static LabelViewModel()
        {
            instance = new LabelViewModel();
            
                
        }


        public static LabelViewModel Instance
        {
            get { return instance; }
        }

        public LabelViewModel()
        {
            Export = new Export() {ExportDate = DateTime.Today};
            ExportDetails = new ObservableCollection<ExportDetail>();
            ExportDate = DateTime.Today;
            using (var ctx = new MNIBDBDataContext())
            {
                Products = new ObservableCollection<Item>(ctx.Items);
                Customers = new ObservableCollection<Customer>(ctx.Customers);
                Harvesters = new ObservableCollection<Harvester>(ctx.Harvesters);
                Boxes = new ObservableCollection<Box>(ctx.Boxes);
            }
            ExportDetails.CollectionChanged += ExportDetailsOnCollectionChanged;
            InputBoxVisibility = Visibility.Collapsed;

            Customers.Add(new Customer() {CustomerName = "MNIB Production", CustomerAddress = "MNIB Production", CustomerNumber = "MNIBPro"});
            Customers.Add(new Customer() { CustomerName = "Pack House", CustomerAddress = "River Road, St. George's", CustomerNumber = "RRPH" });
            Customers.Add(new Customer() { CustomerName = "Grand Anse", CustomerAddress = "Grand Anse, St. George's", CustomerNumber = "VLGD" });
            Customers.Add(new Customer() { CustomerName = "MNIB SGU", CustomerAddress = "True Blue, St. George's", CustomerNumber = "MNIBSGU" });
            Customers.Add(new Customer() { CustomerName = "MNIB Street Sales", CustomerAddress = "MNIB Street Sales", CustomerNumber = "MNIBStr" });
        }

        private void ExportDetailsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
           OnPropertyChanged(nameof(TotalBoxWeight));
        }

        private string _transactionNumber;

        public string TransactionNumber
        {
            get { return _transactionNumber; }
            set
            {
                if (_transactionNumber != value)
                {
                    _transactionNumber = value;
                    OnPropertyChanged(nameof(TransactionNumber));
                    UpdateModel();
                    UpdateWeight();
                    SetExport(new MNIBDBDataContext());
                }
            }
        }

        private void UpdateModel()
        {
            if (string.IsNullOrEmpty(TransactionNumber)) return;
            using (var ctx = new MNIBDBDataContext())
            {
                switch (SourceTransaction)
                {
                    case "Sales Order":
                        var prn = ctx.ExportCustomers.FirstOrDefault(x => x.OrderNo == TransactionNumber);
                        if (prn == null)
                        {
                            MessageBox.Show("Sales Order Not Found! Check Number and Try again.");
                            return;
                        }
                        Instance.Customer = Instance.Customers.FirstOrDefault(x => x.CustomerNumber == prn.CustomerNumber);
                        break;
                    case "Invoice":
                        var trns = ctx.InvoiceCustomerLkps.FirstOrDefault(x => x.InvoiceNo == TransactionNumber);
                        if (trns == null)
                        {
                            MessageBox.Show("Invoice Not Found! Check Number and Try again.");
                            return;
                        }
                        Instance.Customer = Instance.Customers.FirstOrDefault(x => x.CustomerNumber == trns.CustomerNo);
                        break;
                    case "Transfer":
                        var trn = ctx.TransfersLkps.FirstOrDefault(x => x.TransferNo == TransactionNumber);
                        if (trn == null)
                        {
                            MessageBox.Show("Transfer Not Found! Check Number and Try again.");
                            return;
                        }
                        Instance.Customer = Instance.Customers.FirstOrDefault(x => x.CustomerNumber == trn.ToLocation);
                        break;
                    default:
                        break;

                }

                
            }
        }


        private Item product;

        public Item Product
        {
            get { return product; }
            set
            {
                if (product != value)
                {
                    product = value;
                    OnPropertyChanged(nameof(Product));
                    UpdateWeight();
                }
            }
        }

        private void UpdateWeight()
        {
            if (string.IsNullOrEmpty(TransactionNumber) || Product == null) return;
            using (var ctx = new MNIBDBDataContext())
            {
                var netWeight = ctx.TransactionNetWeightLkps.FirstOrDefault(x => x.LotNumber == Barcode);
                if (netWeight == null)
                {
                    MessageBox.Show("Transaction Net Weight Not Found! Check Number and Try again.");
                    return;
                }
                Instance.TotalWeight = netWeight.NetQuantity.GetValueOrDefault();
            }
        }

        private Customer customer;
        public Customer Customer
        {
            get { return customer; }
            set
            {
                if (customer != value)
                {
                    customer = value;
                    OnPropertyChanged(nameof(Customer));
                }
            }
        }

        private Box box;

        public Box Box
        {
            get { return box; }
            set
            {
                if (box != value)
                {
                    box = value;
                    OnPropertyChanged(nameof(Box));
                }

            }
        }

        private Harvester currentHarvester;

        public Harvester CurrentHarvester
        {
            get { return currentHarvester; }
            set
            {
                if (currentHarvester != value)
                {
                    currentHarvester = value;
                    OnPropertyChanged(nameof(CurrentHarvester));
                }

            }
        }

        private float weight;

        public float Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged(nameof(Weight));
                }

            }
        }
        private double total;

        public double TotalWeight
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged(nameof(TotalWeight));
                }

            }
        }

        

        public ObservableCollection<Box> Boxes { get; private set; }
        public ObservableCollection<Item> Products { get; private set; }
        public  ObservableCollection<Customer> Customers { get; private set; }
        public  ObservableCollection<Harvester> Harvesters { get; private set; }

        public ObservableCollection<ExportDetail> ExportDetails { get; private set; }

        public double TotalBoxWeight
        {
            get { return ExportDetails.Sum(x => x.Weight); }
        }



        public Export Export { get; set; }


        public bool VeifyBoxWeight()
        {
            if (Export?.ExportId == 0) return true;
            if (Export?.TotalWeight == 0) return true;
            if (Export?.TotalWeight != 0 && Export?.TotalWeight > TotalBoxWeight)
            {
               // throw new ApplicationException("Total Box Weight is Different to total Weight Received!");
                MessageBox.Show("Total Weight is less than Weight Harvested! Please verify!");
                return false;
            }
            return true;
        }

        RelayCommand _CreateExportDetailCmd;
        public ICommand CreateExportDetailCmd
        {
            get
            {
                if (_CreateExportDetailCmd == null)
                {
                    _CreateExportDetailCmd = new RelayCommand(CreateExportDetail, canCreateExportDetail);
                }
                return _CreateExportDetailCmd;
            }

        }

        private bool canCreateExportDetail()
        {
            return CurrentExportDetail == null;
        }

        public void CreateExportDetail()
        {
            if (CreateBoxChecks()) return;
            using (var ctx = new MNIBDBDataContext())
            {
                if (!CreateExport(ctx)) return ;
                
                if (CurrentExportDetail == null)
                {
                    var rd = UpdateExportDetail(new ExportDetail());
                    ctx.ExportDetails.InsertOnSubmit(rd);
                    ExportDetails.Insert(0,rd);
                    ctx.SubmitChanges();
                    Print(rd);
                }
                
            }
            ResetAfterEditorAdd();
        }

        private ExportDetail UpdateExportDetail(ExportDetail rd)
        {

            rd.ExportId = Export.ExportId;
            rd.ProductDescription = Product.ProductDescription;
            rd.LineNumber = ExportDetails.DefaultIfEmpty().Max(x =>  x?.LineNumber ?? 0) + 1;
            rd.Barcode = GetBarCode(rd);
            rd.BoxId = Box.BoxId;
            rd.TransactionNumber = TransactionNumber;
            //rd.TicketNo = Customer.TicketNo;
            //rd.OrderNo = Customer.OrderNo;
            rd.Weight = Weight - Box.Weight;
            rd.BoxWeight = Box.Weight;
            rd.CustomerInfo = Customer.Info;
            rd.ReceiptNumber = GetReceiptNumber(rd);
            return rd;

        }

        private string GetReceiptNumber(ExportDetail rd)
        {
            return SourceTransaction == "Sales Order"? CurrentHarvester.Intials + "-" + ExportDate.ToString("yyyyMMdd") + "-" + Export.ProductNumber + "-" + (rd.LineNumber).ToString()
                : TransactionNumber + "-" + Barcode + "-" + (rd.LineNumber).ToString();
        }

        private string GetBarCode(ExportDetail rd)
        {
            return ExportDetail.GetBarCode(GetReceiptNumber(rd));
            
        }

        private bool CreateBoxChecks()
        {
            if (Box == null)
            {
                MessageBox.Show("Please Specify Box.");
                return true;
            }
            if (Weight - Box.Weight <= 0)
            {
                MessageBox.Show("The Weight less the box is less than Zero");
                return true;
            }
            return false;
        }

        public bool SetExport(MNIBDBDataContext ctx)
        {
            ResetExport();

            if ( Product == null )
            {
               // MessageBox.Show("Please elect Product");
                return false;
            }
           

            List<ExportDetail> srdetails;
            if (SourceTransaction == "Sales Order")
            {
                if (CurrentHarvester == null)
                {
                    MessageBox.Show("Please Enter Harvester");
                    return false;
                }
                srdetails = ctx.ExportDetails.Where(x => x.ReceiptNumber.StartsWith(CurrentHarvester.Intials) &&
                                                         x.ReceiptNumber.Contains(Product.ProductId) &&
                                                         x.ReceiptNumber.Contains(ExportDate.ToString("yyyyMMdd"))).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(TransactionNumber))
                {
                   // MessageBox.Show("Please Enter ExportNumber");
                    return false;
                }
                srdetails = ctx.ExportDetails.Where(x => x.ReceiptNumber.StartsWith(TransactionNumber.ToString()) &&
                                                         x.ReceiptNumber.Contains(Product.ProductId) &&
                                                         x.ReceiptNumber.Contains(ExportDate.ToString("yyyyMMdd"))).ToList();
            }

            //if (sr.ExportId == Export?.ExportId) return true;
            if (srdetails.Any() )//
            {
                

               // if (!VeifyBoxWeight()) return false;
                
               //ctx.ExportDetails.Where(x => x.ExportId == sr.ExportId).OrderByDescending(x => x.ExportDetailId).ToList().ForEach(x => ExportDetails.Add(x));
                
                srdetails.ForEach(x => ExportDetails.Add(x));
                var sr = ctx.Exports.FirstOrDefault(x => x.ExportId == srdetails.First().ExportId);
                Export = sr;
                TotalWeight = (float) sr.TotalWeight;
                OnPropertyChanged(nameof(TotalWeight));
                return true;
            }

            return false;
        }

        public void ResetExport()
        {
            Export = null;
            ExportDetails.Clear();
            TotalWeight = 0;
            OnPropertyChanged(nameof(TotalWeight));
        }


        private bool CreateExport(MNIBDBDataContext ctx)
        {
            if (TotalWeight == 0)
            {
                MessageBox.Show("Please Enter Total Weight");
                return false;
            }
            if (SetExport(ctx)) return true;

            Export = new Export()
            {
                ExportDate = ExportDate,
                SourceTransaction = SourceTransaction,
                ProductNumber = Product.ProductId,
                ProductDescription = Product.ProductDescription,
                TotalWeight = TotalWeight
            };
            ctx.Exports.InsertOnSubmit(Export);
            ctx.SubmitChanges();
            
            return true;
        }

        public DateTime ExportDate { get; set; }

        public void Search()
        {
            try
            {

                using (var ctx = new MNIBDBDataContext())
                {

                    //Export =
                    //    ctx.Exports.FirstOrDefault(x => x.ExportId == ExportNumber);

                    //ExportDetails = ctx.ExportDetails.Where(x => x.ExportId == ExportNumber).ToList();

                    //foreach (var p in ExportDetails)
                    //{
                    //    p.LabelQty = Convert.ToInt16(p.Quantity/35);
                    //}

                    

                    OnPropertyChanged("Export");
                    OnPropertyChanged("ExportDetails");
                    OnPropertyChanged("PONumber");
                    if (Export == null)
                    {
                        MessageBox.Show("Export not found. Please try again");
                    }
                }
            }
            catch (SqlException se)
            {
               MessageBox.Show("Problem with the Database. Please contact your System Administrator");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Print(List<ExportDetail> plst)
        {
            foreach (var itm in plst)
            {
                Print(itm);
            }
        }

        public void Print(ExportDetail itm)
        {
            if (Export.SourceTransaction != "Sales Order") return;
            try
            {

           
            const int VerticalSpace = 150;
            const int HorizontalSpace = 100;
            const string LabelFontSize = "2.5";

            TSCLIB_DLL.openport(Settings.Default.TSCPrinter);                                           //Open specified printer driver
            TSCLIB_DLL.setup("101", "150", "6", "8", "0", "5", "0");                           //Setup the media size and sensor type info
            TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
            TSCLIB_DLL.downloadpcx("box.pcx", "box.pcx");                                         //Download PCX file into printer

                TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * .5).ToString(), "3", "0", "4", "4", "M.N.I.B.");
                TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 1.25).ToString(), "3", "0", "1.75", "1.75", "Young Street, St. George's, Grenada");

                TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 2.5).ToString(), "3", "0", LabelFontSize, LabelFontSize, Customer.CustomerName);
                TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 3).ToString(), "3", "0", "1.75", "1.75", Customer.CustomerAddress);


                TSCLIB_DLL.printerfont((HorizontalSpace * .5 ).ToString(), (VerticalSpace * 4).ToString(), "3", "0", "2", "2", DateTime.Today.ToString("yyyy-MMM-dd"));        //Drawing printer font

            
            TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 4.5).ToString(), "3", "0", LabelFontSize, LabelFontSize, itm.Barcode);

            
            TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 5).ToString(), "3", "0", LabelFontSize, LabelFontSize, Export.ProductDescription);

                TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 5.75).ToString(), "3", "0", "2", "2", Box.Description);

                TSCLIB_DLL.printerfont((HorizontalSpace * 3).ToString(), (VerticalSpace * 5.75).ToString(), "3", "0", "2", "2", itm.Weight.ToString());
                TSCLIB_DLL.printerfont((HorizontalSpace * 4).ToString(), (VerticalSpace * 5.75).ToString(), "3", "0", "2", "2", "LBS.");

                TSCLIB_DLL.barcode((HorizontalSpace * .5).ToString(), (VerticalSpace * 7).ToString(), "128", "125", "1", "0", "8", "8", itm.Barcode); //Drawing barcode

              //  TSCLIB_DLL.printerfont((HorizontalSpace * 2.3).ToString(), (VerticalSpace * 8.5).ToString(), "3", "0", "2", "2", Box.Description);

                //TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 4).ToString(), "3", "0", LabelFontSize, LabelFontSize, "SORT");
                //TSCLIB_DLL.sendcommand(string.Format("BOX {0},{1},{2},{3},4,19.2", HorizontalSpace * 4,VerticalSpace * 4,HorizontalSpace * 8,VerticalSpace * 4 + 100));

                //TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 5).ToString(), "3", "0", LabelFontSize, LabelFontSize, "GRADED");
                //TSCLIB_DLL.sendcommand(string.Format("BOX {0},{1},{2},{3},4,19.2", HorizontalSpace * 4, VerticalSpace * 5, HorizontalSpace * 8, VerticalSpace * 5 + 100));

                //TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 6).ToString(), "3", "0", LabelFontSize, LabelFontSize, "WASH");
                //TSCLIB_DLL.sendcommand(string.Format("BOX {0},{1},{2},{3},4,19.2", HorizontalSpace * 4, VerticalSpace * 6, HorizontalSpace * 8, VerticalSpace * 6 + 100));

                //TSCLIB_DLL.printerfont((HorizontalSpace * .5).ToString(), (VerticalSpace * 7).ToString(), "3", "0", LabelFontSize, LabelFontSize, "CHILLER#");
                //TSCLIB_DLL.sendcommand(string.Format("BOX {0},{1},{2},{3},4,19.2", HorizontalSpace * 4, VerticalSpace * 7, HorizontalSpace * 8, VerticalSpace * 7 + 100));


                TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
            TSCLIB_DLL.closeport();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private ExportDetail currentExportDetail = null;

        public ExportDetail CurrentExportDetail
        {
            get { return currentExportDetail; }
            set
            {
                if (currentExportDetail != value && value != null)
                {
                    currentExportDetail = value;
                    if (currentExportDetail != null)
                    {
                        Weight = (float) currentExportDetail.Weight;
                        Box = Boxes.FirstOrDefault(x => x.BoxId == CurrentExportDetail.BoxId);
                        OnPropertyChanged(nameof(Weight));
                        OnPropertyChanged(nameof(Box));
                    }

                    OnPropertyChanged(nameof(CurrentExportDetail));
                }
            }
        }

        public void DeleteLine()
        {
            if (CurrentExportDetail == null)
            {
                MessageBox.Show("Please Select Line to Delete!");
                return;
            }
            using (var ctx = new MNIBDBDataContext())
            {
               

                var r = ctx.ExportDetails.FirstOrDefault(x => x.ExportDetailId == CurrentExportDetail.ExportDetailId);
                ctx.ExportDetails.DeleteOnSubmit(r);
                ctx.SubmitChanges();
                ExportDetails.Remove(CurrentExportDetail);
                currentExportDetail = null;
                Weight = 0;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public bool ValidateUser(string password)
        {
            using (var ctx = new MNIBDBDataContext())
            {
                var u =
                    ctx.Harvesters.FirstOrDefault(
                        x => x.CanDelete.GetValueOrDefault() == true && x.Password.Equals(password));
                if (u == null)
                {
                    MessageBox.Show("Incorrect Password!");
                    return false;
                }
                return true;
            }
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        internal async Task<DataTable> GetExportReport(DateTime startDate, DateTime endDate)
        {
            // var eb = db.PayrollItems.AsEnumerable().GroupBy(b => new BranchSummary { BranchName = b.Name, PayrollItems = new ObservableCollection<DataLayer.PayrollItem>(( p => p.PayrollItems)), Total = b.Sum(p => p.NetAmount) }).AsEnumerable().Pivot(E => E.PayrollItems, E => E.PayrollJob.Branch.Name, E => E.Amount, true, TransformerClassGenerationEventHandler).ToList();
            try
            {

                var dt = new DataTable();
                //get Data for Date
                var xDetailsData = new List<ExportReportLine>();
                using (var ctx = new MNIBDBDataContext())
                {
                     xDetailsData =
                            ctx.ExportReportLines.Where(x => x.ExportDate >= startDate && x.ExportDate <= endDate.AddHours(23)).ToList();
                   
                }

                if (xDetailsData.Any() == false) return dt;


                dt.Columns.Add("ExportDate");
                dt.Columns.Add("ExportNumber");
                dt.Columns.Add("TransactionType");
                dt.Columns.Add("Harvester");
                dt.Columns.Add("ProductNumber");
                dt.Columns.Add("ProductDescription");
                dt.Columns.Add("LineNumber");
                dt.Columns.Add("Weight");


                var lst = xDetailsData.OrderBy(x => x.ExportDate).ThenBy(x => x.Harvester)
                                   .ThenBy(x => x.ProductNumber)
                                   .Select(x => new List<string>()
               {
                   x.ExportDate.ToString("dd-MMM-yyyy"),
                   x.ExportNumber,
                   x.SourceTransaction,
                   x.Harvester,
                   x.ProductNumber,
                   x.ProductDescription,
                   x.LineNumber.ToString(),
                   x.Weight.ToString("n1")
               });

                foreach (var itm in lst)
                {
                    dt.Rows.Add(itm.ToArray<string>());
                }


                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public async Task Send2Excel(DateTime startDate, DateTime endDate)
        {
            var dt = await GetExportReport(startDate, endDate).ConfigureAwait(false);
            var p = new ExportToExcel();
            p.GenerateReport(dt);
        }


        public DailySummary PrepareDailySummary()
        {
            using (var ctx = new MNIBDBDataContext())
            {
                var ds = new DailySummary();
                var res = ctx.ExportReportLines.Where(x => x.ExportDate == this.ExportDate)
                                                .OrderBy(x => x.CustomerInfo)
                                                .ThenBy(y => y.TransactionNumber)
                                                .ThenBy(z => z.Harvester)
                                                .ThenBy(z => z.ProductDescription)
                                                .Select(w => new DailySummary.ReportLine()
                                                {
                                                   TransactionType = w.SourceTransaction,
                                                   Customer = w.CustomerInfo,
                                                   TransactionNumber = w.TransactionNumber,
                                                   Harvester = w.Harvester,
                                                   Product = w.ProductDescription,
                                                   Weight = w.Weight,
                                                   BarCode = w.ExportNumber
                                                }).ToList();
                ds.Summary = new ObservableCollection<DailySummary.SummaryLine>(
                                                ctx.ExportReportLines.GroupBy(x => x.ReceiptNumber.Substring(0,x.ReceiptNumber.LastIndexOf("-")))
                                                  .Select(x => new DailySummary.SummaryLine() { LotNumber = x.Key, TotalWeight = x.Sum(y => y.Weight)} )
                                                  );

                ds.Lines = new ObservableCollection<DailySummary.ReportLine>(res);
                ds.TotalWeight = res.Sum(x => x.Weight);
                ds.TotalBoxes = res.Count;
                ds.ReportDate = ExportDate;
                return ds;
            }
        }

        RelayCommand _AmendExportDetailCmd;
        private string _barcode;
        private string _sourceTransaction;

        public ICommand AmendExportDetailCmd
        {
            get
            {
                if (_AmendExportDetailCmd == null)
                {
                    _AmendExportDetailCmd = new RelayCommand(ViewInputBox, canAmendExportDetail);
                }
                return _AmendExportDetailCmd;
            }

        }
        public Visibility InputBoxVisibility { get; set; }

        public string Barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;
                OnPropertyChanged(nameof(Barcode));
                //Todo: Do Lookup
                DoBarcodeLookup();

            }
        }

        public string SourceTransaction
        {
            get { return _sourceTransaction; }
            set
            {
                _sourceTransaction = value;
                OnPropertyChanged(nameof(SourceTransaction));
            }
        }

        private void DoBarcodeLookup()
        {
            if (string.IsNullOrEmpty(Barcode)) return;
            using (var ctx = new MNIBDBDataContext())
            {
                var itm = ctx.PurchaseOrderDetails.FirstOrDefault(x => x.LotNumber.Replace("-", "") == Barcode);
                if (itm != null)
                {
                   Instance.Product = Instance.Products.FirstOrDefault(x => x.ProductId == itm.ItemNumber);
                    
                }
                else
                {
                    MessageBox.Show("Barcode Transaction Not Found! Please try again");
                }
            }
        }


        private void ViewInputBox()
        {
            InputBoxVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(InputBoxVisibility));
        }

        private bool canAmendExportDetail()
        {
            return CurrentExportDetail != null;
        }

        public void AmendExportDetail()
        {
            if (CreateBoxChecks()) return;
            if (CurrentExportDetail == null)
            {
                MessageBox.Show("Please Select an Item.");
                return;
            }
            using(var ctx = new MNIBDBDataContext())
            {
                var rd = ctx.ExportDetails.FirstOrDefault(x => x.ExportDetailId == CurrentExportDetail.ExportDetailId);
                if (rd == null)
                {
                    MessageBox.Show("Item No Yet Saved!");
                    return;
                }
                rd = ctx.ExportDetails.FirstOrDefault(x => x.ExportDetailId == CurrentExportDetail.ExportDetailId);
                UpdateExportDetail(rd);
                OnPropertyChanged(nameof(TotalBoxWeight));
                CurrentExportDetail.Weight = Weight - Box.Weight;
                ctx.SubmitChanges();
                Print(rd);
            }
            ResetAfterEditorAdd();
        }

        private void ResetAfterEditorAdd()
        {
            currentExportDetail = null;
            Weight = 0;
            OnPropertyChanged(nameof(Weight));
            OnPropertyChanged(nameof(CurrentExportDetail));
            OnPropertyChanged(nameof(TotalBoxWeight));
        }

        public void SetCurrentExportDetailToNull()
        {
            currentExportDetail = null;
            OnPropertyChanged(nameof(CurrentExportDetail));
        }
    }

    public class DailySummary
    {
        public ObservableCollection<ReportLine> Lines { get; set; }
        public double TotalWeight { get; set; }
        public DateTime ReportDate { get; set; }
        public int TotalBoxes { get; set; }
        public ObservableCollection<SummaryLine> Summary { get; set; }

        public class ReportLine
        {
            public string TransactionType { get; set; }
            public string Customer { get; set; }
            public string TransactionNumber { get; set; }
            public string Harvester { get; set; }
            public string Product { get; set; }
            public double Weight { get; set; }
            public string BarCode { get; set; }
            
        }

        public class SummaryLine
        {
            public string LotNumber { get; set; }
            public double TotalWeight { get; set; }
        }
    }
}
