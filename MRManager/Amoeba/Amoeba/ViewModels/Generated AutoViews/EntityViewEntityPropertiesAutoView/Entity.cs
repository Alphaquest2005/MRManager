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
	public partial class EntityViewEntityPropertiesAutoViewModel_AutoGen 
	{
		public object PresentationProperties  { get { return GetValue(); } set { SetValue(value); }}
		public object TestValues  { get { return GetValue(); } set { SetValue(value); }}
		public object Entities  { get { return GetValue(); } set { SetValue(value); }}
		public object EntityProperties  { get { return GetValue(); } set { SetValue(value); }}
		
		protected sealed override IEntityViewEntityPropertiesAutoView CreateNullEntity()
		{
			return new EntityViewEntityPropertiesAutoView(){Id = EntityStates.NullEntity};
		}
	
	}
}
