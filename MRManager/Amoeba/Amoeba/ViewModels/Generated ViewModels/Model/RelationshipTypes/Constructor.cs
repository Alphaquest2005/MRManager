﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.ComponentModel;
using Core.Common.UI;
using Interfaces;
using DesignTime;
using ValidationSets;

namespace ViewModels
{
	public partial class RelationshipTypesViewModel_AutoGen : BaseViewModel<IRelationshipTypes>
	{
		public RelationshipTypesViewModel_AutoGen(): base(new IRelationshipTypesValidator())
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				CurrentEntity = DesignDataContext.RelationshipTypes;
				base.EntitySet.Add(DesignDataContext.RelationshipTypes);
			}
			else
			{
				CurrentEntity = CreateNullEntity();
			}
			OnCreated();        
			OnTotals();
		}
			
		partial void OnCreated();
		partial void OnTotals();
	}
}
