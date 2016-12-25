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
	public class CountriesMap
	{
		public static void Map(EntityTypeBuilder<Countries> entityBuilder)
		{
			entityBuilder.ToTable("Countries", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
		//-------------------Navigation Properties -------------------------------//
				entityBuilder.HasMany(x => x.AddressCountries).WithOne(p => p.Countries).HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.PersonCountryOfResidence).WithOne(p => p.Countries).HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.Restrict);
	
				//----------------Parent Properties
	
		}
	}
}
