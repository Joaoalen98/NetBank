using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetBank.Api.Models;
using NetBank.Domain.Entidades;
using NetBank.Domain.Enums;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Services;

namespace NetBank.Api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly IContaRepo _contaRepo;

        public UsuarioController(IUsuarioRepo usuarioRepo, IContaRepo contaRepo)
        {
            _usuarioRepo = usuarioRepo;
            _contaRepo = contaRepo;

        }


        private async Task CriarContaUsuario(Usuario usuario, TipoContaEnum tipoConta)
        {
            while (true)
            {
                try
                {
                    var numeroConta = ContaService.GerarNumeroConta();
                    var conta = ContaService.GerarConta(usuario, tipoConta, numeroConta);

                    await _contaRepo.Criar(conta);

                    break;
                }
                catch (Exception)
                {

                }
            }
        }



        [HttpPost]
        [Route("cadastro")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> Criar([FromBody] UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Ativo = true,
                    CPF = model.CPF,
                    DataNascimento = model.DataNascimento,
                    Email = model.Email,
                    Id = Guid.NewGuid().ToString(),
                    NomeCompleto = model.NomeCompleto,
                    Senha = HashService.HashSenha(model.Senha),
                    Telefone = model.Telefone,
                };

                try
                {
                    await _usuarioRepo.Criar(usuario);

                    await CriarContaUsuario(usuario, TipoContaEnum.ContaCorrente);
                    await CriarContaUsuario(usuario, TipoContaEnum.ContaPoupanca);

                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    var erro = new ErrorModel(400);
                    erro.Status = 400;
                    erro.Errors.Add("Outro", new string[] { ex.Message });
                    return BadRequest(erro);
                }
            }

            return BadRequest();
        }



        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(UsuarioAutenticacaoViewModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = await _usuarioRepo.ObterPorCpfSenha(model.CPF, model.Senha);
                    var token = TokenService.GerarTokenUsuario(usuario);
                    usuario.Senha = "";

                    return StatusCode(200, new UsuarioAutenticacaoViewModel(usuario, token));
                }
                catch (Exception ex)
                {
                    var erro = new ErrorModel(400);
                    erro.Errors.Add("Outro", new string[] { ex.Message });
                    return BadRequest(erro);
                }
            }

            return BadRequest();
        }
    }
}
