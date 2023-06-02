using Ilia.ControleDePonto.Application.Services;
using Ilia.ControleDePonto.Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Ilia.ControleDePonto.API.Controllers
{
    [ApiController]
    [Route("v1/folhas-de-ponto")]
    public class FolhasDePontoController : ControllerBase
    {
        private readonly IRegistroService _registroService;
        private readonly IMesValidation _mesValidation;

        public FolhasDePontoController(IRegistroService registroService,
                                       IMesValidation mesValidation)
        {
            _registroService = registroService;
            _mesValidation = mesValidation;
        }

        [HttpGet("{mes}")]
        public IActionResult GeraRelatorioMensal(string mes)
        {
            (var isBadRequest, var mensagem) = _mesValidation.ValidateMes(mes);
            if (isBadRequest)
                return BadRequest(mensagem);

            var relatorio = _registroService.GetRelatorio(mes);

            return Ok(relatorio);
        }
    }
}
