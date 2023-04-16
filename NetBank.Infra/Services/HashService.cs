using static BCrypt.Net.BCrypt;

namespace NetBank.Infra.Services
{
    public static class HashService
    {
        public static string HashSenha(string senha)
            => HashPassword(senha);


        public static bool ComparaSenha(string senha, string hash)
            => Verify(senha, hash);
    }
}
