using NetBank.Domain.Entidades;

namespace NetBank.DTOs
{
    public class TransacaoDTO
    {
        public string Id { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public string ContaEnviouId { get; set; }

        public string ContaEnviouAgencia { get; set; }

        public string ContaEnviouNumero { get; set; }

        public string ContaRecebeuId { get; set; }

        public string ContaRecebeuAgencia { get; set; }

        public string ContaRecebeuNumero { get; set; }

        public DateTime DataOperacao { get; set; }




        public static IEnumerable<TransacaoDTO> ObterListaTransacaoDTO(IEnumerable<Transacao> transacoes)
        {
            var listaTransacaoDTO = new List<TransacaoDTO>();
            foreach (var transacao in transacoes)
            {
                listaTransacaoDTO.Add(ObterTransacaoDTO(transacao));
            }
            return listaTransacaoDTO;
        }


        public static TransacaoDTO ObterTransacaoDTO(Transacao transacao)
        {
            return new TransacaoDTO
            {
                Id = transacao.Id,
                ContaEnviouId = transacao.ContaId,
                ContaEnviouNumero = transacao.ContaEnviouNumero,
                ContaEnviouAgencia = transacao.ContaEnviouAgencia,
                ContaRecebeuAgencia = transacao.ContaRecebeuAgencia,
                ContaRecebeuId = transacao.ContaRecebeuId,
                ContaRecebeuNumero = transacao.ContaRecebeuNumero,
                DataOperacao = transacao.DataOperacao,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor
            };
        }
    }
}
