﻿using EFCore.Toolkit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToolkitSample.Model;

namespace ToolkitSample.DataAccess.Context
{
    public class CountryEntityConfiguration : EntityTypeConfiguration<Model.Country>
    {
        public override void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasKey(d => d.Id);

            entity.Property(t => t.Id)
                .ValueGeneratedNever()
                .IsRequired()
                .HasMaxLength(3);

            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(255)
                //TODO .IsUnique()
                ;
        }
    }
}