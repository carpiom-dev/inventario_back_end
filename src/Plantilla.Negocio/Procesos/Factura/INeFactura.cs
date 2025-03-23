using Plantilla.Dto.Modelo.Procesos.Facturas;
using Plantilla.Infraestructura.Modelo.Respuestas;
using static Plantilla.Dto.Modelo.Procesos.Facturas.FacturaDto;

namespace Plantilla.Negocio.Procesos.Factura
{
    public interface INeFactura
    {
        Task<RespuestaGenericaConsultaDto<FacturaDto.Detallado>> ConsultarPorId(ConsultarFactura consultar);

        Task<RespuestaGenericaConsultasDto<FacturaDto>> ConsultarTodos();

        Task<RespuestaGenericaDto> Crear(CrearFactura crear);

        Task<RespuestaGenericaDto> Eliminar(EliminarFactura eliminar);
    }
}
