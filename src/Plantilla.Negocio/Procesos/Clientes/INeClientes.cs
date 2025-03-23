using Plantilla.Dto.Modelo.Procesos.Clientes;
using Plantilla.Infraestructura.Modelo.Respuestas;
using static Plantilla.Dto.Modelo.Procesos.Clientes.ClientesDto;

namespace Plantilla.Negocio.Procesos.Clientes
{
    public interface INeClientes
    {
        Task<RespuestaGenericaConsultaDto<ClientesDto>> ConsultarPorId(ConsultarCliente consultar);

        Task<RespuestaGenericaConsultasDto<ClientesDto>> ConsultarTodos(ConsultarClientes consultar);

        Task<RespuestaGenericaDto> Crear(CrearCliente crear);

        Task<RespuestaGenericaDto> Actualizar(ActualizarCliente actualizar);

        Task<RespuestaGenericaDto> Eliminar(EliminarCliente eliminar);
    }
}
