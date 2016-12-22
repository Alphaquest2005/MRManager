﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class UnitsMap
	{
		public static void Map(EntityTypeBuilder<Units> entityBuilder)
		{
			entityBuilder.ToTable("Units", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.Description).HasColumnName("Description").IsRequired().HasMaxLength(50);
			entityBuilder.Property(t => t.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
			entityBuilder.Property(t => t.ShortName).HasColumnName("ShortName").IsRequired().HasMaxLength(50);
		//-------------------Navigation Properties -------------------------------//
				entityBuilder.HasMany(x => x.BloodPressure).WithOne(p => p.Units).HasForeignKey(c => c.UnitId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.Height).WithOne(p => p.Units).HasForeignKey(c => c.UnitId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.Pulse).WithOne(p => p.Units).HasForeignKey(c => c.UnitId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.Respiration).WithOne(p => p.Units).HasForeignKey(c => c.UnitId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.Temperature).WithOne(p => p.Units).HasForeignKey(c => c.UnitId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.Weight).WithOne(p => p.Units).HasForeignKey(c => c.UnitId).OnDelete(DeleteBehavior.Restrict);
	
				//----------------Parent Properties
	
		}
	}
}
