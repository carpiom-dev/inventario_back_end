using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Identity;
using Plantilla.RepositorioEfCore.Servicios.Identity;
using Plantilla.RepositorioEfCore.Servicios.Procesos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Plantilla.RepositorioEfCore.Servicios
{
    internal static class ServicesConfiguration
    {
        internal static IServiceCollection ConfigurarServiciosRepositorio(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender<AuthUser>, EmailSender>();
            services.AddTransient<ITaskItemRepositorio, TaskItemRepositorio>();
            services.AddTransient<IProductosRepositorio, ProductosRepositorio>();
            services.AddTransient<IMovimientosInventarioRepositorio, MovimientosInventarioRepositorio>();
            services.AddTransient<ISaldosProductoRepositorio, SaldosProductoRepositorio>();
            services.AddTransient<IClientesRepositorio, ClientesRepositorio>();
            services.AddTransient<IFacturasRepositorio, FacturasRepositorio>();

            services.ConfigurarIdentity();

            return services;
        }
    }
}