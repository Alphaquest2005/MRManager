﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF.Mappings
{
	public class AddressStatesMap
	{
		public static void Map(EntityTypeBuilder<AddressStates> entityBuilder)
		{
			entityBuilder.ToTable("AddressStates", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedNever();	
			entityBuilder.Property(t => t.Id).HasColumnName("Id").IsRequired();
			entityBuilder.Property(t => t.StateId).HasColumnName("StateId").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Addresses).WithOne(p => p.AddressStates).HasForeignKey<Addresses>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
				//entityBuilder.HasOne(p => p.States).WithMany(p => p.AddressStates).HasForeignKey(c => c.StateId).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
