using Plantilla.Entidad.Modelo.Procesos;

namespace Plantilla.Entidad.Interfaz.Procesos
{
    public interface IProductosRepositorio
    {
        Task<long> Crear(Productos taskItem);
        Task Actualizar(Productos taskItem);
        Task<Productos?> ConsultarPorId(long id);
        Task<IEnumerable<Productos>> ConsultarTodos();
    }
}
