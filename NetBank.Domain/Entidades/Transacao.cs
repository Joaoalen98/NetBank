namespace NetBank.Domain.Entidades
{
    public class Transacao : BaseEntidade
    {
        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public string ContaEnviouId { get; set; }

        public virtual Conta ContaEnviou { get; set; }

        public string ContaRecebeuId { get; set; }

        public virtual Conta ContaRecebeu { get; set; }

        public DateTime DataOperacao { get; set; }
    }
}
