﻿// <auto-generated />
using System;
using Gestfac.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gestfac.Migrations
{
    [DbContext(typeof(GestfacDbContext))]
    partial class GestfacDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Gestfac.DTOs.ProductDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
