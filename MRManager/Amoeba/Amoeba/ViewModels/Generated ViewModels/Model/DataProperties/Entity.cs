﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using Core.Common.UI;
using EF.Entities;
using DataInterfaces;
using Interfaces;
using Utilities;

namespace ViewModels
{
	public partial class DataPropertiesViewModel_AutoGen 
	{
		public int DataTypeId  { get { return GetValue(); } set { SetValue(value); }
	    }
		public int EntityPropertyId  { get { return GetValue(); } set { SetValue(value); }
	    }
		public int Id  { get { return GetValue(); } set { SetValue(value); }
	    }
		public int MaxLength  { get { return GetValue(); } set { SetValue(value); }
	    }
		public int ModelTypeId  { get { return GetValue(); } set { SetValue(value); }
	    }

		protected override IDataProperties CreateEntity()
		{
			return new DataProperties() 
					{
						DataTypeId = BaseViewModel.CurrentDataTypes.Id, 
						EntityPropertyId = BaseViewModel.CurrentEntityProperties.Id, 
						ModelTypeId = BaseViewModel.CurrentModelTypes.Id, 
						RowState = DataInterfaces.RowState.Added
					};
		}
		protected override sealed IDataProperties CreateNullEntity()
		{
			return new DataProperties(){ Id = EntityStates.NullEntity };
		}
	
	}
}
