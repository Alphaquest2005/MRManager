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
	public class OrganisationsMap
	{
		public static void Map(EntityTypeBuilder<Organisations> entityBuilder)
		{
			entityBuilder.ToTable("Organisations", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.EntryTimeStamp).HasColumnName("EntryTimeStamp").ValueGeneratedOnAdd();
			entityBuilder.Property(t => t.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
			entityBuilder.Property(t => t.VATNumber).HasColumnName("VATNumber").IsRequired().HasMaxLength(50);
		//-------------------Navigation Properties -------------------------------//
				entityBuilder.HasMany(x => x.OrganisationAddress).WithOne(p => p.Organisations).HasForeignKey(c => c.OrganisationId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.OrganisationPhoneNumbers).WithOne(p => p.Organisations).HasForeignKey(c => c.OrganisationId).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasOne(p => p.Organisations_Companys).WithOne(p => p.Organisations).HasForeignKey<Organisations_Companys>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasOne(p => p.Organisations_Hotels).WithOne(p => p.Organisations).HasForeignKey<Organisations_Hotels>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
				entityBuilder.HasMany(x => x.PersonJob).WithOne(p => p.Organisations).HasForeignKey(c => c.OrganisationId).OnDelete(DeleteBehavior.Restrict);
	
				//----------------Parent Properties
	
		}
	}
}
