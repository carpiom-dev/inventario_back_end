using Plantilla.Entidad.Modelo.Procesos;

namespace Plantilla.Entidad.Interfaz.Procesos
{
    public interface IMovimientosInventarioRepositorio
    {
        Task<long> Crear(MovimientosInventario taskItem);
        Task Eliminar(long id); 
        Task<IEnumerable<MovimientosInventario>> ConsultarTodos(
            long? idProducto, int? fechaInicio, int? fechaFin);
    }
}
