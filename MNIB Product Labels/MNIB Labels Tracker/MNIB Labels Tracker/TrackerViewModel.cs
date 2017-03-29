using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;


namespace MNIB_Labels_Tracker
{
   public class TrackerViewModel :INotifyPropertyChanged
    {
       private static readonly TrackerViewModel instance;
        static TrackerViewModel()
        {
            instance = new TrackerViewModel();
            
        }

        public static TrackerViewModel Instance
        {
            get { return instance; }
        }

        public TrackerViewModel()
        {
            using (var ctx = new TrackerDBDataContext())
            {
                ctx.CommandTimeout = 0;
                Locations = new List<Location>() { new Location() { LocationCode = "All", Description = "All" } };
                Locations.AddRange(ctx.Locations.ToList());
                CurrentLocation = Locations.FirstOrDefault();
            }
            StartPODate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
            EndPODate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.DaysInMonth(DateTime.Now.Date.Year, DateTime.Now.Date.Month)); 
        }
        public DateTime StartPODate { get; set; }
        public DateTime EndPODate { get; set; }
        public string LocationId { get; set; }
        public string LotNumber { get; set; }
       private string itemDescription = null;

       public string ItemDescription
       {
           get
           {
               
               return itemDescription ?? "";
           }
           set { itemDescription = value;}
       }

       public List<Location> Locations { get; set; }
        public Location CurrentLocation { get; set; }

        internal async Task<DataTable> GetPOSummary()
        {
            
            try
            {
               
                var dt = new DataTable();
                //get Data for Date
                var poDetailsData = new List<PurchaseOrderDetail>();
                using (var ctx = new TrackerDBDataContext())
                {
                    ctx.CommandTimeout = 0;
                    if (CurrentLocation.LocationCode == "All")
                    {
                        poDetailsData =
                            ctx.PurchaseOrderDetails.Where(x => (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23) && x.Quantity > 0) 
                                && x.ItemDescription.Contains(ItemDescription)).ToList();
                    }
                    else
                    {
                        poDetailsData =
                            ctx.PurchaseOrderDetails.Where(x => x.PurchaseOrderDate >= StartPODate
                                                        && x.PurchaseOrderDate <= EndPODate.AddHours(23) 
                                                        && x.LocationCode == CurrentLocation.LocationCode 
                                                        && x.ItemDescription.Contains(ItemDescription)).ToList();
                    }
                }

                if (poDetailsData.Any() == false) return dt;

                var d = (from f in poDetailsData
                         group f by new { f.ItemDescription, f.Unit}
                             into myGroup
                             where myGroup.Any()
                             select new
                             {
                                 myGroup.Key.ItemDescription,
                                 Unit = myGroup.Key.Unit,
                                 Total = myGroup.Sum(x => x.Quantity).ToString("n1"),
                                 Location = myGroup.GroupBy(f => f.LocationCode).Select
                                 (m => new { Location = m.Key, Quantity = m.Sum(c => c.Quantity) })
                             }).ToList();

                var locations = poDetailsData.Select(x => x.LocationCode).Distinct().ToList();

                //Creating array for adding dynamic columns
                var objDataColumn = new ArrayList();

                if (poDetailsData.Any())
                {
                    //Three column are fix "rank","pupil","Total".
                    objDataColumn.Add("ItemDescription");
                    objDataColumn.Add("Unit");
                    objDataColumn.Add("Total");

                    //Add Subject Name as column in Datatable
                    foreach (var location in locations)
                    {
                        objDataColumn.Add(location);
                    }
                }


                //Add dynamic columns name to datatable dt
                for (int i = 0; i < objDataColumn.Count; i++)
                {
                    dt.Columns.Add(objDataColumn[i].ToString());
                }

                for (int i = 0; i < d.Count; i++)
                {
                    List<string> tempList = new List<string>();
                    tempList.Add(d[i].ItemDescription.ToString());
                    tempList.Add(d[i].Unit == null? "" : d[i].Unit.ToString());
                    tempList.Add(d[i].Total.ToString());

                    var res = d[i].Location.ToList();
                    for (int j = 0; j < res.Count; j++)
                    {
                        tempList.Add(res[j].Quantity.ToString("n1"));
                    }

                    dt.Rows.Add(tempList.ToArray<string>());
                }

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }


       internal async Task<DataTable> GetPODetails()
       {
           try
           {
               var dt = new DataTable();
               var poDetailsData = new List<PurchaseOrderDetail>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                   if (!string.IsNullOrEmpty(LotNumber))
                   {
                       poDetailsData =
                              ctx.PurchaseOrderDetails.Where(
                                  x => x.LotNumber.Contains(LotNumber)).ToList();
                   }
                   else
                   {


                       if (CurrentLocation.LocationCode == "All")
                       {
                           poDetailsData =
                               ctx.PurchaseOrderDetails.Where(
                                   x => x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23) && x.Quantity > 0 && x.ItemDescription.Contains(ItemDescription)).ToList();
                       }
                       else
                       {
                           poDetailsData =
                               ctx.PurchaseOrderDetails.Where(x => x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)
                                                             && x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription)).ToList();
                       }
                   }
               }


               dt.Columns.Add("Date");
               dt.Columns.Add("Location");
               dt.Columns.Add("Vendor");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("Quantity");
               dt.Columns.Add("Unit");
               dt.Columns.Add("TrackedQty");
              // dt.Columns.Add("TrackedDateTime");
               
               if (!poDetailsData.Any()) return dt;

               var lst = poDetailsData.OrderBy(x => x.PurchaseOrderDate).ThenBy(x => x.LocationCode)
                                   .ThenBy(x => x.ItemDescription)
                                   .Select(x => new List<string>()
               {
                   x.PurchaseOrderDate.ToString("dd-MMM-yyyy"),
                   x.LocationCode,
                   x.VendorName,

                   x.ItemDescription,
                   x.LotNumber,
                   x.Quantity.ToString("n1"),
                   x.Unit,
                   x.Quantity == 0?"" : x.Quantity.ToString("n1"),
                 //  x.TrackedDateTime == DateTime.MinValue?"": x.TrackedDateTime.ToString("MMM-dd-yyyy HH:mm:ss")
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

       internal async Task<DataTable> GetTransferSummary()
       {
           // var eb = db.PayrollItems.AsEnumerable().GroupBy(b => new BranchSummary { BranchName = b.Name, PayrollItems = new ObservableCollection<DataLayer.PayrollItem>(( p => p.PayrollItems)), Total = b.Sum(p => p.NetAmount) }).AsEnumerable().Pivot(E => E.PayrollItems, E => E.PayrollJob.Branch.Name, E => E.Amount, true, TransformerClassGenerationEventHandler).ToList();
           try
           {

               var dt = new DataTable();
               //get Data for Date
               var xDetailsData = new List<TransferDailyDetail>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                   if (CurrentLocation.LocationCode == "All")
                   {
                       xDetailsData =
                           ctx.TransferDailyDetails.Where(x => x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23) && x.Quantity > 0 && x.ItemDescription.Contains(ItemDescription)).ToList();
                   }
                   else
                   {
                       xDetailsData =
                           ctx.TransferDailyDetails.Where(x => x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)
                                                       && x.ToLocation == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription)).ToList();
                   }
               }

               if (xDetailsData.Any() == false) return dt;

               var d = (from f in xDetailsData
                        group f by new { f.ItemDescription, f.Unit }
                            into myGroup
                            where myGroup.Any()
                            select new
                            {
                                myGroup.Key.ItemDescription,
                                Unit = myGroup.Key.Unit,
                                Total = myGroup.Sum(x => x.Quantity).ToString("n1"),
                                Location = myGroup.GroupBy(f => f.ToLocation).Select
                                (m => new { Location = m.Key, Quantity = m.Sum(c => c.Quantity) })
                            }).ToList();

               var locations = xDetailsData.Select(x => x.ToLocation).Distinct().ToList();

               //Creating array for adding dynamic columns
               var objDataColumn = new ArrayList();

               if (xDetailsData.Any())
               {
                   //Three column are fix "rank","pupil","Total".
                   objDataColumn.Add("ItemDescription");
                   objDataColumn.Add("Unit");
                   objDataColumn.Add("Total");

                   //Add Subject Name as column in Datatable
                   foreach (var location in locations)
                   {
                       objDataColumn.Add(location);
                   }
               }


               //Add dynamic columns name to datatable dt
               for (int i = 0; i < objDataColumn.Count; i++)
               {
                   dt.Columns.Add(objDataColumn[i].ToString());
               }

               for (int i = 0; i < d.Count; i++)
               {
                   List<string> tempList = new List<string>();
                   tempList.Add(d[i].ItemDescription.ToString());
                   tempList.Add(d[i].Unit.ToString());
                   tempList.Add(d[i].Total.ToString());

                   var res = d[i].Location.ToList();
                   for (int j = 0; j < res.Count; j++)
                   {
                       tempList.Add(res[j].Quantity.ToString("n1"));
                   }

                   dt.Rows.Add(tempList.ToArray<string>());
               }

               return dt;
           }
           catch (Exception)
           {

               throw;
           }
       }

       internal async Task<DataTable> GetTransferDetails()
       {
           try
           {
               var dt = new DataTable();
               var xDetailsData = new List<TransferDailyDetail>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;


                       if (CurrentLocation.LocationCode == "All")
                       {
                           xDetailsData =
                               ctx.TransferDailyDetails.Where(
                                   x => x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23) && x.Quantity > 0 && x.ItemDescription.Contains(ItemDescription)).ToList();
                       }
                       else
                       {
                           xDetailsData =
                               ctx.TransferDailyDetails.Where(x => x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)
                                                             && x.ToLocation == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription)).ToList();
                       }
                   
               }

               dt.Columns.Add("TransferNo");
               dt.Columns.Add("Date");
               dt.Columns.Add("ToLocation");
               dt.Columns.Add("FromLocation");
               dt.Columns.Add("ItemDescription");
               
               dt.Columns.Add("Quantity");
               dt.Columns.Add("Unit");
              

               if (!xDetailsData.Any()) return dt;

               var lst = xDetailsData.OrderBy(x => x.TransferDate).ThenBy(x => x.ToLocation)
                                   .ThenBy(x => x.ItemDescription)
                                   .Select(x => new List<string>()
               {
                   x.TransferNo,
                   x.TransferDate.GetValueOrDefault().ToString("dd-MMM-yyyy"),
                   x.ToLocation,
                   x.FromLocation,
                   x.ItemDescription,
                   
                   x.Quantity.ToString("n1"),
                   x.Unit,
                 //  x.TrackedQuantity.GetValueOrDefault() == 0?"" : x.TrackedQuantity.GetValueOrDefault().ToString("n1"),
                  // x.TrackedDateTime.GetValueOrDefault()== DateTime.MinValue?"": x.TrackedDateTime.GetValueOrDefault().ToString("MMM-dd-yyyy HH:mm:ss")
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

       public async Task<DataTable> GetPOReturnsSummary()
       {
           try
           {

               var dt = new DataTable();
               //get Data for Date
               var poDetailsData = new List<PurchaseReturnsDetail>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                   if (CurrentLocation.LocationCode == "All")
                   {
                       poDetailsData =
                           ctx.PurchaseReturnsDetails.Where(x => (x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23) && x.Quantity > 0)
                               && x.ItemDescription.Contains(ItemDescription)).ToList();
                   }
                   else
                   {
                       poDetailsData =
                           ctx.PurchaseReturnsDetails.Where(x => x.ReturnDate >= StartPODate
                                                       && x.ReturnDate <= EndPODate.AddHours(23)
                                                       && x.LocationCode == CurrentLocation.LocationCode
                                                       && x.ItemDescription.Contains(ItemDescription)).ToList();
                   }
               }

               if (poDetailsData.Any() == false) return dt;

               var d = (from f in poDetailsData
                        group f by new { f.ItemDescription, f.Unit }
                            into myGroup
                            where myGroup.Any()
                            select new
                            {
                                myGroup.Key.ItemDescription,
                                Unit = myGroup.Key.Unit,
                                Total = myGroup.Sum(x => x.Quantity).ToString("n1"),
                                Location = myGroup.GroupBy(f => f.LocationCode).Select
                                (m => new { Location = m.Key, Quantity = m.Sum(c => c.Quantity) })
                            }).ToList();

               var locations = poDetailsData.Select(x => x.LocationCode).Distinct().ToList();

               //Creating array for adding dynamic columns
               var objDataColumn = new ArrayList();

               if (poDetailsData.Any())
               {
                   //Three column are fix "rank","pupil","Total".
                   objDataColumn.Add("ItemDescription");
                   objDataColumn.Add("Unit");
                   objDataColumn.Add("Total");

                   //Add Subject Name as column in Datatable
                   foreach (var location in locations)
                   {
                       objDataColumn.Add(location);
                   }
               }


               //Add dynamic columns name to datatable dt
               for (int i = 0; i < objDataColumn.Count; i++)
               {
                   dt.Columns.Add(objDataColumn[i].ToString());
               }

               for (int i = 0; i < d.Count; i++)
               {
                   List<string> tempList = new List<string>();
                   tempList.Add(d[i].ItemDescription.ToString());
                   tempList.Add(d[i].Unit == null ? "" : d[i].Unit.ToString());
                   tempList.Add(d[i].Total.ToString());

                   var res = d[i].Location.ToList();
                   for (int j = 0; j < res.Count; j++)
                   {
                       tempList.Add(res[j].Quantity.ToString("n1"));
                   }

                   dt.Rows.Add(tempList.ToArray<string>());
               }

               return dt;
           }
           catch (Exception)
           {

               throw;
           }
       }

       public async Task<DataTable> GetPOReturnDetails()
       {
           try
           {
               var dt = new DataTable();
               var xDetailsData = new List<DailyReturn>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;


                   if (CurrentLocation.LocationCode == "All")
                   {
                       xDetailsData =
                           ctx.DailyReturns.Where(
                               x => x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23) && x.TrackedQuantity > 0 && x.ItemDescription.Contains(ItemDescription)).ToList();
                   }
                   else
                   {
                       xDetailsData =
                           ctx.DailyReturns.Where(x => x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23)
                                                         && x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription)).ToList();
                   }

               }

               dt.Columns.Add("ReturnNo");
               dt.Columns.Add("Date");
               dt.Columns.Add("Location");
               dt.Columns.Add("RefPONumber");
               dt.Columns.Add("Vendor");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("Quantity");
               dt.Columns.Add("Unit");
              


               if (!xDetailsData.Any()) return dt;

               var lst = xDetailsData.OrderBy(x => x.ReturnDate).ThenBy(x => x.LocationCode)
                                   .ThenBy(x => x.ItemDescription)
                                   .Select(x => new List<string>()
               {
                   x.ReturnNumber,
                   x.ReturnDate.ToShortDateString(),
                   x.LocationCode,
                   x.RefPONumber,
                   x.VendorNo,
                   x.ItemDescription,
                   x.LotNumber,
                   x.TrackedQuantity.ToString("n1"),
                   x.Unit,
                 //  x.TrackedQuantity.GetValueOrDefault() == 0?"" : x.TrackedQuantity.GetValueOrDefault().ToString("n1"),
                  // x.TrackedDateTime.GetValueOrDefault()== DateTime.MinValue?"": x.TrackedDateTime.GetValueOrDefault().ToString("MMM-dd-yyyy HH:mm:ss")
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

       internal async Task<DataTable> GetTransActivitySummary()
       {
           // var eb = db.PayrollItems.AsEnumerable().GroupBy(b => new BranchSummary { BranchName = b.Name, PayrollItems = new ObservableCollection<DataLayer.PayrollItem>(( p => p.PayrollItems)), Total = b.Sum(p => p.NetAmount) }).AsEnumerable().Pivot(E => E.PayrollItems, E => E.PayrollJob.Branch.Name, E => E.Amount, true, TransformerClassGenerationEventHandler).ToList();
           try
           {

               var dt = new DataTable();
               dt.TableName = "TransActivitySummary";
               //get Data for Date
               var locItemDetails = GetTransActivtySummaryData();

               if (locItemDetails.Any() == false) return dt;

               var d = (from f in locItemDetails
                        group f by new { f.LocationCode,f.ItemDescription, f.LotNumber }
                            into myGroup
                            where myGroup.Any()
                            select new
                            {
                                myGroup.Key.ItemDescription,
                                myGroup.Key.LotNumber,
                                myGroup.First().DateCreated,
                                myGroup.Key.LocationCode,
                                
                                Total = myGroup.Sum(x => x.Quantity.GetValueOrDefault()).ToString("n1"),
                                Type = myGroup.GroupBy(f => f.Type).Select
                                (m => new { Type = m.Key, Quantity = m.Sum(c => c.Quantity) })
                            }).ToList();

               var types = locItemDetails.Select(x => x.Type).Distinct().OrderBy(x => x).ToList();

               //Creating array for adding dynamic columns
               var objDataColumn = new ArrayList();

               if (locItemDetails.Any())
               {
                   //Three column are fix "rank","pupil","Total".
                   objDataColumn.Add("ItemDescription");
                   objDataColumn.Add("LotNumber");
                   objDataColumn.Add("DateCreated");
                   objDataColumn.Add("Location");
                   objDataColumn.Add("NetQuantity");

                   //Add Subject Name as column in Datatable
                   foreach (var type in types)
                   {
                       objDataColumn.Add(type);
                   }

               }


               //Add dynamic columns name to datatable dt
               for (int i = 0; i < objDataColumn.Count; i++)
               {
                   if (objDataColumn[i] == "NetQuantity")
                   {
                       dt.Columns.Add(objDataColumn[i].ToString(), typeof(double));
                   }
                   else
                   {
                       dt.Columns.Add(objDataColumn[i].ToString());
                   }
                   
               }

               for (int i = 0; i < d.Count; i++)
               {
                   List<string> tempList = new List<string>();
                   tempList.Add(d[i].ItemDescription.ToString());
                   tempList.Add(d[i].LotNumber.ToString());
                   tempList.Add(d[i].DateCreated.ToString("dd-MMM-yyyy"));
                   tempList.Add(d[i].LocationCode);
                   
                   tempList.Add(d[i].Total.ToString());
                   
                   tempList.AddRange(types.Select(t => ""));

                   var res = d[i].Type.ToList();
                   for (int j = 0; j < res.Count; j++)
                   {
                        var c = 4;
                       foreach (var t in types)
                       {
                           c += 1;
                           if (res[j].Type == t)
                           {
                               tempList[c] = (res[j].Quantity.HasValue == false
                                   ? ""
                                   : res[j].Quantity.GetValueOrDefault().ToString("n1"));
                               break;
                           }
                          
                       }
                   }

                   dt.Rows.Add(tempList.ToArray<string>());
               }

               return dt;
           }
           catch (Exception)
           {

               throw;
           }
       }

       private List<TransactionActivity> GetTransactionActivtyDetailsData()
       {
           var locItemDetails = new List<TransactionActivity>();
           using (var ctx = new TrackerDBDataContext())
           {
               ctx.CommandTimeout = 0;
               if (!string.IsNullOrEmpty(LotNumber))
               {
                   locItemDetails.AddRange(
                       ctx.DailyPODetails
                           .Where(x => x.LotNumber.Contains(LotNumber))
                           .Select(x => new TransactionActivity()
                           {
                               TransactionNo = x.PONumber,
                               TransactionDate = x.PODate,
                               LocationCode = x.LocationCode,
                               Contact = x.VendorNo,
                               ItemDescription = x.ItemDescription,
                               LotNumber = x.LotNumber,
                               Type = x.Type,
                               Quantity = x.TrackedQuantity,
                               Unit = x.Unit,
                               TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                               TrackedDateTime = x.TrackedDateTime,
                               DateCreated = x.PODate
                           }).ToList());

                    locItemDetails.AddRange(
                       ctx.DailyExportDetails
                           .Where(x => x.LotNumber.Contains(LotNumber))
                           .Select(x => new TransactionActivity()
                           {
                               TransactionNo = x.ExportNumber,
                               TransactionDate = x.ExportDate,
                               LocationCode = x.LocationCode,
                               Contact = x.Vendor,
                               ItemDescription = x.ItemDescription,
                               LotNumber = x.LotNumber,
                               Type = x.Type,
                               Quantity = (decimal?) x.TrackedQuantity,
                               Unit = x.Unit,
                               TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                               TrackedDateTime = x.TrackedDateTime,
                               DateCreated = x.ExportDate
                           }).ToList());

                    locItemDetails.AddRange(
                      ctx.DailyReturns
                          .Where(x => x.LotNumber.Contains(LotNumber))
                          .Select(x => new TransactionActivity()
                          {
                              TransactionNo = x.ReturnNumber,
                              TransactionDate = x.ReturnDate,
                              LocationCode = x.LocationCode,
                              Contact = x.VendorNo,
                              ItemDescription = x.ItemDescription,
                              LotNumber = x.LotNumber,
                              Type = x.Type,
                              Quantity = x.TrackedQuantity,
                              Unit = x.Unit,
                              TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                              TrackedDateTime = x.TrackedDateTime,
                              DateCreated = x.ReturnDate
                          }).ToList());

                   locItemDetails.AddRange(
                       ctx.DailyTransfersOuts
                           .Where(x => x.LotNumber.Contains(LotNumber))
                           .Select(x => new TransactionActivity()
                           {
                               TransactionNo = x.TransferNo,
                               TransactionDate = x.TransferDate,
                               LocationCode = x.LocationCode,
                               Contact = x.ToLocation,
                               ItemDescription = x.ItemDescription,
                               LotNumber = x.LotNumber,
                               Type = x.Type,
                               Quantity = x.TrackedQuantity,
                               Unit = x.Unit,
                               TrackedQty = x.TrackedQuantity,
                               TrackedDateTime = x.TrackedDateTime,
                               DateCreated = x.PurchaseOrderDate
                           }).ToList());

                   locItemDetails.AddRange(
                       ctx.DailyTransfersRecieveds
                           .Where(x => x.LotNumber.Contains(LotNumber))
                           .Select(x => new TransactionActivity()
                           {
                               TransactionNo = x.TransferNo,
                               TransactionDate = x.TransferDate,
                               LocationCode = x.LocationCode,
                               Contact = x.FromLocation,
                               ItemDescription = x.ItemDescription,
                               LotNumber = x.LotNumber,
                               Type = x.Type,
                               Quantity = x.TrackedQuantity,
                               Unit = x.Unit,
                               TrackedQty = x.TrackedQuantity,
                               TrackedDateTime = x.TrackedDateTime,
                               DateCreated = x.PurchaseOrderDate
                           }).ToList());

                   locItemDetails.AddRange(
                       ctx.DailySalesDetails
                           .Where(x => x.LotNumber.Contains(LotNumber))
                           .Select(x => new TransactionActivity()
                           {
                               TransactionNo = x.InvoiceNo,
                               TransactionDate = x.InvoiceDate,
                               LocationCode = x.LocationCode,
                               Contact = x.CustomerNo,
                               ItemDescription = x.ItemDescription,
                               LotNumber = x.LotNumber,
                               Type = x.Type,
                               Quantity = x.TrackedQuantity,
                               Unit = x.Unit,
                               TrackedQty = x.TrackedQuantity,
                               TrackedDateTime = x.TrackedDateTime,
                               DateCreated = x.PurchaseOrderDate
                           }).ToList());

               }
               else
               {
                   if (CurrentLocation.LocationCode == "All")
                   {
                       locItemDetails.AddRange(
                           ctx.DailyPODetails
                               .Where(x => x.PODate >= StartPODate && x.PODate <= EndPODate.AddHours(23) && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.PONumber,
                                   TransactionDate = x.PODate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PODate
                               }).ToList());

                        locItemDetails.AddRange(
                           ctx.DailyExportDetails
                               .Where(x => x.ExportDate >= StartPODate && x.ExportDate <= EndPODate.AddHours(23) && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.ExportNumber,
                                   TransactionDate = x.ExportDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.Vendor,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = (decimal?) x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.ExportDate
                               }).ToList());

                        locItemDetails.AddRange(
                           ctx.DailyReturns
                               .Where(x => ((x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23)) || (x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23)))
                                       && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.ReturnNumber,
                                   TransactionDate = x.ReturnDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.RefPONumber,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt32(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.ReturnDate
                               }).ToList());

                       locItemDetails.AddRange(
                           ctx.DailyTransfersOuts
                               .Where(x => ((x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)) || (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23))) 
                                       && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.ToLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());

                       locItemDetails.AddRange(
                           ctx.DailyTransfersRecieveds
                               .Where(x => ((x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)) ||
                                       (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23))) && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.FromLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());

                      locItemDetails.AddRange(
                      ctx.DailySalesDetails
                          .Where(x => ((x.InvoiceDate >= StartPODate && x.InvoiceDate <= EndPODate.AddHours(23)) ||
                                       (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23))) && x.ItemDescription.Contains(ItemDescription))
                          .Select(x => new TransactionActivity()
                          {
                              TransactionNo = x.InvoiceNo,
                              TransactionDate = x.InvoiceDate,
                              LocationCode = x.LocationCode,
                              Contact = x.CustomerNo,
                              ItemDescription = x.ItemDescription,
                              LotNumber = x.LotNumber,
                              Type = x.Type,
                              Quantity = x.TrackedQuantity,
                              Unit = x.Unit,
                              TrackedQty = x.TrackedQuantity,
                              TrackedDateTime = x.TrackedDateTime,
                              DateCreated = x.PurchaseOrderDate
                          }).ToList());
                   }
                   else
                   {
                       locItemDetails.AddRange(
                           ctx.DailyPODetails
                               .Where(
                                   x =>
                                       x.PODate >= StartPODate && x.PODate <= EndPODate.AddHours(23) &&
                                       x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.PONumber,
                                   TransactionDate = x.PODate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PODate
                               }).ToList());

                        locItemDetails.AddRange(
                           ctx.DailyExportDetails
                               .Where(
                                   x =>
                                       x.ExportDate >= StartPODate && x.ExportDate <= EndPODate.AddHours(23) &&
                                       x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.ExportNumber,
                                   TransactionDate = x.ExportDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.Vendor,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = (decimal?) x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.ExportDate
                               }).ToList());

                        locItemDetails.AddRange(
                          ctx.DailyReturns
                              .Where(
                                  x =>
                                      x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23) &&
                                      x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription))
                              .Select(x => new TransactionActivity()
                              {
                                  TransactionNo = x.ReturnNumber,
                                  TransactionDate = x.ReturnDate,
                                  LocationCode = x.LocationCode,
                                  Contact = x.VendorNo,
                                  ItemDescription = x.ItemDescription,
                                  LotNumber = x.LotNumber,
                                  Type = x.Type,
                                  Quantity = x.TrackedQuantity,
                                  Unit = x.Unit,
                                  TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                  TrackedDateTime = x.TrackedDateTime,
                                  DateCreated = x.ReturnDate
                              }).ToList());

                       locItemDetails.AddRange(
                           ctx.DailyTransfersOuts
                               .Where(
                                   x =>
                                       ((x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)) ||
                                       (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23))) &&
                                       x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.ToLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());

                       locItemDetails.AddRange(
                           ctx.DailyTransfersRecieveds
                               .Where(
                                   x =>
                                       ((x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)) ||
                                       (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23))) &&
                                       x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.FromLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());

                       locItemDetails.AddRange(
                      ctx.DailySalesDetails
                          .Where(
                                   x =>
                                       ((x.InvoiceDate >= StartPODate && x.InvoiceDate <= EndPODate.AddHours(23)) ||
                                       (x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23))) &&
                                       x.LocationCode == CurrentLocation.LocationCode && x.ItemDescription.Contains(ItemDescription))
                          .Select(x => new TransactionActivity()
                          {
                              TransactionNo = x.InvoiceNo,
                              TransactionDate = x.InvoiceDate,
                              LocationCode = x.LocationCode,
                              Contact = x.CustomerNo,
                              ItemDescription = x.ItemDescription,
                              LotNumber = x.LotNumber,
                              Type = x.Type,
                              Quantity = x.TrackedQuantity,
                              Unit = x.Unit,
                              TrackedQty = x.TrackedQuantity,
                              TrackedDateTime = x.TrackedDateTime,
                              DateCreated = x.PurchaseOrderDate
                          }).ToList());
                   }
               }
           }
           return locItemDetails;
       }

       private List<TransactionActivity> GetTransActivtySummaryData()
       {
           var locItemDetails = new ConcurrentQueue<List<TransactionActivity>>();
         
               
               if (!string.IsNullOrEmpty(LotNumber))
               {
                   Task.Run(() => GetTransActWithLotNumbers(locItemDetails)).Wait();
               }
               else
               {
                   if (CurrentLocation.LocationCode == "All")
                   {
                       Task.Run(() => GetTransActWithAllLocations(locItemDetails)).Wait();
                   }
                   else
                   {
                       Task.Run(() => GetTransActWithLocation(locItemDetails)).Wait();
                   }
               }

               var res = locItemDetails.SelectMany(x => x).ToList();

               var returns = res.Where(x => x.Type == "Returns").ToList();
               var purchases = res.Where(x => x.Type == "Purchases").ToList();

               foreach (var ret in returns)
               {
                   DailyReturn r = null;
                   using( var ctx = new TrackerDBDataContext())
                   {
                       r = ctx.DailyReturns.FirstOrDefault(x => x.LotNumber == ret.LotNumber);
                   }
                   if (r != null && !string.IsNullOrEmpty(r.RefPONumber))
                   {
                       var ps = purchases.Where(x => r.RefPONumber.Contains(x.TransactionNo)
                                                     && x.ItemDescription == r.ItemDescription
                                                     && x.TransactionDate <= ret.TransactionDate
                                                     && x.Contact == r.VendorNo);
                       foreach (var p in ps)
                       {
                           ret.LotNumber = p.LotNumber;
                           ret.DateCreated = p.DateCreated;

                       }
                   }
               }
         
           return res ;
       }

       private async Task GetTransActWithLocation(ConcurrentQueue<List<TransactionActivity>> locItemDetails)
       {
           var tasks = new List<Task>();
           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyPODetails
                               .Where(
                                   x =>
                                       (x.PODate >= StartPODate && x.PODate <= EndPODate.AddHours(23)) &&
                                       x.LocationCode == CurrentLocation.LocationCode &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.PONumber,
                                   TransactionDate = x.PODate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PODate
                               }).ToList());
                   }
               }
                   ));

            tasks.Add(
            Task.Run(() =>
            {
                using (var ctx = new TrackerDBDataContext())
                {
                    ctx.CommandTimeout = 0;
                    locItemDetails.Enqueue(
                        ctx.DailyExportDetails
                            .Where(
                                x =>
                                    (x.ExportDate >= StartPODate && x.ExportDate <= EndPODate.AddHours(23)) &&
                                    x.LocationCode == CurrentLocation.LocationCode &&
                                    x.ItemDescription.Contains(ItemDescription))
                            .Select(x => new TransactionActivity()
                            {
                                TransactionNo = x.ExportNumber,
                                TransactionDate = x.ExportDate,
                                LocationCode = x.LocationCode,
                                Contact = x.Vendor,
                                ItemDescription = x.ItemDescription,
                                LotNumber = x.LotNumber,
                                Type = x.Type,
                                Quantity = (decimal?) x.TrackedQuantity,
                                Unit = x.Unit,
                                TrackedQty = Convert.ToDouble(x.TrackedQuantity),
                                TrackedDateTime = x.TrackedDateTime,
                                DateCreated = x.ExportDate
                            }).ToList());
                }
            }
                   ));

            tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyReturns
                               .Where(
                                   x =>
                                       (x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23)) &&
                                       x.LocationCode == CurrentLocation.LocationCode &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.ReturnNumber,
                                   TransactionDate = x.ReturnDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity * -1,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity * -1),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.ReturnDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyTransfersOuts
                               .Where(
                                   x =>
                                       ((x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)) ||
                                        (x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23))) &&
                                       x.LocationCode == CurrentLocation.LocationCode &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.ToLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyTransfersRecieveds
                               .Where(
                                   x =>
                                       ((x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)) ||
                                        (x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23))) &&
                                       x.LocationCode == CurrentLocation.LocationCode &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.FromLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailySalesDetails
                               .Where(
                                   x =>
                                       ((x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)) ||
                                        (x.InvoiceDate >= StartPODate && x.InvoiceDate <= EndPODate.AddHours(23))) &&
                                       x.LocationCode == CurrentLocation.LocationCode &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.InvoiceNo,
                                   TransactionDate = x.InvoiceDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.CustomerNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));
          await Task.WhenAll(tasks).ConfigureAwait(false);
       }

       private async Task GetTransActWithAllLocations(ConcurrentQueue<List<TransactionActivity>> locItemDetails)
       {
           var tasks = new List<Task>();
           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyPODetails
                               .Where(
                                   x =>
                                       x.PODate >= StartPODate && x.PODate <= EndPODate.AddHours(23) &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.PONumber,
                                   TransactionDate = x.PODate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToInt16(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PODate
                               }).ToList());
                   }
               }
                   ));

            tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyExportDetails
                               .Where(
                                   x =>
                                       x.ExportDate >= StartPODate && x.ExportDate <= EndPODate.AddHours(23) &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.ExportNumber,
                                   TransactionDate = x.ExportDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.Vendor,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = (decimal?) x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToDouble(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.ExportDate
                               }).ToList());
                   }
               }
                   ));

            tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyReturns
                               .Where(
                                   x =>
                                       x.ReturnDate >= StartPODate && x.ReturnDate <= EndPODate.AddHours(23) &&
                                       x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.ReturnNumber,
                                   TransactionDate = x.ReturnDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity * -1,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToDouble(x.TrackedQuantity * -1),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.ReturnDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyTransfersOuts
                               .Where(x => ((x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)) ||
                                            (x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23))) &&
                                           x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.ToLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyTransfersRecieveds
                               .Where(x => ((x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)) ||
                                            (x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23))) &&
                                           x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.FromLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));
           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailySalesDetails
                               .Where(x => ((x.PurchaseOrderDate >= StartPODate && x.PurchaseOrderDate <= EndPODate.AddHours(23)) ||
                                            (x.InvoiceDate >= StartPODate && x.InvoiceDate <= EndPODate.AddHours(23))) &&
                                           x.ItemDescription.Contains(ItemDescription))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.InvoiceNo,
                                   TransactionDate = x.InvoiceDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.CustomerNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));
           await Task.WhenAll(tasks).ConfigureAwait(false);
       }

       private async Task GetTransActWithLotNumbers(ConcurrentQueue<List<TransactionActivity>> locItemDetails)
       {

           var tasks = new List<Task>();
           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyPODetails
                               .Where(x => x.LotNumber.Contains(LotNumber))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.PONumber,
                                   TransactionDate = x.PODate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.VendorNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = Convert.ToDouble(x.TrackedQuantity),
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PODate
                               }).ToList());
                   }
               }
                   ));

            tasks.Add(
              Task.Run(() =>
              {
                  using (var ctx = new TrackerDBDataContext())
                  {
                      ctx.CommandTimeout = 0;
                      locItemDetails.Enqueue(
                          ctx.DailyExportDetails
                              .Where(x => x.LotNumber.Contains(LotNumber))
                              .Select(x => new TransactionActivity()
                              {
                                  TransactionNo = x.ExportNumber,
                                  TransactionDate = x.ExportDate,
                                  LocationCode = x.LocationCode,
                                  Contact = x.Vendor,
                                  ItemDescription = x.ItemDescription,
                                  LotNumber = x.LotNumber,
                                  Type = x.Type,
                                  Quantity = (decimal?) x.TrackedQuantity,
                                  Unit = x.Unit,
                                  TrackedQty = Convert.ToDouble(x.TrackedQuantity),
                                  TrackedDateTime = x.TrackedDateTime,
                                  DateCreated = x.ExportDate
                              }).ToList());
                  }
              }
                  ));

            tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyTransfersOuts
                               .Where(x => x.LotNumber.Contains(LotNumber))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.ToLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailyTransfersRecieveds
                               .Where(x => x.LotNumber.Contains(LotNumber))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.TransferNo,
                                   TransactionDate = x.TransferDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.FromLocation,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));

           tasks.Add(
               Task.Run(() =>
               {
                   using (var ctx = new TrackerDBDataContext())
                   {
                       ctx.CommandTimeout = 0;
                       locItemDetails.Enqueue(
                           ctx.DailySalesDetails
                               .Where(x => x.LotNumber.Contains(LotNumber))
                               .Select(x => new TransactionActivity()
                               {
                                   TransactionNo = x.InvoiceNo,
                                   TransactionDate = x.InvoiceDate,
                                   LocationCode = x.LocationCode,
                                   Contact = x.CustomerNo,
                                   ItemDescription = x.ItemDescription,
                                   LotNumber = x.LotNumber,
                                   Type = x.Type,
                                   Quantity = x.TrackedQuantity,
                                   Unit = x.Unit,
                                   TrackedQty = x.TrackedQuantity,
                                   TrackedDateTime = x.TrackedDateTime,
                                   DateCreated = x.PurchaseOrderDate
                               }).ToList());
                   }
               }
                   ));
           
           tasks.Add(
                         Task.Run(() =>
                         {
                             using (var ctx = new TrackerDBDataContext())
                             {
                                 ctx.CommandTimeout = 0;
                                 locItemDetails.Enqueue(
                                     ctx.DailyReturns
                                         .Where(x => x.LotNumber.Contains(LotNumber.Substring(0, LotNumber.Length - 2)))
                                         .Select(x => new TransactionActivity()
                                         {
                                             TransactionNo = x.RefPONumber,
                                             TransactionDate = x.ReturnDate,
                                             LocationCode = x.LocationCode,
                                             Contact = x.VendorNo,
                                             ItemDescription = x.ItemDescription,
                                             LotNumber = x.LotNumber,
                                             Type = x.Type,
                                             Quantity = x.TrackedQuantity * -1,
                                             Unit = x.Unit,
                                             TrackedQty = Convert.ToDouble(x.TrackedQuantity * -1),
                                             TrackedDateTime = x.TrackedDateTime,
                                             DateCreated = x.ReturnDate
                                         }).ToList());
                             }
                         }));

           await Task.WhenAll(tasks).ConfigureAwait(false);


       }

       internal async Task<DataTable> GetTransActivityDetails()
       {
           try
           {
               var dt = new DataTable();
               var LotDetailsData = GetTransactionActivtyDetailsData();


              
               dt.Columns.Add("TransactionDate");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("TransactionNo");
               dt.Columns.Add("Activity");
               dt.Columns.Add("Location");
               dt.Columns.Add("Contact");
               dt.Columns.Add("TrackedQty");
               dt.Columns.Add("Unit");
               


               if (!LotDetailsData.Any()) return dt;

               var lst = LotDetailsData.OrderBy(x => x.LotNumber).ThenBy(x => x.TransactionDate)
                                   .ThenBy(x => x.LocationCode)
                                   .Select(x => new List<string>()
               {
                    x.TransactionDate.GetValueOrDefault().ToString("dd-MMM-yyyy"),
                   x.ItemDescription,
                   x.LotNumber,
                   x.TransactionNo,
                   x.Type,
                   x.LocationCode,
                  x.Contact,
                   x.TrackedQty.GetValueOrDefault() == 0?"" : x.TrackedQty.GetValueOrDefault().ToString("n1"),
                   x.Unit
                  // x.TrackedDateTime.GetValueOrDefault()== DateTime.MinValue?"": x.TrackedDateTime.GetValueOrDefault().ToString("MMM-dd-yyyy HH:mm:ss")
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

       internal async Task<DataTable> GetUnknownGunData()
       {
           try
           {
               var dt = new DataTable();
               var UGData = new List<UnknownGunTransaction>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                   UGData =
                       ctx.UnknownGunTransactions.Where(
                           x => x.TransactionDateTime >= StartPODate && x.TransactionDateTime <= EndPODate.AddHours(23)).ToList();
               }



               dt.Columns.Add("TransactionNo");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("TrackedQty");
               dt.Columns.Add("TrackedDateTime");
              
               if (!UGData.Any()) return dt;

               var lst = UGData.OrderBy(x => x.LotNumber).ThenBy(x => x.ItemDescription)
                                   .ThenBy(x => x.TransactionDateTime)
                                   .Select(x => new List<string>()
               {
                   x.TransactionNo,
                   x.LotNumber,
                   x.ItemDescription,
                   x.Quantity.ToString("N0"),
                   x.TransactionDateTime.ToString("MMM-dd-yyyy HH:mm:ss")
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

       public async Task<DataTable> GetTransferOS()
       {
           try
           {
               var dt = new DataTable();
               var OSData = new List<OverShort>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                  OSData.AddRange(
                       ctx.ReconDailyTransfers.Where(
                           x => x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)).Select(x => new OverShort()
                           {
                               TransactionDate = x.TransferDate.GetValueOrDefault(),
                               TransactionNo = x.TransferNo,
                               LotNumber = x.LotNumber,
                               ItemDescription = x.ItemDescription,
                               TrackedQty = x.TrackedQty.GetValueOrDefault(),
                               TransactionQty = Convert.ToInt16(x.TransactionQty),
                               Variance = Convert.ToInt16(x.Variance),
                               VarianceType = x.Type
                           }).ToList());
               }



               dt.Columns.Add("TransactionDate");
               dt.Columns.Add("TransactionNo");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("TrackedQty");
               dt.Columns.Add("TransactionQty");
               dt.Columns.Add("Variance");
               dt.Columns.Add("VarianceType");

               if (!OSData.Any()) return dt;

               var lst = OSData.OrderBy(x => x.LotNumber).ThenBy(x => x.ItemDescription)
                                   .Select(x => new List<string>()
               {
                   x.TransactionDate.ToString("MMM-dd-yyyy"),
                   x.TransactionNo,
                   x.LotNumber,
                   x.ItemDescription,
                   x.TrackedQty.ToString("N0"),
                   x.TransactionQty.ToString("N0"),
                   x.Variance.ToString("N0"),
                   x.VarianceType
                  
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

       internal async Task<DataTable> GetOversShorts()
       {
           try
           {
               var dt = new DataTable();
               var OSData = new List<OverShort>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                   OSData.AddRange(
                       ctx.ReconDailySales.Where(
                           x => x.InvoiceDate >= StartPODate && x.InvoiceDate <= EndPODate.AddHours(23)).Select(x => new OverShort()
                           {
                               TransactionDate = x.InvoiceDate.GetValueOrDefault(),
                               TransactionNo = x.InvoiceNo,
                               LotNumber = x.LotNumber,
                               ItemDescription = x.ItemDescription,
                               TrackedQty = x.TrackedQty.GetValueOrDefault(),
                               TransactionQty = Convert.ToInt16(x.TransactionQty),
                               Variance = Convert.ToInt16(x.Variance),
                               VarianceType = x.Type
                           }).ToList());

                   //OSData.AddRange(
                   //    ctx.ReconDailyTransfers.Where(
                   //        x => x.TransferDate >= StartPODate && x.TransferDate <= EndPODate.AddHours(23)).Select(x => new OverShort()
                   //        {
                   //            TransactionDate = x.TransferDate.GetValueOrDefault(),
                   //            TransactionNo = x.TransferNo,
                   //            LotNumber = x.LotNumber,
                   //            ItemDescription = x.ItemDescription,
                   //            TrackedQty = x.TrackedQty.GetValueOrDefault(),
                   //            TransactionQty = Convert.ToInt16(x.TransactionQty),
                   //            Variance = Convert.ToInt16(x.Variance),
                   //            VarianceType = x.Type
                   //        }).ToList());
               }



               dt.Columns.Add("TransactionDate");
               dt.Columns.Add("TransactionNo");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("TrackedQty");
               dt.Columns.Add("TransactionQty");
               dt.Columns.Add("Variance");
               dt.Columns.Add("VarianceType");

               if (!OSData.Any()) return dt;

               var lst = OSData.OrderBy(x => x.LotNumber).ThenBy(x => x.ItemDescription)
                                   .Select(x => new List<string>()
               {
                   x.TransactionDate.ToString("MMM-dd-yyyy"),
                   x.TransactionNo,
                   x.LotNumber,
                   x.ItemDescription,
                   x.TrackedQty.ToString("N0"),
                   x.TransactionQty.ToString("N0"),
                   x.Variance.ToString("N0"),
                   x.VarianceType
                  
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

       internal async Task<DataTable> GetGunData()
       {
           try
           {
               var dt = new DataTable();
               var UGData = new List<GunData>();
               using (var ctx = new TrackerDBDataContext())
               {
                   ctx.CommandTimeout = 0;
                   UGData =
                       ctx.GunDatas.Where(
                           x => x.TransactionDateTime >= StartPODate && x.TransactionDateTime <= EndPODate.AddHours(23).AddHours(23)).ToList();
               }



               dt.Columns.Add("TransactionNo");
               dt.Columns.Add("LotNumber");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("TrackedQty");
               dt.Columns.Add("TrackedDateTime");

               if (!UGData.Any()) return dt;

               var lst = UGData.OrderBy(x => x.LotNumber)
                                   .ThenBy(x => x.TransactionDateTime)
                                   .Select(x => new List<string>()
               {
                   x.TransactionNo,
                   x.LotNumber,
                   x.ItemDescription,
                   x.Quantity.ToString("N0"),
                   x.TransactionDateTime.ToString("MMM-dd-yyyy HH:mm:ss")
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

        internal async Task<DataTable> GetReceiptReport()
        {
            // var eb = db.PayrollItems.AsEnumerable().GroupBy(b => new BranchSummary { BranchName = b.Name, PayrollItems = new ObservableCollection<DataLayer.PayrollItem>(( p => p.PayrollItems)), Total = b.Sum(p => p.NetAmount) }).AsEnumerable().Pivot(E => E.PayrollItems, E => E.PayrollJob.Branch.Name, E => E.Amount, true, TransformerClassGenerationEventHandler).ToList();
            try
            {

                var dt = new DataTable();
                //get Data for Date
                var xDetailsData = new List<ExportData>();
                using (var ctx = new TrackerDBDataContext())
                {
                    xDetailsData =
                           ctx.ExportDatas.Where(x => x.ExportDate >= StartPODate && x.ExportDate <= EndPODate.AddHours(23)).ToList();

                }

                if (xDetailsData.Any() == false) return dt;


                dt.Columns.Add("ReceiptDate");
                dt.Columns.Add("ReceiptNumber");
                dt.Columns.Add("Harvester");
                dt.Columns.Add("CustomerName");
                dt.Columns.Add("ProductDescription");
                dt.Columns.Add("LineNumber");
                dt.Columns.Add("Weight");
                dt.Columns.Add("TicketNo");
                


                var lst = xDetailsData.OrderBy(x => x.ExportDate).ThenBy(x => x.Harvester)
                                   .ThenBy(x => x.CustomerInfo)
                                   .Select(x => new List<string>()
               {
                   x.ExportDate.ToString("dd-MMM-yyyy"),
                   x.ReceiptNumber,
                   x.Harvester,
                   x.CustomerInfo,
                   x.ProductDescription,
                   x.LineNumber.ToString(),
                   x.Weight.ToString("n1"),
                   x.TransactionNumber.ToString(),
                   //  x.OrderNo.ToString(),
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

       
    }

    internal class OverShort
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionNo { get; set; }
        public string LotNumber { get; set; }
        public string ItemDescription { get; set; }
        public int TrackedQty { get; set; }
        public int TransactionQty { get; set; }
        public int Variance { get; set; }
        public string VarianceType { get; set; }
    }

    internal class TransactionActivity
    {
        public DateTime? TransactionDate { get; set; }
        public string LocationCode { get; set; }
        public string ItemDescription { get; set; }
        public string LotNumber { get; set; }
        public string Type { get; set; }
        public decimal? Quantity { get; set; }
        public double? TrackedQty { get; set; }
        public DateTime? TrackedDateTime { get; set; }
        public string Contact { get; set; }
        public string Unit { get; set; }
        public string TransactionNo { get; set; }
        public DateTime DateCreated
        { get; set; }
    }


    public class PoSummaryLine
   {
       public string LocationId { get; set; }
       public double Total { get; set; }
       public List<DailyPODetail> DailyPODetails { get; set; }
   }
}
