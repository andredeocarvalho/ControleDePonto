using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Application.Validations
{
    public interface IMesValidation
    {
        (bool, MensagemRetorno) ValidateMes(string mes);
    }
}