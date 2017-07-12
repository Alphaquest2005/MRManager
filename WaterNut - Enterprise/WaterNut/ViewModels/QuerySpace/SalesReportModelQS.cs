using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using System.Windows;
using System.Windows.Controls;
using AllocationQS.Client.Repositories;
using Core.Common.Converters;
using Core.Common.UI;
using CoreEntities.Client.Entities;
using SalesDataQS.Client.Repositories;
using SimpleMvvmToolkit;


namespace WaterNut.QuerySpace.AllocationQS.ViewModels
{
    public class SalesReportModel : ViewModelBase<SalesReportModel> //BaseViewModel
    {
        private static readonly SalesReportModel instance;

        static SalesReportModel()
        {
            instance = new SalesReportModel();
        }

        public static SalesReportModel Instance
        {
            get { return instance; }
        }

        private SalesReportModel()
        {
            RegisterToReceiveMessages<AsycudaDocument>(CoreEntities.MessageToken.CurrentAsycudaDocumentChanged,
                OnCurrentAsycudaDocumentChanged);
        }

        private async void OnCurrentAsycudaDocumentChanged(object sender, NotificationEventArgs<AsycudaDocument> e)
        {
            if (e.Data != null)
            {
                var sdata = await GetDocumentSalesReport(e.Data.ASYCUDA_Id).ConfigureAwait(false);
                SalesReportData = sdata == null
                    ? new ObservableCollection<SaleReportLine>()
                    : new ObservableCollection<SaleReportLine>(sdata);
            }
        }

        public static async Task<IEnumerable<SaleReportLine>> GetDocumentSalesReport(int ASYCUDA_Id)
        {
            try
            {


                using (var ctx = new AsycudaSalesAllocationsExRepository())
                {
                    var alst =
                        (await ctx.GetAsycudaSalesAllocationsExsByExpression(string.Format("xASYCUDA_Id == {0} " +
                                                                                           "&& EntryDataDetailsId != null " +
                                                                                           "&& PreviousItem_Id != null" +
                                                                                           "&& pRegistrationDate != null",
                            ASYCUDA_Id))
                            .ConfigureAwait(false)).ToList();
                    if (alst.Count <= 0) return null;
                    var d =
                        alst.Where(x => x.xLineNumber != null)
                            .OrderBy(s => s.xLineNumber)
                            .ThenBy(s => s.InvoiceNo)
                            .Select(s => new SaleReportLine
                            {
                                Line = Convert.ToInt32(s.xLineNumber),
                                Date = Convert.ToDateTime(s.InvoiceDate),
                                InvoiceNo = s.InvoiceNo,
                                CustomerName = s.CustomerName,
                                ItemNumber = s.ItemNumber,
                                ItemDescription = s.ItemDescription,
                                TariffCode = s.TariffCode,
                                Quantity = Convert.ToDouble(s.QtyAllocated),
                                Price = Convert.ToDouble(s.Cost),
                                SalesType = s.DutyFreePaid,
                                GrossSales = Convert.ToDouble(s.TotalValue),
                                PreviousCNumber = s.pCNumber,
                                PreviousLineNumber = s.pLineNumber.ToString(),
                                PreviousRegDate = Convert.ToDateTime(s.pRegistrationDate).ToShortDateString(),
                                CIFValue =
                                    (Convert.ToDouble(s.Total_CIF_itm)/Convert.ToDouble(s.pQuantity))*
                                    Convert.ToDouble(s.QtyAllocated),
                                DutyLiablity =
                                    (Convert.ToDouble(s.DutyLiability)/Convert.ToDouble(s.pQuantity))*
                                    Convert.ToDouble(s.QtyAllocated)
                            }).Distinct();


                  
                    return new ObservableCollection<SaleReportLine>(d);
                }
               
            }
            catch (Exception Ex)
            {

            }
            return null;
        }


        private ObservableCollection<SaleReportLine> _salesReportData = new ObservableCollection<SaleReportLine>();

        public ObservableCollection<SaleReportLine> SalesReportData
        {
            get { return _salesReportData; }
            set
            {
                _salesReportData = value;
                NotifyPropertyChanged(x => SalesReportData);
            }
        }

        public class SaleReportLine
        {
            public int Line { get; set; }
            public DateTime Date { get; set; }
            public string InvoiceNo { get; set; }
            public string CustomerName { get; set; }
            public string ItemNumber { get; set; }
            public string ItemDescription { get; set; }
            public string TariffCode { get; set; }
            public double Quantity { get; set; }
            public double Price { get; set; }
            public string SalesType { get; set; }
            public double GrossSales { get; set; }
            public string PreviousCNumber { get; set; }
            public string PreviousLineNumber { get; set; }
            public string PreviousRegDate { get; set; }
            public double CIFValue { get; set; }
            public double DutyLiablity { get; set; }
        }


        internal async Task Send2Excel(string path, DataGrid GridData)
        {
            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {

                await Task.Factory.StartNew(() =>
                {
                    var s = new ExportToExcel<SaleReportLine, List<SaleReportLine>>();
                    s.StartUp();
                    
                        try
                        {
                            var data = GridData.ItemsSource.OfType<SaleReportLine>();
                            if (data != null)
                            {
                                
                                s.dataToPrint = data.ToList();
                                s.SaveReport(path);
                            }
                            
                            StatusModel.StatusUpdate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                   
                    s.ShutDown();
                },
                    CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
        }

        internal async Task ExportDocSetSalesReport(string folder)
        {
            var doclst =
                await
                    SalesDataRepository.Instance.GetSalesDocuments(
                        CoreEntities.ViewModels.BaseViewModel.Instance.CurrentAsycudaDocumentSetEx.AsycudaDocumentSetId)
                        .ConfigureAwait(false);
            StatusModel.StartStatusUpdate("Exporting Files", doclst.Count());

            var exceptions = new ConcurrentQueue<Exception>();

            using (var sta = new StaTaskScheduler(numberOfThreads: 1))
            {

                await Task.Factory.StartNew(() =>
                {
                    var s = new ExportToExcel<SaleReportLine, List<SaleReportLine>>();
                    s.StartUp();
                    foreach (var doc in doclst)
                    {
                        try
                        {
                            var data = GetDocumentSalesReport(doc.ASYCUDA_Id).Result;
                            if (data != null)
                            {
                                string path = Path.Combine(folder,
                                    !string.IsNullOrEmpty(doc.CNumber) ? doc.CNumber : doc.ReferenceNumber + ".xls");
                                s.dataToPrint = data.ToList();
                                s.SaveReport(path);
                            }
                            else
                            {
                                File.Create(Path.Combine(folder, doc.CNumber ?? doc.ReferenceNumber + ".xls"));
                            }
                            StatusModel.StatusUpdate();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    }
                    s.ShutDown();
                },
                    CancellationToken.None, TaskCreationOptions.None, sta).ConfigureAwait(false);
            }
            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
    }
}