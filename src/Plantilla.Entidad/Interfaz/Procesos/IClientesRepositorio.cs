using Plantilla.Entidad.Modelo.Procesos;

namespace Plantilla.Entidad.Interfaz.Procesos
{
    public interface IClientesRepositorio
    {
        Task<long> Crear(Clientes taskItem);
        Task Actualizar(Clientes taskItem);
        Task<Clientes?> ConsultarPorId(long id);
        Task<IEnumerable<Clientes>> ConsultarTodos(string? numeroIdentificacion, string? nombreContiene);
    }
}
