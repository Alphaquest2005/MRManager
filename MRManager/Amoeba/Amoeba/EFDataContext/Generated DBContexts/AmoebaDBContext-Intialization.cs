﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Data;
using EF.Entities;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace EF.DBContexts
{
	public partial class AmoebaDBContext
	{
		private static readonly AmoebaDBContext _instance = new AmoebaDBContext();

		public static AmoebaDBContext Instance => _instance;

		static AmoebaDBContext()
		{
			Instance.Database.Migrate();
		}
               
			
		
	}
}
