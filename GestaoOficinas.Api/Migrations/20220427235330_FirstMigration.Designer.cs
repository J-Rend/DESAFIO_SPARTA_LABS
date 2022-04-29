﻿// <auto-generated />
using System;
using GestaoOficinas.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestaoOficinas.Api.Migrations
{
    [DbContext(typeof(GestaoOficinasApiContext))]
    [Migration("20220427235330_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GestaoOficinas.Api.Models.Oficina", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UnidadeTempoDiaria")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Oficina");
                });

            modelBuilder.Entity("GestaoOficinas.Api.Models.OficinaServico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DataServico")
                        .HasColumnType("datetime2");

                    b.Property<long>("OficinaId")
                        .HasColumnType("bigint");

                    b.Property<long>("ServicoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("OficinaServico");
                });

            modelBuilder.Entity("GestaoOficinas.Api.Models.Servico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UnidadesTrabalhoRequerida")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Servico");
                });
#pragma warning restore 612, 618
        }
    }
}