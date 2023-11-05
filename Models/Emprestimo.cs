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
            livro.Quantidade--;

            bc.Update(livro);
            bc.Emprestimos.Add(this);

            bc.SaveChanges();
        }

        public void Excluir()
        {
            BibliotecaContext bc = new();
            Livro livro = bc.Livros.Find(this.LivroId);

            if(this.Devolvido == false)
                livro.Quantidade++;

            bc.Update(livro);
            bc.Emprestimos.Remove(this);
            bc.SaveChanges();
        }

        public void Devolver()
        {
            BibliotecaContext bc = new();
            Livro livro = bc.Livros.First(l => l.Id == this.LivroId);

            this.Devolvido = true;
            livro.Quantidade++;

            bc.Livros.Update(livro);
            bc.Emprestimos.Update(this);
            bc.SaveChanges();
        }

        public void Emprestar()
        {
            BibliotecaContext bc = new();
            Livro livro = bc.Livros.First(l => l.Id == this.LivroId);

            this.Devolvido = false;
            livro.Quantidade--;

            bc.Livros.Update(livro);
            bc.Emprestimos.Update(this);
            bc.SaveChanges();
        }

        public void Atualizar()
        {
            BibliotecaContext bc = new();
            Livro livro = bc.Livros.Find(this.LivroId);

            bc.Update(this);
            bc.SaveChanges();
        }

        public List<Emprestimo> ListarTodos()
        {
            BibliotecaContext bc = new();

            return bc.Emprestimos.ToList();
        }
    }

}