using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using X.PagedList;

namespace Biblioteca.Services
{
    public class LivroService
    {
        public ICollection<Livro> ListarTodos(int skip, FiltrosLivros filtro = null)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Livro> query;

                if (filtro != null)
                {
                    switch (filtro.TipoFiltro)
                    {
                        case "Autor":
                            query = bc.Livros
                                .Where(l => l.Autor.Contains(filtro.Filtro))
                                .OrderBy(a => a.Titulo)
                                .Skip(skip)
                                .Take(10);
                            break;

                        case "Titulo":
                            query = bc.Livros
                                .Where(l => l.Titulo.Contains(filtro.Filtro))
                                .OrderBy(a => a.Titulo)
                                .Skip(skip)
                                .Take(10);
                            break;

                        default:
                            query = bc.Livros
                                .OrderBy(a => a.Titulo)
                                .Skip(skip)
                                .Take(10);
                            break;
                    }
                    return query.ToList();
                }
                query = bc.Livros
                    .OrderBy(a => a.Titulo)
                    .Skip(skip)
                    .Take(10);

                return query.ToList();
            }
        }

        public ICollection<Livro> ListarDisponiveis()
        {
            BibliotecaContext bc = new();
            return bc.Livros.Where(l => !bc.Emprestimos
                .Where(e => e.Devolvido == false)
                .Select(e => e.LivroId)
                .Contains(l.Id))
                .ToList();
        }

        public Livro ObterPorId(int id)
        {
            BibliotecaContext bc = new();
            return bc.Livros.Find(id);
        }
    }
}