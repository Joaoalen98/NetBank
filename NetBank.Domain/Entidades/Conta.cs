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

        public virtual IEnumerable<Transacao> Transacoes { get; set; }


        [NotMapped]
        public decimal ValorEmConta { get => ObterValorEmConta(); }



        public Conta()
        {
            Transacoes = new List<Transacao>();
        }


        private decimal ObterValorEmConta()
        {
            var valor = Transacoes.Sum(x => x.Valor);
            return valor;
        }
    }
}
