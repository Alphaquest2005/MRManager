﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class ForeignAddressesMap
	{
		public static void Map(EntityTypeBuilder<ForeignAddresses> entityBuilder)
		{
			entityBuilder.ToTable("ForeignAddresses", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();	
			entityBuilder.Property(t => t.AddressId).HasColumnName("AddressId").IsRequired();
			entityBuilder.Property(t => t.AddressTypeId).HasColumnName("AddressTypeId").IsRequired();
			entityBuilder.Property(t => t.PersonId).HasColumnName("PersonId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Addresses).WithMany(p => p.ForeignAddresses).HasForeignKey(c => c.AddressId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.AddressTypes).WithMany(p => p.ForeignAddresses).HasForeignKey(c => c.AddressTypeId).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.Persons_NonResidentPatient).WithMany(p => p.ForeignAddresses).HasForeignKey(c => c.PersonId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
