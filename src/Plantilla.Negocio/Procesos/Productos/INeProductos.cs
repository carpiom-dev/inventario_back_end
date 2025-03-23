using Plantilla.Dto.Modelo.Procesos.Productos;
using Plantilla.Infraestructura.Modelo.Respuestas;
using static Plantilla.Dto.Modelo.Procesos.Productos.ProductosDto;

namespace Plantilla.Negocio.Procesos.Productos
{
    public interface INeProductos
    {
        Task<RespuestaGenericaConsultaDto<ProductosDto>> ConsultarPorId(ConsultarProducto consultar);

        Task<RespuestaGenericaConsultasDto<ProductosDto>> ConsultarTodos();

        Task<RespuestaGenericaDto> Crear(CrearProducto crear);

        Task<RespuestaGenericaDto> Actualizar(ActualizarProducto actualizar);

        Task<RespuestaGenericaDto> Eliminar(EliminarProducto eliminar);
    }
}
