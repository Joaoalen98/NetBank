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

        public Task Criar(Conta entidade)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(string id)
        {
            throw new NotImplementedException();
        }

        public Task Editar(Conta entidade)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Conta>> ObterContasUsuario(string usuarioId, bool transacoes)
        {
            throw new NotImplementedException();
        }

        public Task<Conta> ObterPorId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
