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
	public class PatientReligonMap
	{
		public static void Map(EntityTypeBuilder<PatientReligon> entityBuilder)
		{
			entityBuilder.ToTable("PatientReligon", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.PersonId).HasColumnName("PersonId").IsRequired();
			entityBuilder.Property(t => t.ReligionId).HasColumnName("ReligionId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Persons_Patient).WithMany(p => p.PatientReligon).HasForeignKey(c => c.PersonId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
