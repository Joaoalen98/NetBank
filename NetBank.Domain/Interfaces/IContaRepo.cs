using NetBank.Domain.Entidades;

namespace NetBank.Domain.Interfaces
{
    public interface IContaRepo : IBaseRepo<Conta>
    {
        Task<IEnumerable<Conta>> ObterContasUsuario(string usuarioId, bool transacoesRecebidas, bool transacoesEnviadas);

        Task<Conta> ObterPorId(string id, bool transacoesRecebidas, bool transacoesEnviadas);

        Task EnviarTransacao(string idContaEnviou, string agenciaContaReceber, string numeroContaReceber, decimal valor);

        Task<Conta> ObterPorAgenciaNumero(string agencia, string numero, bool transacoesRecebidas, bool transacoesEnviadas);
    }
}
