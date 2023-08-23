namespace Biblioteca.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public void Inserir()
        {
            BibliotecaContext bc = new();

            bc.Usuarios.Add(this);
            bc.SaveChanges();
        }

        public void Atualizar()
        {
            BibliotecaContext bc = new();

            Usuario usuario = bc.Usuarios.Find(this.Id);

            usuario.Nome = this.Nome;
            usuario.Login = this.Login;
            usuario.Senha = this.Senha;

            bc.Update(usuario);
            bc.SaveChanges();
        }
    }

    public enum TipoUsuario
    {
        Administrador,
        Comum,
    }
}
