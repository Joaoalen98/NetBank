using NetBank.Domain.Enums;

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
    }
}
