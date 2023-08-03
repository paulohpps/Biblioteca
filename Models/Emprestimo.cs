using Biblioteca.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }

        public DateTime DataDevolucao { get; set; }

        public string NomeUsuario { get; set; }

        public string Telefone { get; set; }
        public bool Devolvido { get; set; }

        [ForeignKey("Livro")]
        public int LivroId { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Valor { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal ValorMulta { get; set; }

        public virtual Livro Livro { get; set; }

        public void Inserir()
        {
            BibliotecaContext bc = new();

            Livro livro = bc.Livros.Find(this.LivroId);

            this.Valor = livro.ValorAluguel * (this.DataDevolucao - this.DataEmprestimo).Days;

            bc.Emprestimos.Add(this);
            bc.SaveChanges();
        }

        public void Atualizar()
        {
            BibliotecaContext bc = new();

            Emprestimo emprestimo = bc.Emprestimos.Find(this.Id);
            Livro livro = bc.Livros.Find(this.LivroId);

            emprestimo.DataEmprestimo = this.DataEmprestimo;
            emprestimo.DataDevolucao = this.DataDevolucao;
            emprestimo.NomeUsuario = this.NomeUsuario;
            emprestimo.Telefone = this.Telefone;
            emprestimo.Devolvido = this.Devolvido;
            emprestimo.LivroId = this.LivroId;

            if(!emprestimo.Devolvido)
            {
                emprestimo.Valor = livro.ValorAluguel * (this.DataDevolucao - this.DataEmprestimo).Days;
                if (this.DataDevolucao < DateTime.Now)
                {
                    emprestimo.ValorMulta = livro.ValorAluguel * 1.1m * (DateTime.Now - this.DataDevolucao).Days;
                }
                else
                {
                    emprestimo.ValorMulta = 0;
                }
            }

            bc.Update(emprestimo);
            bc.SaveChanges();
        }

        public List<Emprestimo> ListarTodos()
        {
            BibliotecaContext bc = new();

            return bc.Emprestimos.ToList();
        }
    }

}