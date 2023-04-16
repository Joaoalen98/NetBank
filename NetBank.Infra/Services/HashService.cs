using static BCrypt.Net.BCrypt;

namespace NetBank.Infra.Services
{
    public static class HashService
    {
        private static string _salt = GenerateSalt(8);


        public static string HashSenha(string senha)
            => HashPassword(senha, _salt);


        public static bool ComparaSenha(string senha, string hash)
            => Verify(senha, hash);
    }
}
