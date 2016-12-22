﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class OrganisationPhoneNumbersMap
	{
		public static void Map(EntityTypeBuilder<OrganisationPhoneNumbers> entityBuilder)
		{
			entityBuilder.ToTable("OrganisationPhoneNumbers", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.OrganisationId).HasColumnName("OrganisationId").IsRequired();
			entityBuilder.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(50);
			entityBuilder.Property(t => t.PhoneTypeId).HasColumnName("PhoneTypeId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Organisations).WithMany(p => p.OrganisationPhoneNumbers).HasForeignKey(c => c.OrganisationId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.PhoneTypes).WithMany(p => p.OrganisationPhoneNumbers).HasForeignKey(c => c.PhoneTypeId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
