using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Domain.Entidades
{
    public class Transacao : BaseEntidade
    {
        [Column(TypeName = "decimal(20, 2)")]
        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public string ContaId { get; set; }

        public virtual Conta Conta { get; set; }

        public string ContaEnviouAgencia { get; set; }

        public string ContaEnviouNumero { get; set; }

        public string ContaRecebeuId { get; set; }

        public string ContaRecebeuAgencia { get; set; }

        public string ContaRecebeuNumero { get; set; }

        public DateTime DataOperacao { get; set; }
    }
}
