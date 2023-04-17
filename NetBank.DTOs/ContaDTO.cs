using NetBank.Domain.Entidades;

namespace NetBank.DTOs
{
    public class ContaDTO
    {
        public string Id { get; set; }

        public string Agencia { get; set; }

        public string Numero { get; set; }

        public string Tipo { get; set; }

        public string UsuarioId { get; set; }

        public decimal ValorEmConta { get; set; }

        public virtual IEnumerable<Transacao> TransacoesRecebidas { get; set; }

        public virtual IEnumerable<Transacao> TransacoesEnviadas { get; set; }



        public ContaDTO(Conta conta)
        {
            Id = conta.Id;
            Agencia = conta.Agencia;
            Numero = conta.Numero;
            Tipo = conta.Tipo.ToString();
            UsuarioId = conta.UsuarioId;
            ValorEmConta = conta.ValorEmConta;
            TransacoesEnviadas = conta.TransacoesEnviadas;
            TransacoesRecebidas = conta.TransacoesRecebidas;
        }



        public static IEnumerable<ContaDTO> ObterContasViewModel(IEnumerable<Conta> contas)
        {
            List<ContaDTO> contasViewModel = new();
            foreach (var conta in contas)
            {
                var contaViewModel = new ContaDTO(conta);
                contasViewModel.Add(contaViewModel);
            }
            return contasViewModel;
        }
    }
}
