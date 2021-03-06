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
	public class PatientSyntomsMap
	{
		public static void Map(EntityTypeBuilder<PatientSyntoms> entityBuilder)
		{
			entityBuilder.ToTable("PatientSyntoms", "Interview");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.SyntomId).HasColumnName("SyntomId").IsRequired();
			entityBuilder.Property(t => t.PatientVisitId).HasColumnName("PatientVisitId").IsRequired();
			entityBuilder.Property(t => t.PriorityId).HasColumnName("PriorityId").IsRequired();
			entityBuilder.Property(t => t.StatusId).HasColumnName("StatusId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
				entityBuilder.HasMany(x => x.PatientResponses).WithOne(p => p.PatientSyntoms).HasForeignKey(c => c.PatientSyntomId).OnDelete(DeleteBehavior.Restrict);
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Syntoms).WithMany(p => p.PatientSyntoms).HasForeignKey(c => c.SyntomId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.PatientVisit).WithMany(p => p.PatientSyntoms).HasForeignKey(c => c.PatientVisitId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.SyntomPriority).WithMany(p => p.PatientSyntoms).HasForeignKey(c => c.PriorityId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.SyntomStatus).WithMany(p => p.PatientSyntoms).HasForeignKey(c => c.StatusId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
