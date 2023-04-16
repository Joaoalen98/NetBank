using Microsoft.IdentityModel.Tokens;
using NetBank.Domain.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetBank.Infra.Services
{
    public static class TokenService
    {
        public static string GerarTokenUsuario(Usuario usuario)
        {
            var key = Environment.GetEnvironmentVariable("JwtKey")!;
            var bytesKey = Encoding.ASCII.GetBytes(key);

            var tokenHanlder = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(4),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email, usuario.Email),
                    new(ClaimTypes.Name, usuario.NomeCompleto),
                    new(ClaimTypes.OtherPhone, usuario.Telefone),
                    new("Id", usuario.Id)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHanlder.CreateToken(tokenDescriptor);

            return tokenHanlder.WriteToken(token);
        }
    }
}
