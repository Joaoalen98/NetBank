using NetBank.Domain.Entidades;

namespace NetBank.DTOs
{
    public class UsuarioAutenticacaoDTO
    {
        public Usuario Usuario { get; set; }

        public string Token { get; set; }


        public UsuarioAutenticacaoDTO(Usuario usuario, string token)
        {
            Usuario = usuario;
            Token = token;
        }
    }
}
