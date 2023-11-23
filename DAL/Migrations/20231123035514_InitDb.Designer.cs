﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(EmprendimientosContext))]
    [Migration("20231123035514_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.ComentarioServicio", b =>
                {
                    b.Property<int>("IdDetalleServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleServicio"));

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaComentario")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdServicio")
                        .HasColumnType("int");

                    b.HasKey("IdDetalleServicio");

                    b.HasIndex("IdServicio");

                    b.ToTable("ComentarioServicio");
                });

            modelBuilder.Entity("DAL.Models.DetalleServicio", b =>
                {
                    b.Property<int>("IdDetalleServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleServicio"));

                    b.Property<int>("IdServicio")
                        .HasColumnType("int");

                    b.HasKey("IdDetalleServicio");

                    b.HasIndex("IdServicio")
                        .IsUnique();

                    b.ToTable("DetalleServicios");
                });

            modelBuilder.Entity("DAL.Models.Servicio", b =>
                {
                    b.Property<int>("IdServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServicio"));

                    b.Property<double>("CostoServicio")
                        .HasColumnType("float");

                    b.Property<string>("EstadoServicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<string>("IdServicioPublic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTecnico")
                        .HasColumnType("int");

                    b.Property<string>("TipoServicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalServicio")
                        .HasColumnType("float");

                    b.HasKey("IdServicio");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdTecnico");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("DAL.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DAL.Models.ComentarioServicio", b =>
                {
                    b.HasOne("DAL.Models.Servicio", "Servicio")
                        .WithMany("Comentarios")
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("DAL.Models.DetalleServicio", b =>
                {
                    b.HasOne("DAL.Models.Servicio", "Servicio")
                        .WithOne("DetalleServicio")
                        .HasForeignKey("DAL.Models.DetalleServicio", "IdServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("DAL.Models.Servicio", b =>
                {
                    b.HasOne("DAL.Models.Usuario", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DAL.Models.Usuario", "Tecnico")
                        .WithMany()
                        .HasForeignKey("IdTecnico")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Tecnico");
                });

            modelBuilder.Entity("DAL.Models.Servicio", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("DetalleServicio")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
