using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    internal class BibliotecaContext : DbContext
    {
        protected string connetion = "Server=localhost;DataBase=biblioteca;Uid=root;Password=root";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseMySql(connetion,ServerVersion.AutoDetect(connetion))
                .EnableSensitiveDataLogging();
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
