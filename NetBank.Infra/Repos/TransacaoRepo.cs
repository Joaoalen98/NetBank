using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;

namespace NetBank.Infra.Repos
{
    public class TransacaoRepo : BaseRepo<Transacao>, ITransacaoRepo
    {
        public TransacaoRepo(AppDbContext context) : base(context)
        {
        }

        public async Task Criar(Transacao entidade)
        {
            await Set.AddAsync(entidade);
            await Context.SaveChangesAsync();
        }



        public async Task Editar(Transacao entidade)
        {
            Set.Update(entidade);
            await Context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Transacao>> ObterPorFiltros(
            string contaId, DateTime? dataInicial, DateTime? dataFinal, string? descricao, decimal? valor)
        {
            var query = Set
                .Include(x => x.ContaRecebeu)
                .Include(x => x.ContaEnviou)
                .Where(x => x.ContaRecebeuId == contaId
                || x.ContaEnviouId == contaId);

            if (dataInicial != null)
            {
                query = query.Where(x => x.DataOperacao >= dataInicial);
            }

            if (dataFinal != null)
            {
                query = query.Where(x => x.DataOperacao <= dataFinal);
            }

            if (!string.IsNullOrEmpty(descricao))
            {
                query = query.Where(x => x.Descricao.ToUpper().Contains(descricao.ToUpper()));
            }

            if (valor != null)
            {
                query = query.Where(x => (int)x.Valor == (int)valor);
            }

            return await query.ToListAsync();
        }



        public async Task<Transacao> ObterPorId(string id, bool contaEnviou, bool contaRecebeu)
        {
            var query = Set.Where(x => x.Id == id)!;

            if (!query.Any())
            {
                throw new ApplicationException("Transação não encontrada");
            }

            if (contaEnviou)
            {
                query = query.Include(x => x.ContaEnviou);
            }

            if (contaRecebeu)
            {
                query = query.Include(x => x.ContaRecebeu);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
