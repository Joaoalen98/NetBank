using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Entidades;

namespace NetBank.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Transacao> Transacoes { get; set; }

        public DbSet<Conta> Contas { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
