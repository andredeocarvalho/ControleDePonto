using Ilia.ControleDePonto.Repository;
using Ilia.ControleDePonto.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ilia.ControleDePonto.IoC.InjecoesDeDependencia
{
    public static class RepositoriesDependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IControleDePontoRepository, ControleDePontoRepository>();
            services.AddSingleton<IControleDePontoDbContext, ControleDePontoDbContext>();
        }
    }
}
