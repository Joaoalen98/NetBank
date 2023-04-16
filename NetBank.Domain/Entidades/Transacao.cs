using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Domain.Entidades
{
    public class Transacao : BaseEntidade
    {
        [Column(TypeName = "decimal(20, 2)")]
        public decimal Valor { get; set; }

        public string Descricao { get; set; }



        public string ContaEnviouId { get; set; }

        [InverseProperty("TransacoesEnviadas")]
        public virtual Conta ContaEnviou { get; set; }



        public string ContaRecebeuId { get; set; }

        [InverseProperty("TransacoesRecebidas")]
        public virtual Conta ContaRecebeu { get; set; }



        public DateTime DataOperacao { get; set; }
    }
}
