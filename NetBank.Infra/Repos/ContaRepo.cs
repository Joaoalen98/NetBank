using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Data;

namespace NetBank.Infra.Repos
{
    public class ContaRepo : BaseRepo<Conta>, IContaRepo
    {
        private ITransacaoRepo _transacaoRepo;

        public ContaRepo(AppDbContext context, ITransacaoRepo transacaoRepo) : base(context)
        {
            _transacaoRepo = transacaoRepo;
        }

        public async Task Criar(Conta entidade)
        {
            if (await Set.FirstOrDefaultAsync(x => x.Numero == entidade.Numero) != null)
            {
                throw new ApplicationException("Conta ja registrada com este número");
            }

            await Set.AddRangeAsync(entidade);
            await Context.SaveChangesAsync();
        }


        public async Task Editar(Conta entidade)
        {
            Set.Update(entidade);
            await Context.SaveChangesAsync();
        }



        public async Task EnviarTransacao(
            string idContaEnviou, string agenciaContaReceber, string numeroContaReceber, decimal valor)
        {
            var contaEnviou = await ObterPorId(idContaEnviou, true, true);

            if (valor > contaEnviou.ValorEmConta)
            {
                throw new ApplicationException("O valor a ser enviado é maior do que o valor disponível");
            }

            var contaReceber = await ObterPorAgenciaNumero(agenciaContaReceber, numeroContaReceber, false, false);

            if (contaReceber.Id == idContaEnviou)
            {
                throw new ApplicationException("Não é possivel tranferir para a mesma conta");
            }

            var transacao = new Transacao
            {
                ContaEnviouId = contaEnviou.Id,
                ContaRecebeuId = contaReceber.Id,
                Valor = valor,
                DataOperacao = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
            };

            transacao.Descricao = $"Transação de {valor:c} - {transacao.DataOperacao:dd/MM/yyyy}";

            await _transacaoRepo.Criar(transacao);
        }



        public async Task<IEnumerable<Conta>> ObterContasUsuario(string usuarioId, bool transacoesRecebidas, bool transacoesEnviadas)
        {
            var query = Set
                .Where(x => x.UsuarioId == usuarioId);

            if (transacoesRecebidas)
            {
                query = query.Include(x => x.TransacoesRecebidas);
            }

            if (transacoesEnviadas)
            {
                query = query.Include(x => x.TransacoesEnviadas);
            }

            return await query.ToListAsync();
        }



        public async Task<Conta> ObterPorAgenciaNumero(
            string agencia, string numero, bool transacoesRecebidas, bool transacoesEnviadas)
        {
            var conta = Set.Where(
                x => x.Agencia == agencia
                && x.Numero == numero);

            if (!conta.Any())
            {
                throw new ApplicationException("Conta não encontrada");
            }

            if (transacoesRecebidas)
            {
                conta = conta.Include(x => x.TransacoesRecebidas);
            }

            if (transacoesEnviadas)
            {
                conta = conta.Include(x => x.TransacoesEnviadas);
            }

            return await conta.FirstOrDefaultAsync();
        }



        public async Task<Conta> ObterPorId(string id, bool transacoesRecebidas, bool transacoesEnviadas)
        {
            var query = Set.Where(x => x.Id == id);

            if (!query.Any())
            {
                throw new ApplicationException("Conta não encontrada");
            }

            if (transacoesRecebidas)
            {
                query = query.Include(x => x.TransacoesRecebidas);
            }

            if (transacoesEnviadas)
            {
                query = query.Include(x => x.TransacoesEnviadas);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
