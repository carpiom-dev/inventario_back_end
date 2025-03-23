using Plantilla.Entidad.Modelo.Procesos;
using Plantilla.Infraestructura.Modelo.Respuestas;

namespace Plantilla.Entidad.Interfaz.Procesos
{
    public interface IFacturasRepositorio
    {
        Task<RespuestaGenericaDto> Crear(Facturas taskItem, List<FacturaDetalles> detalles);
        Task<RespuestaGenericaDto> Eliminar(long idFactura);
        Task<(Facturas? factura, IEnumerable<FacturaDetalles>? detalles)> ConsultarPorId(long id);
        Task<IEnumerable<Facturas>> ConsultarTodos();
    }
}
