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
	public class AddressCitiesMap
	{
		public static void Map(EntityTypeBuilder<AddressCities> entityBuilder)
		{
			entityBuilder.ToTable("AddressCities", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedNever();	
			entityBuilder.Property(t => t.CityId).HasColumnName("CityId").IsRequired();
			entityBuilder.Property(t => t.Id).HasColumnName("Id").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Cities).WithMany(p => p.AddressCities).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.Addresses).WithOne(p => p.AddressCities).HasForeignKey<Addresses>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
