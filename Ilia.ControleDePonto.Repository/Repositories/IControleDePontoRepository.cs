using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Repository.Repositories
{
    public interface IControleDePontoRepository
    {
        void AddMomento(MomentoData momentoTable);
        Registro? GetRegistro(DateOnly data);
        List<Registro> GetRegistrosPorMes(string mes);
    }
}
