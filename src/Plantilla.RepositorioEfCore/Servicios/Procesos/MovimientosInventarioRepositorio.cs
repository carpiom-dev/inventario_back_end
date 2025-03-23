using Plantilla.Entidad.Contextos;
using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Procesos;
using Microsoft.EntityFrameworkCore;

namespace Plantilla.RepositorioEfCore.Servicios.Procesos
{
    public class MovimientosInventarioRepositorio(PlantillaDbContext context) : IMovimientosInventarioRepositorio
    {       
        public async Task<long> Crear(MovimientosInventario taskItem)
        {
            await context.MovimientosInventario.AddAsync(taskItem);
            await context.SaveChangesAsync();

            return taskItem.Id;
        }

        public async Task Eliminar(long id)
        {
            var movimiento = await context
                .MovimientosInventario
                .FirstOrDefaultAsync (e => e.Id == id)
                    ?? throw new InvalidOperationException("No existe el registro a eliminar");

            movimiento.Activo = false;
            movimiento.FechaModificacion = DateTime.Now;


            context.MovimientosInventario.Update(movimiento);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovimientosInventario>> ConsultarTodos(long? idProducto, int? fechaInicio, int? fechaFin)
        {
            return await context.MovimientosInventario
                .Where(e => !idProducto.HasValue || e.IdProducto == idProducto)
                .Where(e => !fechaInicio.HasValue || e.FechaMovimiento >= fechaInicio)
                .Where(e => !fechaFin.HasValue || e.FechaMovimiento <= fechaFin)
                .Include(e => e.Producto)
                .AsSplitQuery()
                .ToListAsync();
        }
    }
}
