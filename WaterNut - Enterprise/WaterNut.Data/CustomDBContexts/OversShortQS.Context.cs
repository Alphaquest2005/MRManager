﻿// <autogenerated>
//   This file was generated by T4 code generator ObjectContext.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

            


using System.Data.Entity;
using CoreEntities.Business.Entities;
using OversShortQS.Business.Entities.Mapping;


namespace OversShortQS.Business.Entities
{
    public partial class OversShortQSContext 
    {

        partial void OnModelCreatingExtentsion(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OverShortDetailsEX>().ToTable("OverShortDetailsEX");
            modelBuilder.Entity<OversShortEX>().ToTable("OversShortEX");
        }
	
    }
}

 	
