using Ilia.ControleDePonto.Application.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Ilia.ControleDePonto.IoC.InjecoesDeDependencia
{
    public static class ValidationsDependencyInjection
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddSingleton<IMomentoValidation, MomentoValidation>();
            services.AddSingleton<IMesValidation, MesValidation>();
        }
    }
}
