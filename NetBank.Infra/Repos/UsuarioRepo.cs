using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;

namespace NetBank.Infra.Repos
{
    public class UsuarioRepo : BaseRepo<Usuario>, IUsuarioRepo
    {
        public UsuarioRepo(AppDbContext context) : base(context)
        {
        }

        public Task Criar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(string id)
        {
            throw new NotImplementedException();
        }

        public Task Editar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObterPorCpfSenha(string cpf, string senha)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObterPorId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
