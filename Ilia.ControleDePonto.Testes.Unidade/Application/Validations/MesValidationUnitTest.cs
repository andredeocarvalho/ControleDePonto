using FluentAssertions;
using Ilia.ControleDePonto.Application.Services;
using Ilia.ControleDePonto.Application.Validations;
using Ilia.ControleDePonto.Domain;
using Ilia.ControleDePonto.Repository.Repositories;

namespace Ilia.ControleDePonto.Tests.Unit.Application.Validations
{
    public class MesValidationUnitTest
    {
        private readonly IMesValidation _mesValidation;

        public MesValidationUnitTest()
        {
            _mesValidation = new MesValidation();
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("2018", true)]
        [InlineData("2018-08", false)]
        public void DeveValidarMes(string mes, bool resultado)
        {
            (var isBadRequest, _) = _mesValidation.ValidateMes(mes);

            isBadRequest.Should().Be(resultado);
        }
    }
}