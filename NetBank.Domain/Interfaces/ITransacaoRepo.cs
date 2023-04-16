using NetBank.Domain.Entidades;

namespace NetBank.Domain.Interfaces
{
    public interface ITransacaoRepo : IBaseRepo<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterPorFiltros(DateTime? dataInicial, DateTime? dataFinal, string? descricao, decimal? valor);
    }
}
