using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Enums
{
    public enum TipoContaEnum
    {
        [Display(Name = "Conta Corrente")]
        ContaCorrente = 1,

        [Display(Name = "Conta Poupança")]
        ContaPoupanca = 2,
    }
}
