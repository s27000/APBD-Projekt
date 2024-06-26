﻿// <auto-generated />
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

            modelBuilder.Entity("Projekt.Models.Domain.Client", b =>
                {
                    b.Navigation("Firm")
                        .IsRequired();

                    b.Navigation("Person")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
