﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Interfaces
{
	[InheritedExport]
	public partial interface IDataPropertiesAutoView:DataInterfaces.IEntity  
	{
		string Parameters { get;}
		string DataTypes { get;}
		string PresentationProperties { get;}
		string TestValues { get;}
		string Entities { get;}
		string EntityProperties { get;}
		string ModelTypes { get;}



	}
}
