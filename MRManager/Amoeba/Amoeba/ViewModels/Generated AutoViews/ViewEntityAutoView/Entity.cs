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
	public partial class ViewEntityAutoViewModel_AutoGen 
	{
		public object EntityView  { get { return GetValue(); } set { SetValue(value); }}
		public object EntityProperties  { get { return GetValue(); } set { SetValue(value); }}
		public object Entities  { get { return GetValue(); } set { SetValue(value); }}
		
		protected sealed override IViewEntityAutoView CreateNullEntity()
		{
			return new ViewEntityAutoView(){Id = EntityStates.NullEntity};
		}
	
	}
}
