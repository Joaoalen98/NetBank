using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBank.Domain.Entidades;
using NetBank.Domain.Enums;
using NetBank.Domain.Interfaces;
using NetBank.DTOs;
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


        private async Task<Conta> CriarContaUsuario(Usuario usuario, TipoContaEnum tipoConta, string? numeroContaCorrente)
        {
            Conta conta;
            while (true)
            {
                try
                {
                    string numeroConta;
                    if (tipoConta == TipoContaEnum.ContaPoupanca)
                    {
                        numeroConta = numeroContaCorrente!.Split('-')[0] + "-2";
                    }
                    else
                    {
                        numeroConta = ContaService.GerarNumeroConta() + "-1";
                    }
                    conta = ContaService.GerarConta(usuario, tipoConta, numeroConta);

                    await _contaRepo.Criar(conta);

                    break;
                }
                catch (Exception)
                {

                }
            }
            return conta;
        }



        [HttpPost]
        [Route("cadastro")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErroDTO), 400)]
        public async Task<IActionResult> Criar([FromBody] UsuarioDTO model)
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

                    var contaCorrente = await CriarContaUsuario(usuario, TipoContaEnum.ContaCorrente, null);
                    await CriarContaUsuario(usuario, TipoContaEnum.ContaPoupanca, contaCorrente.Numero);

                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    var erro = new ErroDTO(400);
                    erro.Status = 400;
                    erro.Errors.Add("Outro", new string[] { ex.Message });
                    return BadRequest(erro);
                }
            }

            return BadRequest();
        }



        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(UsuarioAutenticacaoDTO), 200)]
        [ProducesResponseType(typeof(ErroDTO), 400)]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = await _usuarioRepo.ObterPorCpfSenha(model.CPF, model.Senha);
                    var token = TokenService.GerarTokenUsuario(usuario);
                    return StatusCode(200, new UsuarioAutenticacaoDTO(usuario, token));
                }
                catch (Exception ex)
                {
                    var erro = new ErroDTO(400);
                    erro.Errors.Add("Outro", new string[] { ex.Message });
                    return BadRequest(erro);
                }
            }

            return BadRequest();
        }




        [Authorize]
        [HttpGet]
        [Route("token-valido")]
        public async Task<IActionResult> VerificaTokenValido()
        {
            try
            {
                var id = User.FindFirst("Id")!.Value;
                var usuario = await _usuarioRepo.ObterPorId(id);
                usuario.Senha = "";

                return StatusCode(200, new
                {
                    usuario.NomeCompleto,
                    usuario.Email,
                    usuario.Telefone,
                    usuario.CPF,
                    usuario.DataNascimento,
                    usuario.Ativo,
                    usuario.Id,
                });
            }
            catch (Exception ex)
            {
                var erro = new ErroDTO(400);
                erro.Errors.Add("Outro", new string[] { ex.Message });
                return BadRequest(erro);
            }
        }
    }
}
