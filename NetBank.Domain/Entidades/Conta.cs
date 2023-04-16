using NetBank.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Domain.Entidades
{
    public class Conta : BaseEntidade
    {
        public string Agencia { get; set; }

        public string Numero { get; set; }

        public TipoContaEnum Tipo { get; set; }

        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual IEnumerable<Transacao> TransacoesRecebidas { get; set; }

        public virtual IEnumerable<Transacao> TransacoesEnviadas { get; set; }


        [NotMapped]
        public decimal ValorEmConta { get => ObterValorEmConta(); }



        public Conta()
        {
            TransacoesEnviadas = new List<Transacao>();
            TransacoesRecebidas = new List<Transacao>();
        }


        private decimal ObterValorEmConta()
        {
            var valor = TransacoesRecebidas.Sum(x => x.Valor) + TransacoesEnviadas.Sum(x => x.Valor);
            return valor;
        }
    }
}
