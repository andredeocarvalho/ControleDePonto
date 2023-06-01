using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Application.Services
{
    public interface IRegistroService
    {
        Relatorio? GetRelatorio(string mes);
        Registro? GetRegistro(DateOnly data);
        Registro AddMomento(DateTime dataHora);
    }
}
