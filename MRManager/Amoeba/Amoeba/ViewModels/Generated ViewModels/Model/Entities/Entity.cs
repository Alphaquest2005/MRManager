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
	public partial class EntitiesViewModel_AutoGen 
	{
		public int Id  { get { return GetValue(); } set { SetValue(value); }
	    }
		public string EntitySetName  { get { return GetValue(); } set { SetValue(value); }
	    }
		public string Name  { get { return GetValue(); } set { SetValue(value); }
	    }
		public string SchemaName  { get { return GetValue(); } set { SetValue(value); }
	    }

		protected override IEntities CreateEntity()
		{
			return new Entities() 
					{
						RowState = DataInterfaces.RowState.Added
					};
		}
		protected override sealed IEntities CreateNullEntity()
		{
			return new Entities(){ Id = EntityStates.NullEntity };
		}
	
	}
}
