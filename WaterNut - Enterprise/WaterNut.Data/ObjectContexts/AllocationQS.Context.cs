﻿// <autogenerated>
//   This file was generated by T4 code generator AllObjectContext.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

            


using System.Data.Entity;
using CoreEntities.Business.Entities;
using System.Data.Entity.Infrastructure;
using AllocationQS.Business.Entities.Mapping;
using WaterNut.Data;
using System.Data.Entity.Core.Objects;
using TrackableEntities;


namespace AllocationQS.Business.Entities
{
    [DbConfigurationType(typeof(DBConfiguration))] 
    public partial class AllocationQSContext : DbContext
    {
        static AllocationQSContext()
        {
            var x = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            Database.SetInitializer<AllocationQSContext>(null);
        }

        public AllocationQSContext()
            : base("Name=AllocationQS")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
               // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 120;

            objectContext.ObjectMaterialized += ObjectContext_OnObjectMaterialized;
        }
        
        public bool StartTracking { get; set; }

        private void ObjectContext_OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (StartTracking == true) ((dynamic)e.Entity).StartTracking();
        }

        public DbSet<AsycudaSalesAllocationsEx> AsycudaSalesAllocationsExs { get; set; }
     


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AsycudaSalesAllocationsExMap());
         
			OnModelCreatingExtentsion(modelBuilder);

        }
		partial void OnModelCreatingExtentsion(DbModelBuilder modelBuilder);
    }
}

 	
