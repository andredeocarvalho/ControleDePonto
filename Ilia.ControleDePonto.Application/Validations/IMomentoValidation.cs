using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Application.Validations
{
    public interface IMomentoValidation
    {
        (bool, MensagemRetorno) ValidateBadRequest(string momento, out DateTime dataHora);
        (bool, MensagemRetorno) ValidateForbidden(Registro registro, DateTime dataHora);
        (bool, MensagemRetorno) ValidateConflict(Registro registro, DateTime dataHora);
    }
}
