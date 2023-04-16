using System.ComponentModel.DataAnnotations;

namespace NetBank.Api.Models
{
    public class TransacaoViewModel
    {
        [Required(ErrorMessage = "É necessário informar um valor")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "É necessário informar uma agência")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "É necessário informar um número da conta")]
        public string Numero { get; set; }
    }
}
