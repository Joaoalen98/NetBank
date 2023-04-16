using NetBank.Domain.Entidades;

namespace NetBank.Domain.Interfaces
{
    public interface ITransacaoRepo : IBaseRepo<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterPorFiltros(
            string contaId, DateTime? dataInicial, DateTime? dataFinal, string? descricao, decimal? valor);

        Task<Transacao> ObterPorId(string id, bool contaEnviou, bool contaRecebeu);
    }
}
