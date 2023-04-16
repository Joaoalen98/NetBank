using NetBank.Domain.Entidades;

namespace NetBank.Domain.Interfaces
{
    public interface IContaRepo : IBaseRepo<Conta>
    {
        Task<IEnumerable<Conta>> ObterContasUsuario(string usuarioId, bool transacoes);
    }
}
