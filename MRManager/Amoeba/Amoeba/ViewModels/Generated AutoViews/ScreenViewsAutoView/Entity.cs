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
	public partial class ScreenViewsAutoViewModel_AutoGen 
	{
		public object Screens  { get { return GetValue(); } set { SetValue(value); }}
		
		protected sealed override IScreenViewsAutoView CreateNullEntity()
		{
			return new ScreenViewsAutoView(){Id = EntityStates.NullEntity};
		}
	
	}
}
