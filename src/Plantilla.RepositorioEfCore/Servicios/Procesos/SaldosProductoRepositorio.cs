using Plantilla.Entidad.Contextos;
using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Procesos;
using Microsoft.EntityFrameworkCore;

namespace Plantilla.RepositorioEfCore.Servicios.Procesos
{
    public class SaldosProductoRepositorio(PlantillaDbContext context) : ISaldosProductoRepositorio
    {

        public async Task<IEnumerable<SaldosProducto>> ConsultarTodos(long? idProducto)
        {
            return await context.SaldosProducto
                .Where(e => e.Activo)
                .Where(e => !idProducto.HasValue || e.IdProducto == idProducto)
                .ToListAsync();
        }

        public async Task<SaldosProducto?> ConsultarPorProducto(long idProducto)
        {
            return await context.SaldosProducto.FirstOrDefaultAsync(e => e.IdProducto == idProducto);
        }

        public async Task Crear(SaldosProducto taskItem)
        {
            await context.SaldosProducto.AddAsync(taskItem);
            await context.SaveChangesAsync();
        }

        public async Task Actualizar(SaldosProducto taskItem)
        {
            context.SaldosProducto.Update(taskItem);
            await context.SaveChangesAsync();
        }
    }
}
