// <auto-generated />
using System;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210530202548_Code on Company")]
    partial class CodeonCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Code")
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

            modelBuilder.Entity("Domains.Entities.Deliverer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CellphoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeCellphoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VerifyingCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Deliverers");
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

                    b.Property<decimal>("LatitudeDeliverer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LongitudeDeliverer")
                        .HasColumnType("decimal(18,2)");

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

                    b.Property<decimal>("LatitudeDeliverer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LongitudeDeliverer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReasonOccurrence")
                        .HasColumnType("int");

                    b.Property<string>("UrlsEvidences")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("CFOP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarrierCNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarrierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationCEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationComplement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationDistrict")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationUF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DispatchedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IssueType")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerCEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerComplement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerDistrict")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerUF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelNFE")
                        .HasColumnType("int");

                    b.Property<string>("NatureOperation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumericCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverCNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Series")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("DECIMAL");

                    b.Property<string>("UFIssuerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerifyingDigit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("DECIMAL");

                    b.Property<string>("XMLPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domains.Entities.Shipper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Shippers");
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

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domains.Entities.Deliverer", b =>
                {
                    b.HasOne("Domains.Entities.User", "User")
                        .WithOne("Deliverer")
                        .HasForeignKey("Domains.Entities.Deliverer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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
                        .WithMany("Orders")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domains.Entities.Shipper", b =>
                {
                    b.HasOne("Domains.Entities.User", "User")
                        .WithOne("Shipper")
                        .HasForeignKey("Domains.Entities.Shipper", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domains.Entities.Deliverer", b =>
                {
                    b.Navigation("FinishedOrders");

                    b.Navigation("OccurrencesOrders");
                });

            modelBuilder.Entity("Domains.Entities.Order", b =>
                {
                    b.Navigation("FinishOrder");

                    b.Navigation("Occurrences");
                });

            modelBuilder.Entity("Domains.Entities.Shipper", b =>
                {
                    b.Navigation("ShipperHasCompanies");
                });

            modelBuilder.Entity("Domains.Entities.User", b =>
                {
                    b.Navigation("Deliverer");

                    b.Navigation("Shipper");
                });
#pragma warning restore 612, 618
        }
    }
}
