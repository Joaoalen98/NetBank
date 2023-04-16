using NetBank.Domain.Entidades;

namespace NetBank.Api.Models
{
    public class UsuarioAutenticacaoViewModel
    {
        public Usuario Usuario { get; set; }

        public string Token { get; set; }


        public UsuarioAutenticacaoViewModel(Usuario usuario, string token)
        {
            Usuario = usuario;
            Token = token;
        }
    }
}
