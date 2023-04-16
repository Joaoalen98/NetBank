using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Entidades;
using NetBank.Infra.Data;

namespace NetBank.Infra.Repos
{
    public class BaseRepo<T> where T : BaseEntidade
    {
        protected readonly AppDbContext Context;

        protected readonly DbSet<T> Set;

        public BaseRepo(AppDbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }
    }
}
