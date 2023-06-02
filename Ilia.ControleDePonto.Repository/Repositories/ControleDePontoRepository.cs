using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Repository.Repositories
{
    public class ControleDePontoRepository : IControleDePontoRepository
    {
        private readonly IControleDePontoDbContext _dbContext;
        public ControleDePontoRepository(IControleDePontoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddMomento(MomentoData momento)
        {
            _dbContext.Momentos.Add(momento);
            _dbContext.SaveChanges();
        }

        public Registro? GetRegistro(DateOnly data)
        {
            var momentos = _dbContext.Momentos.Where(m => m.Data == data).ToList();
            if (momentos.Any())
                return new Registro(momentos);
            return null;
        }

        public List<Registro> GetRegistrosPorMes(string mes)
        {
            var data = DateOnly.Parse(mes);
            var momentos = _dbContext.Momentos.Where(m => m.Data.Year == data.Year && m.Data.Month == data.Month).ToList();
            return momentos.GroupBy(m => m.Data).Select(g => new Registro(g.ToList())).ToList();
        }
    }
}
