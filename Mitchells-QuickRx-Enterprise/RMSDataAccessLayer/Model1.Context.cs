﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMSDataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RMSModel : DbContext
    {
        public RMSModel()
            : base("name=RMSModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<TransactionBase> TransactionBase { get; set; }
        public virtual DbSet<TransactionEntryBase> TransactionEntryBase { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<CashierLog> CashierLogs { get; set; }
        public virtual DbSet<QBInventoryItem> QBInventoryItems { get; set; }
        public virtual DbSet<TransactionsView> TransactionsViews { get; set; }
        public virtual DbSet<TransactionEntryItem> TransactionEntryItems { get; set; }
        public virtual DbSet<ItemDosage> ItemDosages { get; set; }
    }
}
