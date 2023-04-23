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
            var contaEnviou = await ObterPorId(idContaEnviou, false);

            if (valor > contaEnviou.ValorEmConta)
            {
                throw new ApplicationException("O valor a ser enviado é maior do que o valor disponível");
            }

            var contaReceber = await ObterPorAgenciaNumero(agenciaContaReceber, numeroContaReceber, false);

            if (contaReceber.Id == idContaEnviou)
            {
                throw new ApplicationException("Não é possivel tranferir para a mesma conta");
            }


            try
            {
                var transacaoContaEnviou = new Transacao
                {
                    ContaRecebeuId = contaReceber.Id,
                    Valor = valor * -1,
                    DataOperacao = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    ContaId = contaEnviou.Id,
                    ContaEnviouAgencia = contaEnviou.Agencia,
                    ContaEnviouNumero = contaEnviou.Numero,
                    ContaRecebeuAgencia = contaReceber.Agencia,
                    ContaRecebeuNumero = contaReceber.Numero,
                    Descricao = $"Transferência enviada para {contaReceber.Agencia}-{contaReceber.Numero}"
                };
                await _transacaoRepo.Criar(transacaoContaEnviou);


                var transacaoContaRecebeu = new Transacao
                {
                    ContaRecebeuId = contaReceber.Id,
                    Valor = valor,
                    DataOperacao = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    ContaId = contaReceber.Id,
                    ContaRecebeuAgencia = contaReceber.Agencia,
                    ContaRecebeuNumero = contaReceber.Numero,
                    ContaEnviouAgencia = contaEnviou.Agencia,
                    ContaEnviouNumero = contaEnviou.Numero,
                    Descricao = $"Transferência recebida de {contaEnviou.Agencia}-{contaEnviou.Numero}"
                };
                await _transacaoRepo.Criar(transacaoContaRecebeu);

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<IEnumerable<Conta>> ObterContasUsuario(string usuarioId, bool transacoes)
        {
            var query = Set
                .Where(x => x.UsuarioId == usuarioId);

            if (transacoes)
            {
                query = query.Include(x => x.Transacoes);
            }

            return await query.ToListAsync();
        }



        public async Task<Conta> ObterPorAgenciaNumero(
            string agencia, string numero, bool transacoes)
        {
            var conta = Set.Where(
                x => x.Agencia == agencia
                && x.Numero == numero);

            if (!conta.Any())
            {
                throw new ApplicationException("Conta não encontrada");
            }

            if (transacoes)
            {
                conta = conta.Include(x => x.Transacoes);
            }

            return await conta.FirstOrDefaultAsync();
        }



        public async Task<Conta> ObterPorId(string id, bool transacoes)
        {
            var query = Set.Where(x => x.Id == id);

            if (!query.Any())
            {
                throw new ApplicationException("Conta não encontrada");
            }

            if (transacoes)
            {
                query = query.Include(x => x.Transacoes);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
