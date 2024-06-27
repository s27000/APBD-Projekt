﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt.Context;

#nullable disable

namespace Projekt.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekt.Models.Domain.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdClient");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ClientType");

                    b.Property<bool>("Depreciated")
                        .HasColumnType("bit")
                        .HasColumnName("Depreciated");

                    b.HasKey("IdClient");

                    b.ToTable("client", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdDiscount");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateFrom");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateTo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("Value")
                        .HasColumnType("int")
                        .HasColumnName("Value");

                    b.HasKey("IdDiscount");

                    b.ToTable("discount", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.Firm", b =>
                {
                    b.Property<int>("IdClient")
                        .HasColumnType("int")
                        .HasColumnName("IdClient");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("KRS");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("IdClient");

                    b.ToTable("firm", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.Person", b =>
                {
                    b.Property<int>("IdClient")
                        .HasColumnType("int")
                        .HasColumnName("IdClient");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("PESEL")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PESEL");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Surname");

                    b.HasKey("IdClient");

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.Product", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdProduct");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProduct"));

                    b.Property<decimal>("AnnualPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("AnnualPrice");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("ProductCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductCategory");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Version");

                    b.HasKey("IdProduct");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.ProductContract", b =>
                {
                    b.Property<int>("IdProductContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdProductContract");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProductContract"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateFrom");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2")
                        .HasColumnName("DateTo");

                    b.Property<int>("IdClient")
                        .HasColumnType("int")
                        .HasColumnName("IdClient");

                    b.Property<int?>("IdDiscount")
                        .HasColumnType("int")
                        .HasColumnName("IdDiscount");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int")
                        .HasColumnName("IdProduct");

                    b.Property<string>("ProductUpdateDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductUpdateDescription");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductVersion");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalPrice");

                    b.Property<int>("UpdateSupportDuration")
                        .HasColumnType("int")
                        .HasColumnName("UpdateSupportExtension");

                    b.Property<int?>("Value")
                        .HasColumnType("int")
                        .HasColumnName("Value");

                    b.HasKey("IdProductContract");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdDiscount");

                    b.HasIndex("IdProduct");

                    b.ToTable("productContract", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.ProductContractPayment", b =>
                {
                    b.Property<int>("IdProductContractPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdProductContractPayment");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProductContractPayment"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<int>("IdProductContract")
                        .HasColumnType("int")
                        .HasColumnName("IdProductContract");

                    b.Property<decimal>("PaymentValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PaymentValue");

                    b.HasKey("IdProductContractPayment");

                    b.HasIndex("IdProductContract");

                    b.ToTable("productContractPayment", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.Subscription", b =>
                {
                    b.Property<int>("IdSubscription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdSubscription");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSubscription"));

                    b.Property<int>("IdProduct")
                        .HasColumnType("int")
                        .HasColumnName("IdProduct");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<int>("SubscriptionRenewelInMonths")
                        .HasColumnType("int")
                        .HasColumnName("SubscriptionRenewelInMonths");

                    b.HasKey("IdSubscription");

                    b.HasIndex("IdProduct");

                    b.ToTable("subscription", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Role");

                    b.HasKey("Login");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Projekt.Models.Domain.Firm", b =>
                {
                    b.HasOne("Projekt.Models.Domain.Client", "Client")
                        .WithOne("Firm")
                        .HasForeignKey("Projekt.Models.Domain.Firm", "IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Projekt.Models.Domain.Person", b =>
                {
                    b.HasOne("Projekt.Models.Domain.Client", "Client")
                        .WithOne("Person")
                        .HasForeignKey("Projekt.Models.Domain.Person", "IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Projekt.Models.Domain.ProductContract", b =>
                {
                    b.HasOne("Projekt.Models.Domain.Client", "Client")
                        .WithMany("ProductContracts")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekt.Models.Domain.Discount", "Discount")
                        .WithMany("ProductContracts")
                        .HasForeignKey("IdDiscount");

                    b.HasOne("Projekt.Models.Domain.Product", "Product")
                        .WithMany("ProductContracts")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Discount");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Projekt.Models.Domain.ProductContractPayment", b =>
                {
                    b.HasOne("Projekt.Models.Domain.ProductContract", "ProductContract")
                        .WithMany("ProductContractPayments")
                        .HasForeignKey("IdProductContract")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductContract");
                });

            modelBuilder.Entity("Projekt.Models.Domain.Subscription", b =>
                {
                    b.HasOne("Projekt.Models.Domain.Product", "Product")
                        .WithMany("Subscriptions")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Projekt.Models.Domain.Client", b =>
                {
                    b.Navigation("Firm")
                        .IsRequired();

                    b.Navigation("Person")
                        .IsRequired();

                    b.Navigation("ProductContracts");
                });

            modelBuilder.Entity("Projekt.Models.Domain.Discount", b =>
                {
                    b.Navigation("ProductContracts");
                });

            modelBuilder.Entity("Projekt.Models.Domain.Product", b =>
                {
                    b.Navigation("ProductContracts");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("Projekt.Models.Domain.ProductContract", b =>
                {
                    b.Navigation("ProductContractPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
