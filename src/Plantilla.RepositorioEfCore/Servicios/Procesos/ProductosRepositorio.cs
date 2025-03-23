using Plantilla.Entidad.Contextos;
using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Procesos;
using Microsoft.EntityFrameworkCore;

namespace Plantilla.RepositorioEfCore.Servicios.Procesos
{
    public class ProductosRepositorio(PlantillaDbContext context) : IProductosRepositorio
    {
        public async Task<Productos?> ConsultarPorId(long id)
        {
            return await context
                .Productos
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Productos>> ConsultarTodos()
        {
            return await context
                .Productos
                .Where(e => e.Activo)
                .ToListAsync();
        }

        public async Task<long> Crear(Productos taskItem)
        {
            await context.Productos.AddAsync(taskItem);
            await context.SaveChangesAsync();

            return taskItem.Id;
        }

        public async Task Actualizar(Productos taskItem)
        {
            context.Update(taskItem);
            await context.SaveChangesAsync();
        }
    }
}
