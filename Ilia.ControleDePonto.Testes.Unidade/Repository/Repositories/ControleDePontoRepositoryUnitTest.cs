
using FluentAssertions;
using Ilia.ControleDePonto.Domain;
using Ilia.ControleDePonto.Repository;
using Ilia.ControleDePonto.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Ilia.ControleDePonto.Tests.Unit.Repository.Repositories
{
    public class ControleDePontoRepositoryUnitTest
    {
        private readonly Mock<IControleDePontoDbContext> _controleDePontoDbContextMock;
        private readonly IControleDePontoRepository _controleDePontoRepository;

        public ControleDePontoRepositoryUnitTest()
        {
            _controleDePontoDbContextMock = new Mock<IControleDePontoDbContext>();

            SetupMocks();

            _controleDePontoRepository = new ControleDePontoRepository(_controleDePontoDbContextMock.Object);
        }

        [Fact]
        public void DeveInserirMomento()
        {
            MomentoData momento = new()
            {
                Data = new(2018, 8, 22),
                Hora = new(8, 0, 0)
            };

            _controleDePontoRepository.AddMomento(momento);

            _controleDePontoDbContextMock.Verify(x => x.Momentos.Add(It.IsAny<MomentoData>()));
            _controleDePontoDbContextMock.Verify(x => x.SaveChanges());
        }

        [Fact]
        public void DeveBuscarRegistro()
        {
            DateOnly data = new(2018, 8, 22);

            var registro = _controleDePontoRepository.GetRegistro(data);

            registro.Should().NotBeNull();
            registro.Dia.Should().Be("2018-08-22");
            registro.Horarios.Should().NotBeNullOrEmpty();
            registro.Horarios.First().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void DeveBuscarRegistrosPorMes()
        {
            var mes = "2018-08";

            var registros = _controleDePontoRepository.GetRegistrosPorMes(mes);

            registros.Should().NotBeNull();
            registros.First().Should().NotBeNull();
            registros.First().Dia.Should().Be("2018-08-22");
            registros.First().Horarios.Should().NotBeNullOrEmpty();
            registros.First().Horarios.First().Should().NotBeNullOrWhiteSpace();
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
