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

            modelBuilder.Entity<Transacao>()
                .HasOne(x => x.ContaEnviou)
                .WithMany(x => x.TransacoesEnviadas)
                .HasForeignKey(x => x.ContaEnviouId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Transacao>()
                .HasOne(x => x.ContaRecebeu)
                .WithMany(x => x.TransacoesRecebidas)
                .HasForeignKey(x => x.ContaRecebeuId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
