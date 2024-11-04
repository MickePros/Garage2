﻿// <auto-generated />
using System;
using Garage2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage2.Migrations
{
    [DbContext(typeof(Garage2Context))]
    partial class Garage2ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Garage2.Models.Entities.ParkedVehicle", b =>
                {
                    b.Property<string>("RegNr")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("datetime2");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParkingSpot")
                        .HasColumnType("int");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.Property<int>("Wheels")
                        .HasColumnType("int");

                    b.HasKey("RegNr");

                    b.ToTable("ParkedVehicle");
                });

            modelBuilder.Entity("Garage2.Models.Entities.ParkingSpace", b =>
                {
                    b.Property<int>("ParkingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParkingId"));

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("ParkingId");

                    b.ToTable("ParkingSpaces");
                });

            modelBuilder.Entity("Garage2.Models.ViewModels.OverviewViewModel", b =>
                {
                    b.Property<string>("RegNr")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.HasKey("RegNr");

                    b.ToTable("OverviewViewModel");
                });
#pragma warning restore 612, 618
        }
    }
}
