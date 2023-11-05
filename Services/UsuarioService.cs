using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;

namespace Biblioteca.Services
{
    public class UsuarioService
    {
        public void Inserir(Usuario user)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(user);
                bc.SaveChanges();
            }
        }
        public void Atualizar(Usuario user)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuarios.Find(user.Id);
                usuario.Nome = user.Nome;
                usuario.Login = user.Login;
                usuario.Senha = user.Senha;

                bc.SaveChanges();
            }
        }
        public ICollection<Usuario> ListarTodos(int skip = 0, FiltrosUsuarios filtro = null)
        {
            BibliotecaContext bc = new();
            IQueryable<Usuario> query;

            if (filtro != null)
            {
                switch (filtro.TipoFiltro)
                {
                    case "Nome":
                        query = bc.Usuarios
                            .Where(user => user.Nome.Contains(filtro.Filtro))
                            .Skip(skip)
                            .Take(10);
                        break;

                    case "Login":
                        query = bc.Usuarios
                            .Where(user => user.Login.Contains(filtro.Filtro))
                            .Skip(skip)
                            .Take(10);
                        break;

                    default:
                        query = bc.Usuarios
                            .Skip(skip)
                            .Take(10);
                        break;
                }
            }
            else
            {
                query = bc.Usuarios
                    .Skip(skip)
                    .Take(10);
            }

            return query.OrderBy(user => user.Id).ToList();

        }
        public Usuario ObterPorId(int id)
        {
            using BibliotecaContext bc = new();
            return bc.Usuarios.Find(id);
        }
    }
}
