using Biblioteca.Models;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Services
{
    public class VendaService
    {
        public ICollection<Venda> ListarTodos(int skip, FiltrosVendas filtro = null)
        {
            BibliotecaContext bc = new();
            IQueryable<Venda> query;

            if(filtro != null)
            {
                switch (filtro.TipoFiltro)
                {
                    case "Livro":
                        query = bc.Vendas
                            .Where(v => v.Livro.Titulo.Contains(filtro.Filtro))
                            .OrderBy(a => a.DataVenda)
                            .Skip(skip)
                            .Take(10);
                        break;

                    case "Autor":
                        query = bc.Vendas
                            .Where(v => v.Livro.Autor.Contains(filtro.Filtro))
                            .OrderBy(a => a.DataVenda)
                            .Skip(skip)
                            .Take(10);
                        break;

                    default:
                        query = bc.Vendas
                            .OrderBy(a => a.DataVenda)
                            .Skip(skip)
                            .Take(10);
                        break;
                }

                return query.ToList();
            }
            query = bc.Vendas
                .OrderBy(a => a.DataVenda)
                .Skip(skip)
                .Take(10);

            return query.ToList();
        }
    }
}
