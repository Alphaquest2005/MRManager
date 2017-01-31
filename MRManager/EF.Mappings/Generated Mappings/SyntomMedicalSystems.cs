﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class SyntomMedicalSystemsMap
	{
		public static void Map(EntityTypeBuilder<SyntomMedicalSystems> entityBuilder)
		{
			entityBuilder.ToTable("SyntomMedicalSystems", "Interview");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.MedicalSystemId).HasColumnName("MedicalSystemId").IsRequired();
			entityBuilder.Property(t => t.SyntomId).HasColumnName("SyntomId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.MedicalSystems).WithMany(p => p.SyntomMedicalSystems).HasForeignKey(c => c.MedicalSystemId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.Syntoms).WithMany(p => p.SyntomMedicalSystems).HasForeignKey(c => c.SyntomId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}