using GoogleGson;
using NetBank.Domain.Entidades;
using NetBank.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace NetBank.UI.Mobile.Services
{
    public class ApiService
    {
        private HttpClient _http;

        public ApiService()
        {
            var enderecoApi = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5090"
                : "http://localhost:5090";


            _http = new HttpClient()
            {
                BaseAddress = new Uri(enderecoApi)
            };

            var token = SecureStorage.GetAsync("Token").Result;

            if (token != null)
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", token);
            }
        }

        private string TrataMensagemErro(Dictionary<string, IEnumerable<string>> dict)
        {
            var mensagemBuilder = new StringBuilder();
            mensagemBuilder.Append("Erro ao enviar transação \n");
            foreach (var erros in dict)
            {
                foreach (var erro in erros.Value)
                {
                    mensagemBuilder.Append($"- {erro} \n");
                }
            }

            return mensagemBuilder.ToString();
        }



        public async Task CadastrarUsuario(UsuarioDTO usuario)
        {
            var req = await _http.PostAsJsonAsync("api/v1/usuario/cadastro", usuario);

            var res = await req.Content.ReadAsStringAsync();

            if (!req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<ErroDTO>(res);

                var mensagem = TrataMensagemErro(json.Errors);
                throw new ApplicationException(mensagem);
            }
        }



        public async Task<UsuarioAutenticacaoDTO> AutenticarUsuario(LoginDTO login)
        {
            var req = await _http.PostAsJsonAsync("api/v1/usuario/login", login);

            var res = await req.Content.ReadAsStringAsync();

            if (!req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<ErroDTO>(res);

                var mensagem = TrataMensagemErro(json.Errors);
                throw new ApplicationException(mensagem);
            }
            else
            {
                var json = JsonConvert.DeserializeObject<UsuarioAutenticacaoDTO>(res);
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", json.Token);
                return json;
            }
        }



        public async Task<Usuario> ValidaToken()
        {
            var req = await _http.GetAsync("api/v1/usuario/token-valido");

            var res = await req.Content.ReadAsStringAsync();

            if (req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<Usuario>(res);
                return json;
            }
            else
            {
                throw new ApplicationException("Faça login novamente");
            }
        }



        public async Task<IEnumerable<ContaDTO>> GetContasUsuario()
        {
            var req = await _http.GetAsync("api/v1/conta");
            var res = await req.Content.ReadAsStringAsync();

            if (req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<List<ContaDTO>>(res);
                return json;
            }
            else
            {
                throw new ApplicationException(res);
            }
        }



        public async Task<ContaDTO> GetContaUsuarioPorId(string id)
        {
            var req = await _http.GetAsync($"api/v1/conta/{id}");
            var res = await req.Content.ReadAsStringAsync();

            if (req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<ContaDTO>(res);
                return json;
            }
            else
            {
                throw new ApplicationException(req.ReasonPhrase);
            }
        }



        public async Task<IEnumerable<TransacaoDTO>> GetTransacoesConta(
            string idConta, DateTime dataInicial, DateTime dataFinal, string descricao = "")
        {
            var req = await _http.GetAsync($"api/v1/transacoes/{idConta}?dataInicial={dataInicial:yyyy-MM-dd}&dataFinal={dataFinal:yyyy-MM-dd}&descricao={descricao}");

            var res = await req.Content.ReadAsStringAsync();

            if (req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<List<TransacaoDTO>>(res);
                return json;
            }
            else
            {
                throw new ApplicationException(req.ReasonPhrase);
            }
        }



        public async Task EnviarTransacao(string idConta, CriarTransacaoDTO criarTransacaoDTO)
        {
            var req = await _http.PostAsJsonAsync($"api/v1/conta/{idConta}/nova-transacao", criarTransacaoDTO);
            var res = await req.Content.ReadAsStringAsync();

            if (!req.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<ErroDTO>(res);

                var mensagem = TrataMensagemErro(json.Errors);
                throw new ApplicationException(mensagem);
            }
        }
    }
}
