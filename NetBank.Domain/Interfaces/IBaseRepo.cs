using NetBank.Domain.Entidades;

namespace NetBank.Domain.Interfaces
{
    public interface IBaseRepo<T> where T : BaseEntidade
    {
        Task Criar(T entidade);

        Task Editar(T entidade);
    }
}
