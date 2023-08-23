using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    internal class BibliotecaContext : DbContext
    {
        protected string connection = "Server=localhost;DataBase=biblioteca;Uid=root;Password=root";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseInMemoryDatabase("Biblioteca")
                .EnableSensitiveDataLogging();
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
