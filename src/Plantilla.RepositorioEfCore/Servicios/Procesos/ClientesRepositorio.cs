using Plantilla.Entidad.Contextos;
using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Procesos;
using Microsoft.EntityFrameworkCore;

namespace Plantilla.RepositorioEfCore.Servicios.Procesos
{
    public class ClientesRepositorio(PlantillaDbContext context) : IClientesRepositorio
    {
        public async Task<Clientes?> ConsultarPorId(long id)
        {
            return await context
                .Clientes
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Clientes>> ConsultarTodos(string? numeroIdentificacion, string? razonSocialContiene)
        {
            return await context
                .Clientes
                .Where(e => e.Activo)
                .Where(e => string.IsNullOrEmpty(numeroIdentificacion) || e.NumeroIdentificacion == numeroIdentificacion)
                .Where(e => string.IsNullOrEmpty(razonSocialContiene) || e.RazonSocial.Contains(razonSocialContiene))
                .ToListAsync();
        }

        public async Task<long> Crear(Clientes taskItem)
        {
            await context.Clientes.AddAsync(taskItem);
            await context.SaveChangesAsync();

            return taskItem.Id;
        }

        public async Task Actualizar(Clientes taskItem)
        {
            context.Update(taskItem);
            await context.SaveChangesAsync();
        }
    }
}
