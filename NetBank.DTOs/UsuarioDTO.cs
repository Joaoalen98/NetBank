using System.ComponentModel.DataAnnotations;

namespace NetBank.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O nome deve ser informado")]
        public string NomeCompleto { get; set; }


        [Required(ErrorMessage = "O email deve ser informado")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O telefone deve ser informado")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "O CPF deve ser informado")]
        [RegularExpression("\\d{11}", ErrorMessage = "Informe um CPF válido, com 11 caracteres sem pontos e outros sinais")]
        public string CPF { get; set; }


        [Required(ErrorMessage = "A Data de nascimento deve ser informada")]
        public DateTime DataNascimento { get; set; }


        [Required(ErrorMessage = "A senha deve ser informada")]
        public string Senha { get; set; }
    }
}
