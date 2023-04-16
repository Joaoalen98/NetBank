using NetBank.Domain.Entidades;

namespace NetBank.Domain.Interfaces
{
    public interface IUsuarioRepo : IBaseRepo<Usuario>
    {
        Task<Usuario> ObterPorCpfSenha(string cpf, string senha);
    }
}
