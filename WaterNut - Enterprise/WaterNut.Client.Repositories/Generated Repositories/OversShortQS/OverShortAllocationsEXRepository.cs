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
using OversShortQS.Client.Services;
using OversShortQS.Client.Entities;
using OversShortQS.Client.DTO;
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

using OverShortAllocationsEX = OversShortQS.Client.Entities.OverShortAllocationsEX;

namespace OversShortQS.Client.Repositories 
{
   
    public partial class OverShortAllocationsEXRepository : BaseRepository<OverShortAllocationsEXRepository>
    {

       private static readonly OverShortAllocationsEXRepository instance;
       static OverShortAllocationsEXRepository()
        {
            instance = new OverShortAllocationsEXRepository();
        }

       public static OverShortAllocationsEXRepository Instance
        {
            get { return instance; }
        }
        
        public async Task<IEnumerable<OverShortAllocationsEX>> OverShortAllocationsEXes(List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime) return new List<OverShortAllocationsEX>().AsEnumerable();
            try
            {
                using (var t = new OverShortAllocationsEXClient())
                    {
                        var res = await t.GetOverShortAllocationsEXes(includesLst).ConfigureAwait(continueOnCapturedContext: false);
                        if (res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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

		 public async Task<IEnumerable<OverShortAllocationsEX>> GetOverShortAllocationsEXesByExpression(string exp, List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || exp == null || exp == "None") return new List<OverShortAllocationsEX>().AsEnumerable();
            try
            {
                using (var t = new OverShortAllocationsEXClient())
                    {
					    IEnumerable<DTO.OverShortAllocationsEX> res = null;
                        if(exp == "All")
                        {                       
						    res = await t.GetOverShortAllocationsEXes(includesLst).ConfigureAwait(continueOnCapturedContext: false);					
                        }
                        else
                        {
                             res = await t.GetOverShortAllocationsEXesByExpression(exp, includesLst).ConfigureAwait(continueOnCapturedContext: false);	                         
                        }
                    
                        if (res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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

		 public async Task<IEnumerable<OverShortAllocationsEX>> GetOverShortAllocationsEXesByExpressionLst(List<string> expLst, List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || expLst.Count == 0 || expLst.FirstOrDefault() == "None") return new List<OverShortAllocationsEX>().AsEnumerable();
            try
            {
                using (var t = new OverShortAllocationsEXClient())
                    {
					    IEnumerable<DTO.OverShortAllocationsEX> res = null;
                       
                        res = await t.GetOverShortAllocationsEXesByExpressionLst(expLst, includesLst).ConfigureAwait(continueOnCapturedContext: false);	                         
                      
                    
                        if (res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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


		 public async Task<IEnumerable<OverShortAllocationsEX>> GetOverShortAllocationsEXesByExpressionNav(string exp, Dictionary<string, string> navExp, List<string> includesLst = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || exp == null || exp == "None") return new List<OverShortAllocationsEX>().AsEnumerable();
            try
            {
                using (var t = new OverShortAllocationsEXClient())
                    {
					    IEnumerable<DTO.OverShortAllocationsEX> res = null;
                        if(exp == "All" && navExp.Count == 0)
                        {                       
						    res = await t.GetOverShortAllocationsEXes(includesLst).ConfigureAwait(continueOnCapturedContext: false);					
                        }
                        else
                        {
                             res = await t.GetOverShortAllocationsEXesByExpressionNav(exp, navExp, includesLst).ConfigureAwait(continueOnCapturedContext: false);	                         
                        }
                    
                        if (res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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


        public async Task<OverShortAllocationsEX> GetOverShortAllocationsEX(string id, List<string> includesLst = null)
        {
             try
             {   
                 using (var t = new OverShortAllocationsEXClient())
                    {
                        var res = await t.GetOverShortAllocationsEXByKey(id,includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return new OverShortAllocationsEX(res)
                    {
                  // OverShortDetailAllocation = (res.OverShortDetailAllocation != null?new OverShortDetailAllocation(res.OverShortDetailAllocation): null),    
                  // OverShortDetailsEX = (res.OverShortDetailsEX != null?new OverShortDetailsEX(res.OverShortDetailsEX): null),    
                  // AsycudaDocumentItem = (res.AsycudaDocumentItem != null?new AsycudaDocumentItem(res.AsycudaDocumentItem): null)    
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

        public async Task<OverShortAllocationsEX> UpdateOverShortAllocationsEX(OverShortAllocationsEX entity)
        {
            if (entity == null) return entity;
            var entitychanges = entity.ChangeTracker.GetChanges().FirstOrDefault();
            if (entitychanges != null)
            {
                try
                {
                    using (var t = new OverShortAllocationsEXClient())
                    {
     
                        var updatedEntity =  await t.UpdateOverShortAllocationsEX(entitychanges).ConfigureAwait(false);
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

        public async Task<OverShortAllocationsEX> CreateOverShortAllocationsEX(OverShortAllocationsEX entity)
        {
            try
            {   
                using (var t = new OverShortAllocationsEXClient())
                    {
                        return new OverShortAllocationsEX(await t.CreateOverShortAllocationsEX(entity.DTO).ConfigureAwait(continueOnCapturedContext: false));
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

        public async Task<bool> DeleteOverShortAllocationsEX(string id)
        {
            try
            {
             using (var t = new OverShortAllocationsEXClient())
                {
                    return await t.DeleteOverShortAllocationsEX(id).ConfigureAwait(continueOnCapturedContext: false);
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

		public async Task<Tuple<IEnumerable<OverShortAllocationsEX>, int>> LoadRange(int startIndex, int count, string exp, Dictionary<string, string> navExp, IEnumerable<string> includeLst = null)
        {
			var overallCount = 0;
            if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime || exp == null || exp == "None")
            {
                
                return new Tuple<IEnumerable<OverShortAllocationsEX>, int>(new List<OverShortAllocationsEX>().AsEnumerable(), overallCount);
            }
            
            try
            {
                using (var t = new OverShortAllocationsEXClient())
                {

                    IEnumerable<DTO.OverShortAllocationsEX> res = null;
                                         
						    res = await t.LoadRangeNav(startIndex, count, exp, navExp, includeLst).ConfigureAwait(continueOnCapturedContext: false);
						    overallCount = await t.CountNav(exp, navExp).ConfigureAwait(continueOnCapturedContext: false);
                   
                   
                                
                    if (res != null)
                    {
                        return new Tuple<IEnumerable<OverShortAllocationsEX>, int>(res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable(), overallCount);
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

	 public async Task<IEnumerable<OverShortAllocationsEX>> GetOverShortAllocationsEXByOverShortDetailId(string OverShortDetailId, List<string> includesLst = null)
        {
             if (OverShortDetailId == "0") return null;
            try
            {
                 using (OverShortAllocationsEXClient t = new OverShortAllocationsEXClient())
                    {
                        var res = await t.GetOverShortAllocationsEXByOverShortDetailId(OverShortDetailId, includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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
 	 public async Task<IEnumerable<OverShortAllocationsEX>> GetOverShortAllocationsEXByOversShortsId(string OversShortsId, List<string> includesLst = null)
        {
             if (OversShortsId == "0") return null;
            try
            {
                 using (OverShortAllocationsEXClient t = new OverShortAllocationsEXClient())
                    {
                        var res = await t.GetOverShortAllocationsEXByOversShortsId(OversShortsId, includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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
 	 public async Task<IEnumerable<OverShortAllocationsEX>> GetOverShortAllocationsEXByItem_Id(string Item_Id, List<string> includesLst = null)
        {
             if (Item_Id == "0") return null;
            try
            {
                 using (OverShortAllocationsEXClient t = new OverShortAllocationsEXClient())
                    {
                        var res = await t.GetOverShortAllocationsEXByItem_Id(Item_Id, includesLst).ConfigureAwait(continueOnCapturedContext: false);
                         if(res != null)
                        {
                            return res.Select(x => new OverShortAllocationsEX(x)).AsEnumerable();
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
                using (var t = new OverShortAllocationsEXClient())
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
                using (var t = new OverShortAllocationsEXClient())
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
