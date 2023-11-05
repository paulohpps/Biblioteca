using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Services
{
    public class EmprestimoService
    {
        public void Inserir(Emprestimo e)
        {
            BibliotecaContext bc = new();

            e.Valor = e.Livro.ValorAluguel * (e.DataEmprestimo - e.DataDevolucao).Days;
            bc.Emprestimos.Add(e);
            bc.SaveChanges();
        }

        public void Atualizar(Emprestimo e)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;
                int diasEmprestimo = (e.DataDevolucao - e.DataEmprestimo).Days;
                emprestimo.Valor = e.Livro.ValorAluguel * diasEmprestimo;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(int skip, FiltrosEmprestimos filtro = null)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Emprestimo> query;

                if (filtro != null)
                {
                    switch (filtro.TipoFiltro)
                    {
                        case "Usuario":
                            query = bc.Emprestimos
                                .Where(e => e.NomeUsuario.Contains(filtro.Filtro))
                                .OrderByDescending(d => d.DataEmprestimo)
                                .Skip(skip)
                                .Take(10);
                            break;

                        case "Livro":
                            query = bc.Emprestimos
                                .Where(e => e.Livro.Titulo.Contains(filtro.Filtro))
                                .OrderByDescending(d => d.DataEmprestimo)
                                .Skip(skip)
                                .Take(10);
                            break;

                        default:
                            query = bc.Emprestimos
                                .OrderByDescending(d => d.DataEmprestimo)
                                .Skip(skip)
                                .Take(10);
                            break;
                    }
                }
                else
                {
                    query = bc.Emprestimos
                        .OrderByDescending(d => d.DataEmprestimo)
                        .Skip(skip)
                        .Take(10);
                }

                return query.Include(e => e.Livro).ToList();
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }
    }
}