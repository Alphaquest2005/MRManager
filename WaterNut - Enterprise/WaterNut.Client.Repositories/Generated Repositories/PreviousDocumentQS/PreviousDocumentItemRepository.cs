﻿// <autogenerated>
//   This file was generated by T4 code generator AllRepository.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using TrackableEntities.Common;
using Core.Common.Client.Services;
using Core.Common.Client.Repositories;
using PreviousDocumentQS.Client.Services;
using PreviousDocumentQS.Client.Entities;
using PreviousDocumentQS.Client.DTO;
using Core.Common.Business.Services;
using System.Diagnostics;
using TrackableEntities.Client;


using System.Threading.Tasks;
using System.Linq;
using Core.Common;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.ServiceModel;

using PreviousDocumentItem = PreviousDocumentQS.Client.Entities.PreviousDocumentItem;

namespace PreviousDocumentQS.Client.Repositories 
{
   
    public partial class PreviousDocumentItemRepository : BaseRepository<PreviousDocumentItemRepository>
    {

       private static readonly PreviousDocumentItemRepository instance;
       static PreviousDocumentItemRepository()
        {
            instance = new PreviousDocumentItemRepository();
        }

       public static PreviousDocumentItemRepository Instance
        {
            get { return instance; }
        }
        
        public async Task<IEnumerable<PreviousDocumentItem>> PreviousDocumentItems(List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime) return new List<PreviousDocumentItem>().AsEnumerable();
            try
            {
                using (var t = new PreviousDocumentItemClient())
                    {
                        var res = await t.GetPreviousDocumentItems(includesLst).ConfigureAwait(continueOnCapturedContext: false);
                        if (res != null)
                        {
                            return res.Select(x => new PreviousDocumentItem(x)).AsEnumerable();
                        }
                        else
                        {
                            return null;
                        }                    
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

		 public async Task<IEnumerable<PreviousDocumentItem>> GetPreviousDocumentItemsByExpression(string exp, List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || exp == null || exp == "None") return new List<PreviousDocumentItem>().AsEnumerable();
            try
            {
                using (var t = new PreviousDocumentItemClient())
                    {
					    IEnumerable<DTO.PreviousDocumentItem> res = null;
                        if(exp == "All")
                        {                       
						    res = await t.GetPreviousDocumentItems(includesLst).ConfigureAwait(continueOnCapturedContext: false);					
                        }
                        else
                        {
                             res = await t.GetPreviousDocumentItemsByExpression(exp, includesLst).ConfigureAwait(continueOnCapturedContext: false);	                         
                        }
                    
                        if (res != null)
                        {
                            return res.Select(x => new PreviousDocumentItem(x)).AsEnumerable();
                        }
                        else
                        {
                            return null;
                        }                    
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

		 public async Task<IEnumerable<PreviousDocumentItem>> GetPreviousDocumentItemsByExpressionLst(List<string> expLst, List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || expLst.Count == 0 || expLst.FirstOrDefault() == "None") return new List<PreviousDocumentItem>().AsEnumerable();
            try
            {
                using (var t = new PreviousDocumentItemClient())
                    {
					    IEnumerable<DTO.PreviousDocumentItem> res = null;
                       
                        res = await t.GetPreviousDocumentItemsByExpressionLst(expLst, includesLst).ConfigureAwait(continueOnCapturedContext: false);	                         
                      
                    
                        if (res != null)
                        {
                            return res.Select(x => new PreviousDocumentItem(x)).AsEnumerable();
                        }
                        else
                        {
                            return null;
                        }                    
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }


		 public async Task<IEnumerable<PreviousDocumentItem>> GetPreviousDocumentItemsByExpressionNav(string exp, Dictionary<string, string> navExp, List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || exp == null || exp == "None") return new List<PreviousDocumentItem>().AsEnumerable();
            try
            {
                using (var t = new PreviousDocumentItemClient())
                    {
					    IEnumerable<DTO.PreviousDocumentItem> res = null;
                        if(exp == "All" && navExp.Count == 0)
                        {                       
						    res = await t.GetPreviousDocumentItems(includesLst).ConfigureAwait(continueOnCapturedContext: false);					
                        }
                        else
                        {
                             res = await t.GetPreviousDocumentItemsByExpressionNav(exp, navExp, includesLst).ConfigureAwait(continueOnCapturedContext: false);	                         
                        }
                    
                        if (res != null)
                        {
                            return res.Select(x => new PreviousDocumentItem(x)).AsEnumerable();
                        }
                        else
                        {
                            return null;
                        }                    
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }


        public async Task<PreviousDocumentItem> GetPreviousDocumentItem(string id, List<string> includesLst = null)
        {
             try
             {   
                 using (var t = new PreviousDocumentItemClient())
                    {
                        var res = await t.GetPreviousDocumentItemByKey(id,includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return new PreviousDocumentItem(res)
                    {
                  // PreviousDocument = (res.PreviousDocument != null?new PreviousDocument(res.PreviousDocument): null),    
                     // PreviousItemsExes = new System.Collections.ObjectModel.ObservableCollection<PreviousItemsEx>(res.PreviousItemsExes.Select(y => new PreviousItemsEx(y))),    
                     // PreviousItemEx = new System.Collections.ObjectModel.ObservableCollection<PreviousItemsEx>(res.PreviousItemEx.Select(y => new PreviousItemsEx(y)))    
                  };
                    }
                    else
                    {
                        return null;
                    }                    
                }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

        public async Task<PreviousDocumentItem> UpdatePreviousDocumentItem(PreviousDocumentItem entity)
        {
            if (entity == null) return entity;
            var entitychanges = entity.ChangeTracker.GetChanges().FirstOrDefault();
            if (entitychanges != null)
            {
                try
                {
                    using (var t = new PreviousDocumentItemClient())
                    {
     
                        var updatedEntity =  await t.UpdatePreviousDocumentItem(entitychanges).ConfigureAwait(false);
                        entity.EntityId = updatedEntity.EntityId;
                        entity.DTO.AcceptChanges();
                         //var  = entity.;
                        //entity.ChangeTracker.MergeChanges(,updatedEntity);
                        //entity. = ;
                        return entity;
                    }
                }
                catch (FaultException<ValidationFault> e)
                {
                    throw new Exception(e.Detail.Message, e.InnerException);
                }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
            }
            else
            {
                return entity;
            }

        }

        public async Task<PreviousDocumentItem> CreatePreviousDocumentItem(PreviousDocumentItem entity)
        {
            try
            {   
                using (var t = new PreviousDocumentItemClient())
                    {
                        return new PreviousDocumentItem(await t.CreatePreviousDocumentItem(entity.DTO).ConfigureAwait(continueOnCapturedContext: false));
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

        public async Task<bool> DeletePreviousDocumentItem(string id)
        {
            try
            {
             using (var t = new PreviousDocumentItemClient())
                {
                    return await t.DeletePreviousDocumentItem(id).ConfigureAwait(continueOnCapturedContext: false);
                }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }  
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }         
        }


		//Virtural List Implementation

		public async Task<Tuple<IEnumerable<PreviousDocumentItem>, int>> LoadRange(int startIndex, int count, string exp, Dictionary<string, string> navExp, IEnumerable<string> includeLst = null)
        {
			var overallCount = 0;
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || exp == null || exp == "None")
            {
                
                return new Tuple<IEnumerable<PreviousDocumentItem>, int>(new List<PreviousDocumentItem>().AsEnumerable(), overallCount);
            }
            
            try
            {
                using (var t = new PreviousDocumentItemClient())
                {

                    IEnumerable<DTO.PreviousDocumentItem> res = null;
                                         
						    res = await t.LoadRangeNav(startIndex, count, exp, navExp, includeLst).ConfigureAwait(continueOnCapturedContext: false);
						    overallCount = await t.CountNav(exp, navExp).ConfigureAwait(continueOnCapturedContext: false);
                   
                   
                                
                    if (res != null)
                    {
                        return new Tuple<IEnumerable<PreviousDocumentItem>, int>(res.Select(x => new PreviousDocumentItem(x)).AsEnumerable(), overallCount);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        }

	 public async Task<IEnumerable<PreviousDocumentItem>> GetPreviousDocumentItemByASYCUDA_Id(string ASYCUDA_Id, List<string> includesLst = null)
        {
             if (ASYCUDA_Id == "0") return null;
            try
            {
                 using (PreviousDocumentItemClient t = new PreviousDocumentItemClient())
                    {
                        var res = await t.GetPreviousDocumentItemByASYCUDA_Id(ASYCUDA_Id, includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return res.Select(x => new PreviousDocumentItem(x)).AsEnumerable();
					    }                
					    else
					    {
						    return null;
					    }                    
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        } 
 	 public async Task<IEnumerable<PreviousDocumentItem>> GetPreviousDocumentItemByEntryDataDetailsId(string EntryDataDetailsId, List<string> includesLst = null)
        {
             if (EntryDataDetailsId == "0") return null;
            try
            {
                 using (PreviousDocumentItemClient t = new PreviousDocumentItemClient())
                    {
                        var res = await t.GetPreviousDocumentItemByEntryDataDetailsId(EntryDataDetailsId, includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return res.Select(x => new PreviousDocumentItem(x)).AsEnumerable();
					    }                
					    else
					    {
						    return null;
					    }                    
                    }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }
        } 
         
		public decimal SumField(string whereExp, string sumExp)
        {
            try
            {
                using (var t = new PreviousDocumentItemClient())
                {
                    return t.SumField(whereExp,sumExp);
                }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }

        }

        public async Task<decimal> SumNav(string whereExp, Dictionary<string, string> navExp, string sumExp)
        {
            try
            {
                using (var t = new PreviousDocumentItemClient())
                {
                    return await t.SumNav(whereExp,navExp,sumExp).ConfigureAwait(false);
                }
            }
            catch (FaultException<ValidationFault> e)
            {
                throw new Exception(e.Detail.Message, e.InnerException);
            }
            catch (Exception)
            {
                Debugger.Break();
                throw;
            }

        }
    }
}
