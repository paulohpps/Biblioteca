using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal ValorVenda { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal ValorAluguel { get; set; }

        public int Ano { get; set; }

        public int Quantidade { get; set; }

        public virtual List<Emprestimo> Emprestimos{ get; set; }

        public void Inserir()
        {
            BibliotecaContext bc = new();

            bc.Livros.Add(this);
            bc.SaveChanges();
        }

        public void Atualizar()
        {
            BibliotecaContext bc = new();

            Livro livro = bc.Livros.Find(this.Id);

            livro.Titulo = this.Titulo;
            livro.Autor = this.Autor;
            livro.ValorVenda = this.ValorVenda;
            livro.ValorAluguel = this.ValorAluguel;
            livro.Ano = this.Ano;
            livro.Quantidade = this.Quantidade;

            bc.Update(livro);
            bc.SaveChanges();
        }

        public void Excluir()
        {
            BibliotecaContext bc = new();

            Livro livro = bc.Livros.Find(this.Id);

            bc.Remove(livro);
            bc.SaveChanges();
        }

        public void Vender()
        {
            BibliotecaContext bc = new();

            Livro livro = bc.Livros.Find(this.Id);

            livro!.Quantidade--;

            Venda venda = new();
            venda.LivroId = livro.Id;
            venda.Valor = livro.ValorVenda;
            venda.DataVenda = System.DateTime.Now;
            venda.Inserir();

            bc.Update(livro);
            bc.SaveChanges();
        }
    }
}