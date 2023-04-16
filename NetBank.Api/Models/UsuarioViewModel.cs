using NetBank.Domain.Entidades;

namespace NetBank.Api.Models
{
    public class UsuarioViewModel
    {
        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Senha { get; set; }
    }
}
