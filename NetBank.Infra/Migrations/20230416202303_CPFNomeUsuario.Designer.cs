﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetBank.Infra.Data;

#nullable disable

namespace NetBank.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230416202303_CPFNomeUsuario")]
    partial class CPFNomeUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetBank.Domain.Entidades.Conta", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Agencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("NetBank.Domain.Entidades.Transacao", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContaEnviouId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContaRecebeuId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataOperacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(20, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ContaEnviouId");

                    b.HasIndex("ContaRecebeuId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("NetBank.Domain.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("NetBank.Domain.Entidades.Conta", b =>
                {
                    b.HasOne("NetBank.Domain.Entidades.Usuario", "Usuario")
                        .WithMany("Contas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NetBank.Domain.Entidades.Transacao", b =>
                {
                    b.HasOne("NetBank.Domain.Entidades.Conta", "ContaEnviou")
                        .WithMany("TransacoesEnviadas")
                        .HasForeignKey("ContaEnviouId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NetBank.Domain.Entidades.Conta", "ContaRecebeu")
                        .WithMany("TransacoesRecebidas")
                        .HasForeignKey("ContaRecebeuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ContaEnviou");

                    b.Navigation("ContaRecebeu");
                });

            modelBuilder.Entity("NetBank.Domain.Entidades.Conta", b =>
                {
                    b.Navigation("TransacoesEnviadas");

                    b.Navigation("TransacoesRecebidas");
                });

            modelBuilder.Entity("NetBank.Domain.Entidades.Usuario", b =>
                {
                    b.Navigation("Contas");
                });
#pragma warning restore 612, 618
        }
    }
}
