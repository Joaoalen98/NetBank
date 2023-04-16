using NetBank.Domain.Entidades;
using NetBank.Domain.Enums;

namespace NetBank.Infra.Services
{
    public static class ContaService
    {
        public static Conta GerarConta(Usuario usuario, TipoContaEnum tipoConta, string numeroConta)
        {
            var conta = new Conta()
            {
                Id = Guid.NewGuid().ToString(),
                UsuarioId = usuario.Id,
                Agencia = "0001",
                Numero = numeroConta,
                Tipo = tipoConta,
            };

            return conta;
        }


        public static string GerarNumeroConta()
        {
            Random random = new();

            string numeroConta = "";
            for (int i = 0; i < 5; i++)
            {
                numeroConta += random.Next(0, 10).ToString();
            }
            return numeroConta;
        }
    }
}
