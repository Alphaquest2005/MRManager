﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class FunctionReturnTypeMap
	{
		public static void Map(EntityTypeBuilder<FunctionReturnType> entityBuilder)
		{
			entityBuilder.ToTable("FunctionReturnType", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedNever();	
			entityBuilder.Property(t => t.DataTypeId).HasColumnName("DataTypeId").IsRequired();
			entityBuilder.Property(t => t.Id).HasColumnName("Id").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.DataTypes).WithMany(p => p.FunctionReturnType).HasForeignKey(c => c.DataTypeId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.Functions).WithOne(p => p.FunctionReturnType).HasForeignKey<Functions>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
