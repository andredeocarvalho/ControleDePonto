using FluentAssertions;
using Ilia.ControleDePonto.Application.Services;
using Ilia.ControleDePonto.Application.Validations;
using Ilia.ControleDePonto.Domain;
using Ilia.ControleDePonto.Repository.Repositories;

namespace Ilia.ControleDePonto.Tests.Unit.Application.Validations
{
    public class MomentoValidationUnitTest
    {
        private readonly IMomentoValidation _momentoValidation;

        public MomentoValidationUnitTest()
        {
            _momentoValidation = new MomentoValidation();
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("2018", true)]
        [InlineData("2018-08-22T08:00:00", false)]
        public void DeveValidarBadRequest(string momento, bool resultado)
        {
            (var isBadRequest, _) = _momentoValidation.ValidateBadRequest(momento, out _);

            isBadRequest.Should().Be(resultado);
        }

        [Fact]
        public void DeveValidarForbidden()
        {
            var registro = CreateRegistro();
            DateTime dataHora = new(2018, 08, 22, 13, 00, 00);

            (var isBadRequest, _) = _momentoValidation.ValidateForbidden(registro, dataHora);

            isBadRequest.Should().BeFalse();
        }

        [Fact]
        public void DeveValidarForbidden_MaisDeQuatroMomentos()
        {
            var registro = CreateRegistro();
            registro.Horarios.AddRange(new[] { "13:00:00", "17:00:00" });
            DateTime dataHora = new(2018, 08, 22, 18, 00, 00);

            (var isBadRequest, _) = _momentoValidation.ValidateForbidden(registro, dataHora);

            isBadRequest.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarForbidden_FimDeSemana()
        {
            var registro = CreateRegistro(25);
            DateTime dataHora = new(2018, 08, 25, 13, 00, 00);

            (var isBadRequest, _) = _momentoValidation.ValidateForbidden(registro, dataHora);

            isBadRequest.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarForbidden_UmaHoraDeAlmoco()
        {
            var registro = CreateRegistro();
            DateTime dataHora = new(2018, 08, 22, 12, 30, 00);

            (var isBadRequest, _) = _momentoValidation.ValidateForbidden(registro, dataHora);

            isBadRequest.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarConflict()
        {
            var registro = CreateRegistro();
            DateTime dataHora = new(2018, 08, 22, 13, 00, 00);

            (var isBadRequest, _) = _momentoValidation.ValidateConflict(registro, dataHora);

            isBadRequest.Should().BeFalse();
        }

        [Fact]
        public void DeveValidarConflict_HorarioDuplicado()
        {
            var registro = CreateRegistro();
            DateTime dataHora = new(2018, 08, 22, 12, 00, 00);

            (var isBadRequest, _) = _momentoValidation.ValidateConflict(registro, dataHora);

            isBadRequest.Should().BeTrue();
        }

        private static Registro CreateRegistro(int dia = 22) =>
            new()
            {
                Dia = $"2018-08-{dia}",
                Horarios = new() { "08:00:00", "12:00:00" }
            };
    }
}