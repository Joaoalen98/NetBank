using NetBank.Domain.Entidades;
using NetBank.Domain.Enums;

namespace NetBank.DTOs
{
    public class ContaDTO
    {
        public string Id { get; set; }

        public string Agencia { get; set; }

        public string Numero { get; set; }

        public string Tipo { get; set; }

        public virtual IEnumerable<TransacaoDTO> Transacoes { get; set; }

        public decimal ValorEmConta { get; set; }


        public static IEnumerable<ContaDTO> ObterListaContasDTO(IEnumerable<Conta> contas)
        {
            var listaContasDTO = new List<ContaDTO>();
            foreach (var conta in contas)
            {
                listaContasDTO.Add(ObterContaDTO(conta));
            }
            return listaContasDTO;
        }

        public static ContaDTO ObterContaDTO(Conta conta)
        {
            return new ContaDTO
            {
                Id = conta.Id,
                Agencia = conta.Agencia,
                Numero = conta.Numero,
                Tipo = conta.Tipo.GetDisplayName(),
                Transacoes = TransacaoDTO.ObterListaTransacaoDTO(conta.Transacoes),
                ValorEmConta = conta.ValorEmConta
            };
        }
    }
}
