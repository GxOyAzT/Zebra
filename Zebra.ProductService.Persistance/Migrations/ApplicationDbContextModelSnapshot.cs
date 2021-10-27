﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zebra.ProductService.Persistance.Context;

namespace Zebra.ProductService.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zebra.ProductService.Domain.Entities.PriceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductModelId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Zebra.ProductService.Domain.Entities.ProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ean")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInSale")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Zebra.ProductService.Domain.Entities.RatingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductModelId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Zebra.ProductService.Domain.Entities.PriceModel", b =>
                {
                    b.HasOne("Zebra.ProductService.Domain.Entities.ProductModel", null)
                        .WithMany("Prices")
                        .HasForeignKey("ProductModelId");
                });

            modelBuilder.Entity("Zebra.ProductService.Domain.Entities.RatingModel", b =>
                {
                    b.HasOne("Zebra.ProductService.Domain.Entities.ProductModel", null)
                        .WithMany("Ratings")
                        .HasForeignKey("ProductModelId");
                });

            modelBuilder.Entity("Zebra.ProductService.Domain.Entities.ProductModel", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
