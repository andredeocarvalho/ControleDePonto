using FluentAssertions;
using Ilia.ControleDePonto.Application.Services;
using Ilia.ControleDePonto.Domain;
using Ilia.ControleDePonto.Repository.Repositories;

namespace Ilia.ControleDePonto.Tests.Unit.Application.Services
{
    public class RegistroServiceUnitTest
    {
        private readonly Mock<IControleDePontoRepository> _controleDePontoRepositoryMock;
        private readonly IRegistroService _registroService;

        public RegistroServiceUnitTest()
        {
            _controleDePontoRepositoryMock = new Mock<IControleDePontoRepository>();

            SetupMocks();

            _registroService = new RegistroService(_controleDePontoRepositoryMock.Object);
        }

        [Fact]
        public void DeveGerarRelatorio()
        {
            var relatorio = _registroService.GetRelatorio("2018-08");

            relatorio.Should().NotBeNull();
            relatorio.Mes.Should().Be("2018-08");
            relatorio.HorasTrabalhadas.Should().NotBeNullOrEmpty();
            relatorio.HorasExcedentes.Should().NotBeNullOrEmpty();
            relatorio.HorasDevidas.Should().NotBeNullOrEmpty();
            relatorio.Registros.Should().NotBeNullOrEmpty();
            relatorio.Registros.First().Should().NotBeNull();
            relatorio.Registros.First().Dia.Should().NotBeNullOrWhiteSpace();
            relatorio.Registros.First().Horarios.Should().NotBeNullOrEmpty();
            relatorio.Registros.First().Horarios.First().Should().NotBeNullOrWhiteSpace();

            _controleDePontoRepositoryMock.Verify(x => x.GetRegistrosPorMes(It.IsAny<string>()));
        }

        [Fact]
        public void DeveBuscarRegistro()
        {
            var registro = _registroService.GetRegistro(new DateOnly(2018, 8, 22));

            registro.Should().NotBeNull();
            registro.Dia.Should().Be("2018-08-22");
            registro.Horarios.Should().NotBeNullOrEmpty();
            registro.Horarios.First().Should().NotBeNullOrWhiteSpace();

            _controleDePontoRepositoryMock.Verify(x => x.GetRegistro(It.IsAny<DateOnly>()));
        }

        [Fact]
        public void DeveInserirMomento()
        {
            var registro = _registroService.AddMomento(new(2018, 08, 22, 8, 0, 0));

            registro.Should().NotBeNull();
            registro.Dia.Should().Be("2018-08-22");
            registro.Horarios.Should().NotBeNullOrEmpty();
            registro.Horarios.First().Should().NotBeNullOrWhiteSpace();

            _controleDePontoRepositoryMock.Verify(x => x.GetRegistro(It.IsAny<DateOnly>()));
        }

        private void SetupMocks()
        {
            _controleDePontoRepositoryMock.Setup(x => x.GetRegistrosPorMes(It.IsAny<string>())).Returns(CreateListOfRegistros());
            _controleDePontoRepositoryMock.Setup(x => x.GetRegistro(It.IsAny<DateOnly>())).Returns(CreateRegistro());
            _controleDePontoRepositoryMock.Setup(x => x.AddMomento(It.IsAny<MomentoData>()));
        }

        private static List<Registro> CreateListOfRegistros() =>
            new()
            {
                CreateRegistro()
            };

        private static Registro CreateRegistro() =>
            new()
            {
                Dia = "2018-08-22",
                Horarios = new() { "08:00:00", "12:00:00", "13:00:00", "17:00:00" }
            };
    }
}