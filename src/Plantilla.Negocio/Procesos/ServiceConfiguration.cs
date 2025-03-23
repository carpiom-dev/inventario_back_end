using Microsoft.Extensions.DependencyInjection;
using Plantilla.Negocio.Procesos.Clientes;
using Plantilla.Negocio.Procesos.Factura;
using Plantilla.Negocio.Procesos.Movimientos;
using Plantilla.Negocio.Procesos.Productos;
using Plantilla.Negocio.Procesos.TaskItem;

namespace Plantilla.Negocio.Procesos
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddNeProcesos(this IServiceCollection services)
        {
            services.AddScoped<INeTaskItem, NeTaskItem>();
            services.AddScoped<INeProductos, NeProductos>();
            services.AddScoped<INeClientes, NeClientes>();
            services.AddScoped<INeFactura, NeFactura>();
            services.AddScoped<INeMovimientos, NeMovimientos>();

            return services;
        }
    }
}