using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AllocationDS.Business.Entities;
using AllocationDS.Business.Services;
using Core.Common.UI;
using DocumentDS.Business.Entities;
using DocumentItemDS.Business.Entities;
using MoreLinq;
using TrackableEntities;
using AsycudaDocument = AllocationDS.Business.Entities.AsycudaDocument;
using xBondAllocations = AllocationDS.Business.Entities.xBondAllocations;
using xcuda_Item = DocumentItemDS.Business.Entities.xcuda_Item;
using xcuda_PreviousItem = AllocationDS.Business.Entities.xcuda_PreviousItem;
using xcuda_PreviousItemService = AllocationDS.Business.Services.xcuda_PreviousItemService;


namespace WaterNut.DataSpace
{
	public class BuildSalesReportClass
	{
		private static readonly BuildSalesReportClass instance;
		static BuildSalesReportClass()
		{
			instance = new BuildSalesReportClass();
			Initialization = InitializationAsync();
		}

		public static BuildSalesReportClass Instance
		{
			get { return instance; }
		}

		public static Task Initialization { get; private set; }

		private static async Task InitializationAsync()
		{
	 

		  
		}


		public void BuildSalesReport()//string dfp
		{
			try
			{
				StatusModel.Timer("Processing Allocations");//, alst.Count()
				var exceptions = new ConcurrentQueue<Exception>();
			   
			   var alst = GetAllocations().ToList();
				//Parallel.ForEach(alst, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount * 2}, g =>//

			   // var ps = alst.SelectMany(x => x.Pi).DistinctBy(x => x.Pi.PreviousItem_Id).OrderBy(x => x.pAssessmentDate).ThenBy(x => x.pRegDate);
				var ps =
					alst.OrderBy(x => x.SalesDate).GroupBy(x => new { x.Item_Id, x.DutyFreePaid, x.SalesDate })// ,
						.Select(
							x =>
								new
								{
									x.Key.Item_Id,
									Allocations = x.Select(z =>  z.Allocation).OrderBy(z => z.AllocationId),
									//SalesMonth = x.Key.Month,
									PList = x.SelectMany(z => z.Pi)
                                             .Where(z => z.pAssessmentDate.Month == x.Key.SalesDate.Month && z.pAssessmentDate.Year == x.Key.SalesDate.Year)
                                    .DistinctBy(z => z.Pi.PreviousItem_Id).OrderBy(z => z.pAssessmentDate).ThenBy(z => z.pRegDate),//
									x.Key.DutyFreePaid
								   
								});
				

				foreach (var g in ps)
			  //  Parallel.ForEach(ps, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount * 2 }, g =>
				{
					try
					{
						//var lst = alst.Where(x => x.Pi.Any(z => z.Pi.PreviousItem_Id == g.Pi.PreviousItem_Id)).Select(x => x.Allocation);   
					   
							SetXBondLst(g.Allocations , g.PList.OrderBy(z => z.pAssessmentDate).ThenBy(z => z.pRegDate).Select(x => x.Pi).ToList()).Wait();
						
							StatusModel.StatusUpdate();
					  

					}
					catch (Exception ex)
					{
						exceptions.Enqueue(ex);
					}
				}

				 // });



		   
			if (exceptions.Count > 0) throw new AggregateException(exceptions);
			}
			catch (Exception)
			{
				throw;
			}
		}

		

		public IEnumerable<AllocationPi> GetAllocations()
		{


			using (var ctx = new AllocationDSContext() {StartTracking = true})
			{
				try
				{

					ctx.Configuration.LazyLoadingEnabled = false;
					ctx.Configuration.AutoDetectChangesEnabled = false;

					
					var res = ctx.AsycudaSalesAllocations
						.Where(x => x.EntryDataDetails.Sales != null

                                   // && "LUB/11350".Contains(x.EntryDataDetails.ItemNumber) && x.EntryDataDetails.Sales.EntryDataDate.Month == 1 && x.EntryDataDetails.Sales.EntryDataDate.Year == 2017


                                    && x.PreviousDocumentItem != null
									&& x.PreviousDocumentItem.EntryPreviousItems.Any(
										z =>
                                            x.EntryDataDetails.Sales.EntryDataDate >= z.xcuda_Item.AsycudaDocument.AssessmentDate
										    && z.xcuda_Item.AsycudaDocument.Cancelled != true
											&& z.xcuda_Item.AsycudaDocument.DoNotAllocate != true
											&& z.xcuda_Item.AsycudaDocument != null
											&& z.xcuda_Item.AsycudaDocument.CNumber != null)
									&& x.PreviousDocumentItem.AsycudaDocument != null
									&& x.PreviousDocumentItem.AsycudaDocument.Cancelled != true)
						.OrderBy(x => x.EntryDataDetails.Sales.EntryDataDate)
						.ThenBy(x => x.EntryDataDetails.EntryDataDetailsId)
						.Select(x => new AllocationPi
						{
							Allocation = x,
							SalesDate = x.EntryDataDetails.Sales.EntryDataDate,
							Item_Id = x.PreviousDocumentItem.Item_Id,
							DutyFreePaid = (x.EntryDataDetails.Sales.TaxAmount == 0 ? "9070" : "4070"),
							Pi = x.PreviousDocumentItem.EntryPreviousItems.Where(p => p.xcuda_PreviousItem.xcuda_Item.AsycudaDocument.Cancelled != true
																				 &&   x.EntryDataDetails.Sales.EntryDataDate >= p.xcuda_Item.AsycudaDocument.AssessmentDate
                                                                                 && p.xcuda_PreviousItem.xcuda_Item.AsycudaDocument.Extended_customs_procedure == (x.EntryDataDetails.Sales.TaxAmount == 0?"9070": "4070"))
																			  
							.Select(p => new DatedPi
							{
								Pi = p.xcuda_PreviousItem,
								CNumber = p.xcuda_PreviousItem.xcuda_Item.AsycudaDocument.CNumber,
								pAssessmentDate = p.xcuda_PreviousItem.xcuda_Item.AsycudaDocument.AssessmentDate,
								pRegDate =
									(p.xcuda_PreviousItem.xcuda_Item.AsycudaDocument.RegistrationDate.HasValue
										? p.xcuda_Item.AsycudaDocument.RegistrationDate.Value
										: DateTime.MinValue)
							}).OrderBy(q => q.pAssessmentDate).ThenBy(q => q.pRegDate),


						})
						.Where(x => x.Pi.Any())
						.OrderBy(x => x.Pi.FirstOrDefault().pAssessmentDate)
						.ThenBy(x => x.Pi.FirstOrDefault().pRegDate)
						.ToList();
					return res;

					
				}
				catch (Exception)
				{

					throw;
				}
			}

		}


		public class DatedPi
		{
			public xcuda_PreviousItem Pi { get; set; }
			public DateTime pAssessmentDate { get; set; }
			public DateTime? pRegDate { get; set; }
			public string CNumber { get; set; }
		}

		public class AllocationPi
		{
			public AsycudaSalesAllocations Allocation { get; set; }
			public IEnumerable<DatedPi> Pi { get; set; }
			public int Item_Id { get; set; }
			public string DutyFreePaid { get; internal set; }
			public DateTime SalesDate { get; internal set; }
		}

	public class PiAllocations
	{
		public List<AsycudaSalesAllocations> Allocations { get; set; }
		public DatedPi Pi { get; set; }


	}

	private void ClearXbondAllocations()
		{
			using (var ctx = new AllocationDSContext() {StartTracking = true})
			{
				ctx.Configuration.LazyLoadingEnabled = false;
				ctx.Configuration.AutoDetectChangesEnabled = false;
				ctx.Database.ExecuteSqlCommand("DELETE FROM xBondAllocations FROM xBondAllocations INNER JOIN xcuda_Item ON xBondAllocations.xEntryItem_Id = xcuda_Item.Item_Id WHERE(xcuda_Item.IsAssessed = 1)");

			}
		}

		private async Task SetXBondLst(IEnumerable<AsycudaSalesAllocations> slst, List<xcuda_PreviousItem> plst)
		{
			try
			{

			if (plst == null || !plst.Any()) return;
			var pn = 0;
			var pitm = plst[pn];
			plst.ForEach(x => x.QtyAllocated = 0);
				
				for (int i = 0; i < slst.Count(); i++)
				{
				   
					var ssa = slst.ElementAt(i);
					
					if (slst.Skip(i).Take(3).Sum(x => x.QtyAllocated) == ssa.QtyAllocated && pitm.Suplementary_Quantity - pitm.QtyAllocated >= ssa.QtyAllocated)//
					{
						slst.Skip(i).Take(3).ForEach(async x => await SaveXbond(x, pitm).ConfigureAwait(false));
						pitm.QtyAllocated += slst.Skip(i).Take(3).Sum(x => x.QtyAllocated);
						await SavePitm(pitm).ConfigureAwait(false);
						i += 2;
						continue;
					}

					var diff = slst.Skip(i).Sum(x => x.QtyAllocated);
					if (ssa.QtyAllocated < 0)
					{
						pn -= 1;
						if (pn < 0) pn = 0;
						pitm = plst[pn];
					}                    
					if (pitm.QtyAllocated >= pitm.Suplementary_Quantity && diff > 0)
					{

						if (pn + 1 <= plst.Count - 1)
						{
							pn += 1;
							pitm = plst[pn];
							
						}
						else
						{
							return;
							// allow over allocation
						   // return;
						}

					}
					await SetXBond(ssa, pitm).ConfigureAwait(false);

                    if (slst.Skip(i).Sum(x => x.QtyAllocated) == 0 && (pitm.Suplementary_Quantity - pitm.QtyAllocated) == 0)//
                    {
                        slst.Skip(i).ForEach(async x => await SaveXbond(x, pitm).ConfigureAwait(false));
                        return;
                    }

                }
			}
			catch (Exception)
			{

				throw;
			}
		}



		private async Task SetXBond(AsycudaSalesAllocations ssa, xcuda_PreviousItem pitm)
		{
			var amt = ssa.QtyAllocated;
			var atot = pitm.Suplementary_Quantity - pitm.QtyAllocated;


			

			if (amt <= atot)
			{

				pitm.QtyAllocated += amt;

				await SaveXbond(ssa, pitm).ConfigureAwait(false);
				await SavePitm(pitm).ConfigureAwait(false);
			}
			else
			{
			    if (atot == 0) return;

				pitm.QtyAllocated += atot;
				
				var nallo = new AsycudaSalesAllocations(true)
							{
								TrackingState = TrackingState.Added,
								EntryDataDetailsId = ssa.EntryDataDetailsId,
								PreviousItem_Id = ssa.PreviousItem_Id,
								QtyAllocated = ssa.QtyAllocated - atot,
								 Status = "Ex9 Fix"
				};
				await SaveAllocation(nallo);
				ssa.QtyAllocated = atot;
				ssa.Status = "Ex9 issue";
				await SaveAllocation(ssa);
				await SaveXbond(ssa, pitm).ConfigureAwait(false);
				await SavePitm(pitm).ConfigureAwait(false);
			}


		}

		private async Task SaveXbond(AsycudaSalesAllocations ssa, xcuda_PreviousItem pitm)
		{
			var xbond = new xBondAllocations(true)
			{
				AllocationId = ssa.AllocationId,
				xEntryItem_Id = pitm.PreviousItem_Id,
				TrackingState = TrackingState.Added
			};
			await AllocationDS.DataModels.BaseDataModel.Instance.SavexBondAllocations(xbond)
				.ConfigureAwait(false);
		}

		private async Task SavePitm(xcuda_PreviousItem pitm)
		{
			var res = pitm.ChangeTracker.GetChanges().FirstOrDefault();
		   
			await AllocationDS.DataModels.BaseDataModel.Instance.Savexcuda_PreviousItem(res)
				.ConfigureAwait(false);
		}

		private async Task SaveAllocation(AsycudaSalesAllocations pitm)
		{
			var res = pitm.ChangeTracker.GetChanges().FirstOrDefault();
			
			await AllocationDS.DataModels.BaseDataModel.Instance.SaveAsycudaSalesAllocations(res)
				.ConfigureAwait(false);
		}

		public async Task ReBuildSalesReports(string id)
		{
			using (var ctx = new AllocationDSContext())
			{
				var doc = ctx.AsycudaDocument.FirstOrDefault(x => x.id == id);
				await ReBuildSalesReports(doc).ConfigureAwait(false);
			}
		}

		public async Task ReBuildSalesReports(AsycudaDocument doc)
		{

			try
			{
				if (BaseDataModel.Instance.CurrentApplicationSettings.AllowSalesToPI != "Visible") return;
				var plst = await GetPreviousItems(doc).ConfigureAwait(false);
				await ReLinkPi2Item(plst).ConfigureAwait(false);
				string dfp;
				if (doc.DocumentType == "IM4" && doc.Extended_customs_procedure != "4000")
				{
					dfp = "Duty Paid";
				}
				else
				{
					dfp = "Duty Free";
				}
			   var alst = (await GetSalesData(dfp, doc.ASYCUDA_Id).ConfigureAwait(false));
			   //await BuildSalesReport(alst, dfp).ConfigureAwait(false);//.Where(x => x.CNumber == "29635" && x.PreviousItemEx.LineNumber == 166).ToList()

			}
			catch (Exception Ex)
			{
				throw;
			}
		}

		public async Task ReBuildSalesReports()
		{
			
			try
			{
				if (BaseDataModel.Instance.CurrentApplicationSettings.AllowSalesToPI != "Visible") return;

				/////////////////////////////////////////////////////////////////////////////////////////////

				var piLst = await GetPreviousItems().ConfigureAwait(false);

				await ReLinkPi2Item(piLst).ConfigureAwait(false);


				//////////////////////////////////////////////////////////////////////////////////////////////

				//var alst = await GetSalesData("Duty Paid").ConfigureAwait(false);

				//await BuildSalesReport(alst, "Duty Paid").ConfigureAwait(false);//.Where(x => x.CNumber == "29635" && x.PreviousItemEx.LineNumber == 166).ToList()

				// var alst = (await GetSalesData("Duty Free").ConfigureAwait(false));

				ClearXbondAllocations();

				BuildSalesReport();

				//await BuildSalesReport("Duty Free").ConfigureAwait(false);

			}
			catch (Exception Ex)
			{
				throw;
			}
		}

		private async Task<List<AsycudaSalesAllocations>> GetSalesData( string dfp, int Asycuda_Id)
		{
			try
			{
				var expLst = new List<string>()
				{
					"EntryDataDetails.Sales != null",
					string.Format("EntryDataDetails.Sales.TaxAmount {0}", dfp == "Duty Free" ? "== 0" : "!= 0"),
					string.Format("EntryDataDetails.Sales.EntryDataDate >= \"{0}\" && EntryDataDetails.Sales.EntryDataDate <= \"{1}\"","5/22/2015", "5/31/2015 11:00 pm"),
					"PreviousDocumentItem != null",
					"PreviousDocumentItem.EntryPreviousItems.Any(xcuda_PreviousItem.ASYCUDA_Id == \"" + Asycuda_Id.ToString() + "\")",
				   // "PreviousDocumentItem.AsycudaDocument.CNumber == \"22699\""
				   //, "EntryDataDetails.ItemNumber = \"ASA/2247010\""
				};

				return await GetSalesData(expLst).ConfigureAwait(false);
			}
			catch (Exception)
			{

				throw;
			}
		}

		private async Task<List<AsycudaSalesAllocations>> GetSalesData(string dfp)
		{
			try
			{
				//var expLst = new List<string>()
				//{
				//    "EntryDataDetails.Sales != null",
				//    (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("EntryDataDetails.Sales.EntryDataDate >= \"{0}\"", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "EntryDataDetails.Sales.EntryDataDate >= \"1/1/2010\""),
				//    string.Format("EntryDataDetails.Sales.EntryDataDate >= \"{0}\" && EntryDataDetails.Sales.EntryDataDate <= \"{1}\"","5/22/2015", "6/30/2015 11:00 pm"),
				//    string.Format("EntryDataDetails.Sales.TaxAmount {0}", dfp == "Duty Free" ? "== 0" : "!= 0"),
				//    "PreviousDocumentItem != null",
				//    "PreviousDocumentItem.AsycudaDocument != null",
				//   // "PreviousDocumentItem.AsycudaDocument.CNumber == \"22699\""
				//   //, "EntryDataDetails.ItemNumber = \"ASA/2247010\""
				//};

				


			   // return await GetSalesData(expLst).ConfigureAwait(false);
				return null;
			}
			catch (Exception)
			{

				throw;
			}
		}

		private async Task<List<AsycudaSalesAllocations>> GetSalesData(List<string> expLst)
		{
			StatusModel.Timer("Loading Data...");
			using (var ctx = new AsycudaSalesAllocationsService())
			{
				var tot = await ctx.CountByExpressionLst(expLst).ConfigureAwait(false);
				var alst = await ctx.GetAsycudaSalesAllocationsByBatchExpressionLst(expLst, tot, new List<string>()
				{
					//"EntryDataDetails.Sales",
					//"PreviousDocumentItem.EntryPreviousItems.xcuda_PreviousItem.xcuda_Item.AsycudaDocument",
					//"xBondAllocations",
					//"PreviousDocumentItem.AsycudaDocument",
					//"PreviousDocumentItem.EntryPreviousItems.xcuda_Item.xBondAllocations"
				}, false).ConfigureAwait(false);
			   
				var res = alst.OrderBy(x => x.EntryDataDetails.Sales.EntryDataDate)
					.ThenBy(x => x.EntryDataDetails.EntryDataDetailsId)
					.ToList();

				StatusModel.Timer("Cleaning Data...");
				res.ForEach(
					x =>
						x.PreviousDocumentItem.EntryPreviousItems.Select(p => p.xcuda_PreviousItem)
							.ToList()
							.ForEach(z => z.QtyAllocated = 0));
				res.ForEach(x => x.xBondAllocations.Clear());
				return res;
			}
		}

		public async Task ReLinkPi2Item(List<xcuda_PreviousItem> piLst)
		{
			StatusModel.Timer("Getting Previous Items");

		   

			StatusModel.StartStatusUpdate("Re Linking PI to Items", piLst.Count());


		   
			var ilst = piLst.GroupBy(x => new {x.Prev_reg_nbr, x.Prev_reg_dat, x.Prev_reg_cuo, x.Previous_item_number, x.xcuda_Item});
		  
		   // foreach (var g in ilst)
			var exceptions = new ConcurrentQueue<Exception>();

			//ilst.AsParallel(new ParallelLinqOptions() {MaxDegreeOfParallelism = 1})//Environment.ProcessorCount*4
			//    .ForAll(g =>
			using (var dtx = new DocumentDSContext())
			{
				using (var ctx = new DocumentItemDSContext())
				{

					foreach (var g in ilst)
					{
						try
						{

							var bl = String.Format("{0} {1} C {2} art. {3}", g.Key.Prev_reg_cuo,
								g.Key.Prev_reg_dat,
								g.Key.Prev_reg_nbr, g.Key.Previous_item_number);

							var pLineNo = Convert.ToInt32(g.Key.Previous_item_number);
							// get document

							xcuda_ASYCUDA pdoc = null;

							pdoc = dtx.xcuda_ASYCUDA.FirstOrDefault(
								x =>
									x.xcuda_Identification.xcuda_Registration.Date != null &&
									x.xcuda_Identification.xcuda_Registration.Date.EndsWith(g.Key.Prev_reg_dat.Substring(2))
									&& x.xcuda_Identification.xcuda_Registration.Number == g.Key.Prev_reg_nbr &&
									x.xcuda_Identification.xcuda_Office_segment.Customs_clearance_office_code ==
									g.Key.Prev_reg_cuo);

							if (pdoc == null)
							{
								//MessageBox.Show(
								//    string.Format("You need to import Previous Document '{0}' before importing this Ex9 '{1}'",
								//        pi.Prev_reg_nbr, pi.xcuda_Item.AsycudaDocument.CNumber));
								return; // continue;
							}
							xcuda_Item Originalitm = null;
							if (g.Key.xcuda_Item != null)
							{
								var itmNumber = g.Key.xcuda_Item.ItemNumber;
								if(itmNumber != null && g.Key.xcuda_Item.ItemCost != 0 ) 
									Originalitm = ctx.xcuda_Item.Include(x => x.xcuda_PreviousItems).FirstOrDefault(
										x =>
										   (x.xcuda_Tarification.xcuda_HScode.Precision_4 != null && itmNumber.Contains(x.xcuda_Tarification.xcuda_HScode.Precision_4)
											 && x.LineNumber == pLineNo
											 && x.ASYCUDA_Id == pdoc.ASYCUDA_Id));
							}


							if (Originalitm != null)
							{
								var epilst = new List<CoreEntities.Business.Entities.EntryPreviousItems>();
								foreach (var pi in g)
								{
									if ( //pi.xcuda_Items.Any(x => x.Item_Id == Originalitm.Item_Id) == false &&
										Originalitm.xcuda_PreviousItems.Any(x => x.PreviousItem_Id == pi.PreviousItem_Id) ==
										false)
									{
										var epi = new CoreEntities.Business.Entities.EntryPreviousItems(true)
										{
											PreviousItem_Id = pi.PreviousItem_Id,
											Item_Id = Originalitm.Item_Id,
											TrackingState = TrackingState.Added
										};
										// await DocumentItemDS.ViewModels.BaseDataModel.Instance.SaveEntryPreviousItems(epi).ConfigureAwait(false);
										epilst.Add(epi);
										//Originalitm.PreviousItems.Add(epi);
									}
								}
								BaseDataModel.Instance.SaveEntryPreviousItems(epilst).Wait();
							}
						}


						catch
							(Exception ex)
						{
							exceptions.Enqueue(ex);
						}
					}
				}
			}
			///   });
			if (exceptions.Count > 0) throw new AggregateException(exceptions);

			// foreach (var pi in piLst
			//piLst
			//    //.Where(x => x.Prev_reg_nbr == "17353" && x.Previous_item_number == "192")

			//    .AsParallel(new ParallelLinqOptions(){MaxDegreeOfParallelism = Environment.ProcessorCount * 2})
			//    .ForAll(pi =>
			//    //  .Where(x => x.Prev_reg_nbr == "39560" && x.Previous_item_number == "25"

			//{
			//    var bl = String.Format("{0} {1} C {2} art. {3}", pi.Prev_reg_cuo,
			//        pi.Prev_reg_dat,
			//        pi.Prev_reg_nbr, pi.Previous_item_number);
			//    // find original row
			//    if (pi.PreviousItem_Id != 0)
			//    {
			//        var pLineNo = Convert.ToInt32(pi.Previous_item_number);
			//        // get document

			//        xcuda_ASYCUDA pdoc = null;

			//        pdoc = BaseDataModel._documentCache.Data.FirstOrDefault(
			//                x => DateTime.Parse(x.xcuda_Identification.xcuda_Registration.Date).Year.ToString() == pi.Prev_reg_dat 
			//                &&  x.xcuda_Identification.xcuda_Registration.Number == pi.Prev_reg_nbr &&
			//                 x.xcuda_Identification.xcuda_Office_segment.Customs_clearance_office_code == pi.Prev_reg_cuo);

			//        if (pdoc == null)
			//        {
			//            //MessageBox.Show(
			//            //    string.Format("You need to import Previous Document '{0}' before importing this Ex9 '{1}'",
			//            //        pi.Prev_reg_nbr, pi.xcuda_Item.AsycudaDocument.CNumber));
			//            return; // continue;
			//        }
			//        AsycudaDocumentItem Originalitm = null;

			//        Originalitm = BaseDataModel._documentItemCache.Data.FirstOrDefault(
			//            x =>
			//                pi.xcuda_Item != null && pi.xcuda_Item.ItemCost != null && pi.xcuda_Item.ItemNumber != null &&
			//                (x.ItemNumber != null && x.ItemNumber.Contains(
			//                    pi.xcuda_Item.ItemNumber)
			//                 && x.LineNumber == pLineNo
			//                 && x.AsycudaDocumentId == pdoc.ASYCUDA_Id));



			//        if (Originalitm != null)
			//        {
			//            if (pi.xcuda_Items.Any(x => x.Item_Id == Originalitm.Item_Id) == false &&
			//                Originalitm.PreviousItems.Any(x => x.PreviousItem_Id == pi.PreviousItem_Id) == false)
			//            {
			//                var epi = new EntryPreviousItems()
			//                {
			//                    PreviousItem_Id = pi.PreviousItem_Id,
			//                    Item_Id = Originalitm.Item_Id,
			//                    TrackingState = TrackingState.Added
			//                };
			//                // await DocumentItemDS.ViewModels.BaseDataModel.Instance.SaveEntryPreviousItems(epi).ConfigureAwait(false);
			//                DocumentItemDS.DataModels.BaseDataModel.Instance.SaveEntryPreviousItems(epi).Wait();
			//                Originalitm.PreviousItems.Add(epi);
			//            }


			//        }
			//        else
			//        {
			//            //MessageBox.Show(
			//            //    string.Format("Item Not found {0} line: {1} PrevCNumber: {2} CNumber: {3}",
			//            //        pi.xcuda_Item.EX.Precision_4, pLineNo, pdoc.xcuda_Identification.xcuda_Registration.Number,
			//            //        pi.xcuda_Item.AsycudaDocument.CNumber));
			//        }
			//    }
			//    else
			//    {
			//        throw new ApplicationException(string.Format("Item Not found {0}, LineNo:-{1}",
			//            bl, pi.Current_item_number));
			//    }
			//    StatusModel.StatusUpdate();
			//}
			//    );


		}
		private async Task<List<xcuda_PreviousItem>> GetPreviousItems(AsycudaDocument doc)
		{
			List<xcuda_PreviousItem> piLst = null;
			using (var ctx = new xcuda_PreviousItemService())
			{
				piLst =
					(await
						ctx.Getxcuda_PreviousItemByExpression("xcuda_Item.AsycudaDocument.CNumber == \"" + doc.CNumber + "\"",
							new List<string>() // && PreviousItem_Id == 294486
							{
								"xcuda_Item.AsycudaDocument",
								"xcuda_Item.xcuda_Tarification.xcuda_HScode"
							}).ConfigureAwait(false)).ToList();
			}
			return piLst;
		}
		private async Task<List<xcuda_PreviousItem>> GetPreviousItems()
		{
			List<xcuda_PreviousItem> piLst = null;
			using (var ctx = new xcuda_PreviousItemService())
			{
				piLst =
					(await
						ctx.Getxcuda_PreviousItemByExpression("xcuda_Item.AsycudaDocument.CNumber != null && " + (BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate.HasValue ? string.Format("xcuda_Item.AsycudaDocument.AssessmentDate >= \"{0}\"", BaseDataModel.Instance.CurrentApplicationSettings.OpeningStockDate) : "xcuda_Item.AsycudaDocument.AssessmentDate >= \"1/1/2010\""),
							new List<string>() // && PreviousItem_Id == 294486
							{
								"xcuda_Item.AsycudaDocument",
								"xcuda_Item.xcuda_Tarification.xcuda_HScode"
							}).ConfigureAwait(false)).ToList();
			}
			return piLst;
		}
	}
}