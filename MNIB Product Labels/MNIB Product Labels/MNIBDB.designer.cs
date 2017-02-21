﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MNIB_Product_Labels
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MNIBLabelsDB")]
	public partial class MNIBDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPurchaseOrder(PurchaseOrder instance);
    partial void UpdatePurchaseOrder(PurchaseOrder instance);
    partial void DeletePurchaseOrder(PurchaseOrder instance);
    #endregion
		
		public MNIBDBDataContext() : 
				base(global::MNIB_Product_Labels.Properties.Settings.Default.MNIBLabelsDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MNIBDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<PurchaseOrder> PurchaseOrders
		{
			get
			{
				return this.GetTable<PurchaseOrder>();
			}
		}
		
		public System.Data.Linq.Table<PurchaseOrderDetail> PurchaseOrderDetails
		{
			get
			{
				return this.GetTable<PurchaseOrderDetail>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PurchaseOrder")]
	public partial class PurchaseOrder : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _PONumber;
		
		private System.DateTime _PODate;
		
		private string _Vendor;
		
		private string _VendorNo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPONumberChanging(string value);
    partial void OnPONumberChanged();
    partial void OnPODateChanging(System.DateTime value);
    partial void OnPODateChanged();
    partial void OnVendorChanging(string value);
    partial void OnVendorChanged();
    partial void OnVendorNoChanging(string value);
    partial void OnVendorNoChanged();
    #endregion
		
		public PurchaseOrder()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PONumber", DbType="VarChar(15) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string PONumber
		{
			get
			{
				return this._PONumber;
			}
			set
			{
				if ((this._PONumber != value))
				{
					this.OnPONumberChanging(value);
					this.SendPropertyChanging();
					this._PONumber = value;
					this.SendPropertyChanged("PONumber");
					this.OnPONumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PODate", DbType="DateTime NOT NULL")]
		public System.DateTime PODate
		{
			get
			{
				return this._PODate;
			}
			set
			{
				if ((this._PODate != value))
				{
					this.OnPODateChanging(value);
					this.SendPropertyChanging();
					this._PODate = value;
					this.SendPropertyChanged("PODate");
					this.OnPODateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Vendor", DbType="VarChar(40)")]
		public string Vendor
		{
			get
			{
				return this._Vendor;
			}
			set
			{
				if ((this._Vendor != value))
				{
					this.OnVendorChanging(value);
					this.SendPropertyChanging();
					this._Vendor = value;
					this.SendPropertyChanged("Vendor");
					this.OnVendorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VendorNo", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string VendorNo
		{
			get
			{
				return this._VendorNo;
			}
			set
			{
				if ((this._VendorNo != value))
				{
					this.OnVendorNoChanging(value);
					this.SendPropertyChanging();
					this._VendorNo = value;
					this.SendPropertyChanged("VendorNo");
					this.OnVendorNoChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PurchaseOrderDetails")]
	public partial class PurchaseOrderDetail
	{
		
		private string _PurchaseOrderNo;
		
		private int _LineNumber;
		
		private string _ItemNumber;
		
		private string _ItemDescription;
		
		private decimal _Quantity;
		
		private string _Unit;
		
		private decimal _Cost;
		
		public PurchaseOrderDetail()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PurchaseOrderNo", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string PurchaseOrderNo
		{
			get
			{
				return this._PurchaseOrderNo;
			}
			set
			{
				if ((this._PurchaseOrderNo != value))
				{
					this._PurchaseOrderNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LineNumber", DbType="Int NOT NULL")]
		public int LineNumber
		{
			get
			{
				return this._LineNumber;
			}
			set
			{
				if ((this._LineNumber != value))
				{
					this._LineNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ItemNumber", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string ItemNumber
		{
			get
			{
				return this._ItemNumber;
			}
			set
			{
				if ((this._ItemNumber != value))
				{
					this._ItemNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ItemDescription", DbType="VarChar(50)")]
		public string ItemDescription
		{
			get
			{
				return this._ItemDescription;
			}
			set
			{
				if ((this._ItemDescription != value))
				{
					this._ItemDescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Quantity", DbType="Decimal(15,4) NOT NULL")]
		public decimal Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if ((this._Quantity != value))
				{
					this._Quantity = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Unit", DbType="VarChar(15)")]
		public string Unit
		{
			get
			{
				return this._Unit;
			}
			set
			{
				if ((this._Unit != value))
				{
					this._Unit = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cost", DbType="Decimal(15,2) NOT NULL")]
		public decimal Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				if ((this._Cost != value))
				{
					this._Cost = value;
				}
			}
		}
	}
}
#pragma warning restore 1591