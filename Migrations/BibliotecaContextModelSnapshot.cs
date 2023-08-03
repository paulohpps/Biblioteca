﻿// <auto-generated />
using System;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Biblioteca.Migrations
{
    [DbContext(typeof(BibliotecaContext))]
    partial class BibliotecaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Biblioteca.Models.Emprestimo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataDevolucao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEmprestimo")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Devolvido")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<string>("NomeUsuario")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<decimal>("ValorMulta")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("Biblioteca.Models.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Autor")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.Property<decimal>("ValorAluguel")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("Id");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("Biblioteca.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Biblioteca.Models.Emprestimo", b =>
                {
                    b.HasOne("Biblioteca.Models.Livro", "Livro")
                        .WithMany("Emprestimos")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("Biblioteca.Models.Livro", b =>
                {
                    b.Navigation("Emprestimos");
                });
#pragma warning restore 612, 618
        }
    }
}