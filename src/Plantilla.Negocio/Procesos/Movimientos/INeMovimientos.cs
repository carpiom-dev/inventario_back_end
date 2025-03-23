using Plantilla.Dto.Modelo.Procesos.Movimientos;
using Plantilla.Infraestructura.Modelo.Excel;
using Plantilla.Infraestructura.Modelo.Respuestas;

namespace Plantilla.Negocio.Procesos.Movimientos
{
    public interface INeMovimientos
    {
        Task<RespuestaGenericaDto> CargaInicialInventario(MovimientoDto.CrearMovimiento crear);
        Task<RespuestaGenericaConsultaDto<ExcelDto>> GenerarKardex(KardexDto.ConsultarKardex consultar);
        Task<RespuestaGenericaConsultaDto<ExcelDto>> GenerarKardexValorizado(KardexValorizadoDto.ConsultarKardexValorizado consultar);
    }
}
