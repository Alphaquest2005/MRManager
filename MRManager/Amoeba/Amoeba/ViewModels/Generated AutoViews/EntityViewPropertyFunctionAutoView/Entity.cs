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
	public partial class EntityViewPropertyFunctionAutoViewModel_AutoGen 
	{
		public object EntityView  { get { return GetValue(); } set { SetValue(value); }}
		public object EntityViewProperties  { get { return GetValue(); } set { SetValue(value); }}
		public object Functions  { get { return GetValue(); } set { SetValue(value); }}
		public object EntityViewPropertyFunctionParameter  { get { return GetValue(); } set { SetValue(value); }}
		
		protected sealed override IEntityViewPropertyFunctionAutoView CreateNullEntity()
		{
			return new EntityViewPropertyFunctionAutoView(){Id = EntityStates.NullEntity};
		}
	
	}
}
