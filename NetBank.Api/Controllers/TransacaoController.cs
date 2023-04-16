using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBank.Api.Models;
using NetBank.Domain.Interfaces;

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
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                var erro = new ErrorModel(400);
                erro.Status = 400;
                erro.Errors.Add("Outro", new string[] { ex.Message });
                return BadRequest(erro);
            }
        }
    }
}
