using NetBank.Domain.Entidades;

namespace NetBank.Api.Models
{
    public class ContaViewModel
    {
        public string Id { get; set; }

        public string Agencia { get; set; }

        public string Numero { get; set; }

        public string Tipo { get; set; }

        public string UsuarioId { get; set; }

        public decimal ValorEmConta { get; set; }

        public virtual IEnumerable<Transacao> TransacoesRecebidas { get; set; }

        public virtual IEnumerable<Transacao> TransacoesEnviadas { get; set; }



        public ContaViewModel(Conta conta)
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



        public static IEnumerable<ContaViewModel> ObterContasViewModel(IEnumerable<Conta> contas)
        {
            List<ContaViewModel> contasViewModel = new();
            foreach (var conta in contas)
            {
                var contaViewModel = new ContaViewModel(conta);
                contasViewModel.Add(contaViewModel);
            }
            return contasViewModel;
        }
    }
}
