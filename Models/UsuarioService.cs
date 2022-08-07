using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
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
        public ICollection<Usuario> ListarTodos(FiltrosUsuarios filtro = null)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Usuario> query;

                if (filtro != null)
                {
                    //definindo dinamicamente a filtragem
                    switch (filtro.TipoFiltro)
                    {
                        case "Nome":
                            query = bc.Usuarios.Where(user => user.Nome.Contains(filtro.Filtro));
                            break;
                        case "Login":
                            query = bc.Usuarios.Where(user => user.Login.Contains(filtro.Filtro));
                            break;
                        default:
                            query = bc.Usuarios;
                            break;
                    }
                }
                else
                {
                    // caso filtro não tenha sido informado
                    query = bc.Usuarios;
                }

                //ordenação padrão
                return query.OrderBy(user => user.Id).ToList();
            }
        }
        public Usuario ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }
    }
}
