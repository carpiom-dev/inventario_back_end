using Plantilla.Entidad.Modelo.Procesos;

namespace Plantilla.Entidad.Interfaz.Procesos
{
    public interface ISaldosProductoRepositorio
    {
        Task Crear(SaldosProducto taskItem);
        Task Actualizar(SaldosProducto taskItem);
        Task<IEnumerable<SaldosProducto>> ConsultarTodos(long? idProducto);
        Task<SaldosProducto?> ConsultarPorProducto(long idProducto);
    }
}
