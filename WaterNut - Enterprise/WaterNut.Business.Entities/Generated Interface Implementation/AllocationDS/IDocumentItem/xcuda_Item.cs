﻿// <autogenerated>
//   This file was generated by T4 code generator DomainInterface.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WaterNut.Interfaces;



namespace AllocationDS.Business.Entities
{
		public partial class xcuda_Item: IDocumentItem //AllocationDS
		{  // please don't expect properties here, they are implict, only multilayer will appear here
                 [IgnoreDataMember]
                 [NotMapped]
                 public String ItemNumber 
                {
                    get{ return this.xcuda_Tarification.xcuda_HScode.Precision_4; }                
                    set { this.xcuda_Tarification.xcuda_HScode.Precision_4 = value;}
                }

		    [IgnoreDataMember]
		    [NotMapped]
		    public String ItemDescription
		    {
		        get
		        {
		            try
		            {
                        return this.xcuda_Goods_description.Commercial_Description;
		            }
		            catch (Exception e)
		            {
		                Console.WriteLine(e);
		                throw;
		            }
		            
		        }
		        set { this.xcuda_Goods_description.Commercial_Description = value; }
		    }

    }
}

