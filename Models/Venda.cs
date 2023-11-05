using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class Venda
    {
        public int Id { get; set; }

        public DateTime DataVenda { get; set; }

        [ForeignKey("Livro")]
        public int LivroId { get; set; }

        public decimal Valor { get; set; }

        public virtual Livro Livro { get; set; }

        public void Inserir()
        {
            BibliotecaContext bc = new();

            bc.Vendas.Add(this);
            bc.SaveChanges();
        }

        public void Rembolsar()
        {
            BibliotecaContext bc = new();
            Livro livro = bc.Livros.Find(this.LivroId);

            livro.Quantidade++;
            bc.Update(livro);
            bc.Vendas.Remove(this);
            bc.SaveChanges();
        }
    }
}
