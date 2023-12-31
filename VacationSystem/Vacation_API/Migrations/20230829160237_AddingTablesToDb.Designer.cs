﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vacation_API.Data;

#nullable disable

namespace Vacation_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230829160237_AddingTablesToDb")]
    partial class AddingTablesToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vacation_API.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupancy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Regime")
                        .HasColumnType("int");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<DateTime>("entryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("leaveDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("vacationDays")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Vacation_API.Models.HistVacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupancy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("finishVacationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("startVacationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.ToTable("HistVacations");
                });

            modelBuilder.Entity("Vacation_API.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Vacation_API.Models.MyDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EligibleDays")
                        .HasColumnType("int");

                    b.Property<string>("Occupancy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("combinedDays")
                        .HasColumnType("int");

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.Property<double>("restantDays")
                        .HasColumnType("float");

                    b.Property<DateTime?>("updatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.ToTable("myDetails");
                });

            modelBuilder.Entity("Vacation_API.Models.HistVacation", b =>
                {
                    b.HasOne("Vacation_API.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Vacation_API.Models.MyDetails", b =>
                {
                    b.HasOne("Vacation_API.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
