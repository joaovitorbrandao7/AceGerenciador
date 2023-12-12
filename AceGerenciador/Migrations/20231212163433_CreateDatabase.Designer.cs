﻿// <auto-generated />
using System;
using AceGerenciador.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AceGerenciador.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231212163433_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("AceGerenciador.Models.Clientes", b =>
                {
                    b.Property<int?>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EnderecoCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TelefoneCliente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoPet")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdCliente");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("AceGerenciador.Models.Compra", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("TEXT");

                    b.Property<int>("FuncionarioIdFuncionario")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalCompra")
                        .HasColumnType("TEXT");

                    b.HasKey("IdCompra");

                    b.HasIndex("FuncionarioIdFuncionario");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("AceGerenciador.Models.Funcionario", b =>
                {
                    b.Property<int>("IdFuncionario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CargoFuncionario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAdmissao")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeFuncionario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdFuncionario");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("AceGerenciador.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("CompraIdCompra")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PrecoUnitarioVenda")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantidadeEmEstoque")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantidadeVendida")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProdutoId");

                    b.HasIndex("CompraIdCompra");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("AceGerenciador.Models.ProdutoVendido", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VendaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProdutoId", "VendaId", "FuncionarioId");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("VendaId");

                    b.ToTable("ProdutoVendido");
                });

            modelBuilder.Entity("AceGerenciador.Models.RelatorioDiario", b =>
                {
                    b.Property<int>("IdRelatorio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataRelatorio")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalVendasDia")
                        .HasColumnType("TEXT");

                    b.HasKey("IdRelatorio");

                    b.ToTable("RelatorioDiario");
                });

            modelBuilder.Entity("AceGerenciador.Models.Venda", b =>
                {
                    b.Property<int>("VendaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientesIdCliente")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("TEXT");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RelatorioDiarioIdRelatorio")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalVenda")
                        .HasColumnType("TEXT");

                    b.HasKey("VendaId");

                    b.HasIndex("ClientesIdCliente");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("RelatorioDiarioIdRelatorio");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("AceGerenciador.Models.Compra", b =>
                {
                    b.HasOne("AceGerenciador.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioIdFuncionario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("AceGerenciador.Models.Produto", b =>
                {
                    b.HasOne("AceGerenciador.Models.Compra", null)
                        .WithMany("Produtos")
                        .HasForeignKey("CompraIdCompra");
                });

            modelBuilder.Entity("AceGerenciador.Models.ProdutoVendido", b =>
                {
                    b.HasOne("AceGerenciador.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AceGerenciador.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AceGerenciador.Models.Venda", "Venda")
                        .WithMany("ProdutosVendidos")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");

                    b.Navigation("Produto");

                    b.Navigation("Venda");
                });

            modelBuilder.Entity("AceGerenciador.Models.Venda", b =>
                {
                    b.HasOne("AceGerenciador.Models.Clientes", "Clientes")
                        .WithMany()
                        .HasForeignKey("ClientesIdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AceGerenciador.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AceGerenciador.Models.RelatorioDiario", null)
                        .WithMany("VendasDia")
                        .HasForeignKey("RelatorioDiarioIdRelatorio");

                    b.Navigation("Clientes");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("AceGerenciador.Models.Compra", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("AceGerenciador.Models.RelatorioDiario", b =>
                {
                    b.Navigation("VendasDia");
                });

            modelBuilder.Entity("AceGerenciador.Models.Venda", b =>
                {
                    b.Navigation("ProdutosVendidos");
                });
#pragma warning restore 612, 618
        }
    }
}