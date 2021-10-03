﻿// <auto-generated />
using System;
using DynamicSun;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DynamicSun.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20211003124510_Arciv")]
    partial class Arciv
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DynamicSun.Archive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("DynamicSun.DynamicSun.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchiveName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CloudCover")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double?>("DewPoint")
                        .HasColumnType("float");

                    b.Property<string>("HorizontalVisibility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LowLimitCloud")
                        .HasColumnType("float");

                    b.Property<double?>("Pressure")
                        .HasColumnType("float");

                    b.Property<double?>("Temp")
                        .HasColumnType("float");

                    b.Property<string>("WeatherEffect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Wet")
                        .HasColumnType("float");

                    b.Property<string>("WindDirect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}