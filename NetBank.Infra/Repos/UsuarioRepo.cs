using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;
using NetBank.Infra.Services;

namespace NetBank.Infra.Repos
{
    public class UsuarioRepo : BaseRepo<Usuario>, IUsuarioRepo
    {
        public UsuarioRepo(AppDbContext context) : base(context)
        {
        }

        public async Task Criar(Usuario entidade)
        {
            if (await Set.FirstOrDefaultAsync(x => x.Email == entidade.Email) != null)
            {
                throw new ApplicationException("Usuario ja cadastrado com o email informado");
            }

            if (await Set.FirstOrDefaultAsync(x => x.CPF == entidade.CPF) != null)
            {
                throw new ApplicationException("Usuario ja cadastrado com o CPF informado");
            }

            if (entidade.DataNascimento == DateTime.MinValue)
            {
                throw new ApplicationException("Informe uma data de nascimento valida");
            }

            await Set.AddAsync(entidade);
            await Context.SaveChangesAsync();
        }


        public async Task Editar(Usuario entidade)
        {
            Set.Update(entidade);
            await Context.SaveChangesAsync();
        }



        public async Task<Usuario> ObterPorCpfSenha(string cpf, string senha)
        {
            var usuario = await Set.FirstOrDefaultAsync(x => x.CPF == cpf);

            if (usuario == null)
            {
                throw new ApplicationException("CPF ou senha incorreta");
            }

            var correta = HashService.ComparaSenha(senha, usuario.Senha);

            if (!correta)
            {
                throw new ApplicationException("CPF ou senha incorreta");
            }

            return usuario;
        }



        public Task<Usuario> ObterPorId(string id)
        {
            var usuario =  Set.FirstOrDefaultAsync(x => x.Id == id)!;

            if (usuario == null)
            {
                throw new ApplicationException("Usuario não encontrado");
            }

            return usuario;
        }
    }
}
