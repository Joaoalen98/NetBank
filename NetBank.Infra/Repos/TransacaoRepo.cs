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



        public async Task<IEnumerable<Transacao>> ObterPorFiltros(
            string contaId, DateTime? dataInicial, DateTime? dataFinal, string? descricao, decimal? valor)
        {
            var query = Set
                .Where(x => x.ContaId == contaId);

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



        public async Task<Transacao> ObterPorId(string id)
        {
            var query = Set.Where(x => x.Id == id)!;

            if (!query.Any())
            {
                throw new ApplicationException("Transação não encontrada");
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
