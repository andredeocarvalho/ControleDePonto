
using Ilia.ControleDePonto.Domain;
using Ilia.ControleDePonto.Repository;
using Ilia.ControleDePonto.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Ilia.ControleDePonto.Tests.Unit.Repository.Repositories
{
    public class ControleDePontoRepositoryUnitTest
    {
        private readonly Mock<ControleDePontoDbContext> _controleDePontoDbContextMock;
        private readonly IControleDePontoRepository _controleDePontoRepository;

        public ControleDePontoRepositoryUnitTest()
        {
            _controleDePontoDbContextMock = new Mock<ControleDePontoDbContext>();

            SetupMocks();

            _controleDePontoRepository = new ControleDePontoRepository();
        }

        private void SetupMocks()
        {
            _controleDePontoDbContextMock.Setup(x => x.Momentos).ReturnsDbSet(CreateMomentosList());
        }

        private static List<MomentoData> CreateMomentosList() =>
            new()
            {
                new()
                {
                    Data=new(2018,08,22),
                    Hora=new(08,00,00)
                },
                new()
                {
                    Data=new(2018,08,22),
                    Hora=new(12,00,00)
                },
                new()
                {
                    Data=new(2018,08,22),
                    Hora=new(13,00,00)
                },
                new()
                {
                    Data=new(2018,08,22),
                    Hora=new(17,00,00)
                }
            };
    }
}
