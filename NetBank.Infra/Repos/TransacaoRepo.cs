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

        public Task Criar(Transacao entidade)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(string id)
        {
            throw new NotImplementedException();
        }

        public Task Editar(Transacao entidade)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transacao>> ObterPorFiltros(DateTime? dataInicial, DateTime? dataFinal, string? descricao, decimal? valor)
        {
            throw new NotImplementedException();
        }

        public Task<Transacao> ObterPorId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
