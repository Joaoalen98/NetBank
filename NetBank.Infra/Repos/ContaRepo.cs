using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;

namespace NetBank.Infra.Repos
{
    public class ContaRepo : BaseRepo<Conta>, IContaRepo
    {
        public ContaRepo(AppDbContext context) : base(context)
        {
        }

        public async Task Criar(Conta entidade)
        {
            if (await Set.FirstOrDefaultAsync(x => x.Numero == entidade.Numero) != null)
            {
                throw new ApplicationException("Conta ja registrada com este número");
            }

            await Set.AddRangeAsync(entidade);
            await Context.SaveChangesAsync();
        }


        public async Task Editar(Conta entidade)
        {
            Set.Update(entidade);
            await Context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Conta>> ObterContasUsuario(string usuarioId, bool transacoesRecebidas, bool transacoesEnviadas)
        {
            var query = Set
                .Where(x => x.UsuarioId == usuarioId);

            if (transacoesRecebidas)
            {
                query = query.Include(x => x.TransacoesRecebidas);
            }

            if (transacoesEnviadas)
            {
                query = query.Include(x => x.TransacoesEnviadas);
            }

            return await query.ToListAsync();
        }



        public async Task<Conta> ObterPorId(string id, bool transacoesRecebidas, bool transacoesEnviadas)
        {
            var query = Set.Where(x => x.Id == id);

            if (!query.Any())
            {
                throw new ApplicationException("Conta não encontrada");
            }

            if (transacoesRecebidas)
            {
                query = query.Include(x => x.TransacoesRecebidas);
            }

            if (transacoesEnviadas)
            {
                query = query.Include(x => x.TransacoesEnviadas);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
