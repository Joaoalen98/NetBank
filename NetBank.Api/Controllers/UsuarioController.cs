using Microsoft.AspNetCore.Mvc;
using NetBank.Api.Models;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.Infra.Services;

namespace NetBank.Api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepo _usuarioRepo;

        public UsuarioController(IUsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }



        [HttpPost]
        [Route("cadastro")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErroViewModel), 400)]
        public async Task<IActionResult> Criar([FromBody] UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario
                    {
                        Ativo = true,
                        Cpf = model.Cpf,
                        DataNascimento = model.DataNascimento,
                        Email = model.Email,
                        Id = Guid.NewGuid().ToString(),
                        NomeCompleto = model.NomeCompleto,
                        Senha = HashService.HashSenha(model.Senha),
                        Telefone = model.Telefone,
                    };

                    await _usuarioRepo.Criar(usuario);

                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ErroViewModel(ex.Message));
                }
            }

            return BadRequest(model);
        }



        [HttpPost]
        [Route("login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErroViewModel), 400)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = await _usuarioRepo.ObterPorCpfSenha(model.Cpf, model.Senha);
                    var token = TokenService.GerarTokenUsuario(usuario);

                    return StatusCode(200, new { usuario, token });
                }
                catch (Exception ex)
                {
                    return BadRequest(new ErroViewModel(ex.Message));
                }
            }

            return BadRequest(model);
        }
    }
}
