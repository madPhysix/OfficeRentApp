﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OfficeRentApp.Data;

#nullable disable

namespace OfficeRentApp.Migrations
{
    [DbContext(typeof(OfficeRentDbContext))]
    partial class OfficeRentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OfficeRentApp.Helpers.ImageManipulation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("OfficeRentApp.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<bool>("HasAC")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCoffeeService")
                        .HasColumnType("bit");

                    b.Property<bool>("HasParking")
                        .HasColumnType("bit");

                    b.Property<bool>("HasWifi")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerHour")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "44 Jafar Jabbarli street, Baku 1065",
                            BuildingName = "Caspian Plaza",
                            Description = "bomba ofisdi",
                            Floor = 8,
                            HasAC = true,
                            HasCoffeeService = true,
                            HasParking = true,
                            HasWifi = true,
                            Location = "40.3853494919391, 49.828683540862414",
                            PricePerHour = 60m
                        },
                        new
                        {
                            Id = 2,
                            Address = "44 Jafar Jabbarli street, Baku 1065",
                            BuildingName = "Caspian Plaza",
                            Description = "bomba ofisdi",
                            Floor = 15,
                            HasAC = true,
                            HasCoffeeService = true,
                            HasParking = true,
                            HasWifi = true,
                            Location = "40.3853494919391, 49.828683540862414",
                            PricePerHour = 85m
                        },
                        new
                        {
                            Id = 3,
                            Address = "44 Jafar Jabbarli street, Baku 1065",
                            BuildingName = "Caspian Plaza",
                            Description = "bomba ofisdi",
                            Floor = 3,
                            HasAC = true,
                            HasCoffeeService = true,
                            HasParking = true,
                            HasWifi = true,
                            Location = "40.3853494919391, 49.828683540862414",
                            PricePerHour = 40m
                        });
                });

            modelBuilder.Entity("OfficeRentApp.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndOfRent")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartOfRent")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("OfficeRentApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OfficeRentApp.Models.UserRoleDefine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "User"
                        },
                        new
                        {
                            Id = 2,
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("OfficeRentApp.Helpers.ImageManipulation", b =>
                {
                    b.HasOne("OfficeRentApp.Models.Office", "Office")
                        .WithMany("Images")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("OfficeRentApp.Models.Rental", b =>
                {
                    b.HasOne("OfficeRentApp.Models.Office", "Office")
                        .WithMany("Rentals")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("OfficeRentApp.Models.User", b =>
                {
                    b.HasOne("OfficeRentApp.Models.UserRoleDefine", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OfficeRentApp.Models.Office", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("OfficeRentApp.Models.UserRoleDefine", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
