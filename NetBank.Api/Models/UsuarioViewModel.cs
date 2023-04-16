using System.ComponentModel.DataAnnotations;

namespace NetBank.Api.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O nome deve ser informado")]
        public string NomeCompleto { get; set; }


        [Required(ErrorMessage = "O email deve ser informado")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O telefone deve ser informado")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "O CPF deve ser informado")]
        public string CPF { get; set; }


        [Required(ErrorMessage = "A Data de nascimento deve ser informada")]
        public DateTime DataNascimento { get; set; }


        [Required(ErrorMessage = "A senha deve ser informada")]
        public string Senha { get; set; }
    }
}
