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
	public class Organisations_CompanysMap
	{
		public static void Map(EntityTypeBuilder<Organisations_Companys> entityBuilder)
		{
			entityBuilder.ToTable("Organisations_Companys", "dbo");
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedNever();	
			entityBuilder.Property(t => t.Id).HasColumnName("Id").IsRequired();
		//-------------------Navigation Properties -------------------------------//
	
				//----------------Parent Properties
				//entityBuilder.HasOne(p => p.Organisations).WithOne(p => p.Organisations_Companys).HasForeignKey<Organisations>(c => c.Id).OnDelete(DeleteBehavior.Restrict);
	
		}
	}
}
