using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBank.Domain.Interfaces;
using NetBank.DTOs;

namespace NetBank.Api.Controllers
{
    [Authorize]
    [Route("api/v1/conta")]
    [ApiController]
    [ProducesResponseType(401)]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepo _contaRepo;

        public ContaController(IContaRepo contaRepo)
        {
            _contaRepo = contaRepo;
        }



        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContaDTO>), 200)]
        [ProducesResponseType(typeof(ErroDTO), 400)]
        public async Task<IActionResult> ObterContas()
        {
            try
            {
                var id = User.FindFirst("Id")!.Value;
                var contas = ContaDTO.ObterContasViewModel(await _contaRepo.ObterContasUsuario(id, true, true));
                return Ok(contas);
            }
            catch (Exception ex)
            {
                var erro = new ErroDTO(400);
                erro.Status = 400;
                erro.Errors.Add("Outro", new string[] { ex.Message });
                return BadRequest(erro);
            }
        }



        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ContaDTO), 200)]
        [ProducesResponseType(typeof(ErroDTO), 400)]
        public async Task<IActionResult> ObterContaPorId([FromRoute] string id)
        {
            try
            {
                var contas = new ContaDTO(await _contaRepo.ObterPorId(id, true, true));
                return Ok(contas);
            }
            catch (Exception ex)
            {
                var erro = new ErroDTO(400);
                erro.Status = 400;
                erro.Errors.Add("Outro", new string[] { ex.Message });
                return BadRequest(erro);
            }
        }



        [HttpPost]
        [Route("{id}/nova-transacao")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErroDTO), 400)]
        public async Task<IActionResult> NovaTransacao(
            [FromRoute] string id,
            [FromBody] CriarTransacaoDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contaRepo.EnviarTransacao(id, model.Agencia, model.Numero, model.Valor);
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
    }
}
