﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Interfaces.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;


namespace Interfaces
{
	[InheritedExport]
	public partial interface IExamResults_AnioticFluid:IEntity  
	{
		double MVP { get;}
		double AFI { get;}
		double Q1 { get;}
		double Q2 { get;}
		double Q3 { get;}
		double Q4 { get;}



	}
}
