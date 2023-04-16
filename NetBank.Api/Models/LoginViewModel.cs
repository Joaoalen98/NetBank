using System.ComponentModel.DataAnnotations;

namespace NetBank.Api.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF deve ser informado")]
        public string CPF { get; set; }


        [Required(ErrorMessage = "A senha deve ser informada")]
        public string Senha { get; set; }
    }
}
