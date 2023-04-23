using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;
using NetBank.DTOs;

namespace NetBank.Api.Controllers
{
    [Authorize]
    [Route("api/v1/transacoes")]
    [ApiController]
    [ProducesResponseType(401)]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoRepo _transacaoRepo;

        public TransacaoController(ITransacaoRepo transacaoRepo)
        {
            _transacaoRepo = transacaoRepo;
        }



        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IEnumerable<TransacaoDTO>), 200)]
        [ProducesResponseType(typeof(ErroDTO), 400)]
        public async Task<IActionResult> ObterTransacoes(
            [FromRoute] string id,
            [FromQuery] DateTime? dataInicial,
            [FromQuery] DateTime? dataFinal,
            [FromQuery] string? descricao,
            [FromQuery] decimal? valor)
        {
            try
            {
                var transacoes = await _transacaoRepo.ObterPorFiltros(id, dataInicial, dataFinal, descricao, valor);
                return Ok(TransacaoDTO.ObterListaTransacaoDTO(transacoes));
            }
            catch (Exception ex)
            {
                var erro = new ErroDTO(400);
                erro.Status = 400;
                erro.Errors.Add("Outro", new string[] { ex.Message });
                return BadRequest(erro);
            }
        }
    }
}
