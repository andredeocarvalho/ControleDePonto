using Ilia.ControleDePonto.Application.Services;
using Ilia.ControleDePonto.Application.Validations;
using Ilia.ControleDePonto.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ilia.ControleDePonto.API.Controllers
{
    [ApiController]
    [Route("v1/batidas")]
    public class BatidasController : ControllerBase
    {
        private readonly IRegistroService _registroService;
        private readonly IMomentoValidation _momentoValidation;

        public BatidasController(IRegistroService registroService,
                                 IMomentoValidation momentoValidation)
        {
            _registroService = registroService;
            _momentoValidation = momentoValidation;
        }

        [HttpPost]
        public IActionResult InsereBatida(Momento momento)
        {
            (var isBadRequest, var mensagem) = _momentoValidation.ValidateBadRequest(momento.DataHora, out var dataHora);

            if (isBadRequest)
                return BadRequest(mensagem);

            var dateOnly = new DateOnly(dataHora.Year, dataHora.Month, dataHora.Day);
            var registro = _registroService.GetRegistro(dateOnly);

            if (registro != null)
            {
                (var isConflict, mensagem) = _momentoValidation.ValidateConflict(registro, dataHora);
                if (isConflict)
                    return Conflict(mensagem);

                (var isForbidden, mensagem) = _momentoValidation.ValidateForbidden(registro, dataHora);
                if (isForbidden)
                    return StatusCode((int)HttpStatusCode.Forbidden, mensagem);
            }

            registro = _registroService.AddMomento(dataHora);

            return Created("", registro);
        }
    }
}
