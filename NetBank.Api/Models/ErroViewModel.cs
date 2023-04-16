namespace NetBank.Api.Models
{
    public class ErroViewModel
    {
        public string Mensagem { get; set; }

        public string? StackTrace { get; set; }

        public ErroViewModel(string mensagem, string stackTrace = null)
        {
            Mensagem = mensagem;
            StackTrace = stackTrace;

        }
    }
}
