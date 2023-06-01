using Ilia.ControleDePonto.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ilia.ControleDePonto.IoC.InjecoesDeDependencia
{
    public static class ServicesDependencyInjection
    {
        public static void AddServicos(this IServiceCollection services)
        {
            services.AddSingleton<IRegistroService, RegistroService>();
        }
    }
}
