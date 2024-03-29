﻿// <auto-generated />
using System;
using Gestfac.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gestfac.Migrations
{
    [DbContext(typeof(GestfacDbContext))]
    [Migration("20220116021920_Second")]
    partial class Second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Gestfac.DTOs.PriceUpdateDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductDTOId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductDTOId");

                    b.ToTable("PriceUpdates");
                });

            modelBuilder.Entity("Gestfac.DTOs.ProductDTO", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagsSerialized")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Gestfac.DTOs.PriceUpdateDTO", b =>
                {
                    b.HasOne("Gestfac.DTOs.ProductDTO", "ProductDTO")
                        .WithMany("PriceUpdates")
                        .HasForeignKey("ProductDTOId")
                        .HasConstraintName("FK_PriceUpdates_Products_ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductDTO");
                });

            modelBuilder.Entity("Gestfac.DTOs.ProductDTO", b =>
                {
                    b.Navigation("PriceUpdates");
                });
#pragma warning restore 612, 618
        }
    }
}
