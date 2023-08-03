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

            bc.Update(livro);
            bc.SaveChanges();
        }

        public void Excluir(int id)
        {
            BibliotecaContext bc = new();

            Livro livro = bc.Livros.Find(id);

            bc.Livros.Remove(livro);
            bc.SaveChanges();
        }
    }
}