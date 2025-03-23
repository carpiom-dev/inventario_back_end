using Plantilla.Negocio.Identity;
using Microsoft.Extensions.DependencyInjection;
using Plantilla.Negocio.Procesos;

namespace Plantilla.Negocio
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigurarNegocio(this IServiceCollection services)
        {
            services.AddNeIdentity();
            services.AddNeProcesos();

            return services;
        }
    }
}