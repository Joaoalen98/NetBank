using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBank.Api.Models;
using NetBank.Domain.Entidades;
using NetBank.Domain.Interfaces;

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
        [ProducesResponseType(typeof(IEnumerable<ContaViewModel>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> ObterContas()
        {
            try
            {
                var id = User.FindFirst("Id")!.Value;
                var contas = ContaViewModel.ObterContasViewModel(await _contaRepo.ObterContasUsuario(id, true, true));
                return Ok(contas);
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
