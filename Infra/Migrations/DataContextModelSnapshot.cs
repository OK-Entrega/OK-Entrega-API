﻿// <auto-generated />
using System;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domains.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Segment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Domains.Entities.FinishOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DelivererId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FinishType")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ReasonDevolution")
                        .HasColumnType("int");

                    b.Property<string>("UrlVoucher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlsEvidences")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DelivererId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("FinishedOrders");
                });

            modelBuilder.Entity("Domains.Entities.OccurrenceOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DelivererId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReasonOccurrence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DelivererId");

                    b.HasIndex("OrderId");

                    b.ToTable("Occurrences");
                });

            modelBuilder.Entity("Domains.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarrierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromCNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IssueType")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuerCompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerStateRegistration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelNFE")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverCompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverStateRegistration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Series")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToCNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPriceNFE")
                        .HasColumnType("DECIMAL");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domains.Entities.ShipperCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ShipperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ShipperRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ShipperId");

                    b.ToTable("ShipperCompanies");
                });

            modelBuilder.Entity("Domains.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Domains.Entities.Deliverer", b =>
                {
                    b.HasBaseType("Domains.Entities.User");

                    b.Property<string>("CellphoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Deliverer");
                });

            modelBuilder.Entity("Domains.Entities.Shipper", b =>
                {
                    b.HasBaseType("Domains.Entities.User");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Shipper");
                });

            modelBuilder.Entity("Domains.Entities.FinishOrder", b =>
                {
                    b.HasOne("Domains.Entities.Deliverer", "Deliverer")
                        .WithMany("FinishedOrders")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domains.Entities.Order", "Order")
                        .WithOne("FinishOrder")
                        .HasForeignKey("Domains.Entities.FinishOrder", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliverer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domains.Entities.OccurrenceOrder", b =>
                {
                    b.HasOne("Domains.Entities.Deliverer", "Deliverer")
                        .WithMany("OccurrencesOrders")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domains.Entities.Order", "Order")
                        .WithMany("Occurrences")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliverer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domains.Entities.Order", b =>
                {
                    b.HasOne("Domains.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domains.Entities.ShipperCompany", b =>
                {
                    b.HasOne("Domains.Entities.Company", "Company")
                        .WithMany("CompanyHasShippers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domains.Entities.Shipper", "Shipper")
                        .WithMany("ShipperHasCompanies")
                        .HasForeignKey("ShipperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Shipper");
                });

            modelBuilder.Entity("Domains.Entities.Company", b =>
                {
                    b.Navigation("CompanyHasShippers");
                });

            modelBuilder.Entity("Domains.Entities.Order", b =>
                {
                    b.Navigation("FinishOrder");

                    b.Navigation("Occurrences");
                });

            modelBuilder.Entity("Domains.Entities.Deliverer", b =>
                {
                    b.Navigation("FinishedOrders");

                    b.Navigation("OccurrencesOrders");
                });

            modelBuilder.Entity("Domains.Entities.Shipper", b =>
                {
                    b.Navigation("ShipperHasCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}
