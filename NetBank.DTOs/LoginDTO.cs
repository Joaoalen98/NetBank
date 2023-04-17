using System.ComponentModel.DataAnnotations;

namespace NetBank.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O CPF deve ser informado")]
        [RegularExpression("\\d{11}", ErrorMessage = "Informe um CPF válido, com 11 caracteres sem pontos e outros sinais")]
        public string CPF { get; set; }


        [Required(ErrorMessage = "A senha deve ser informada")]
        public string Senha { get; set; }
    }
}
