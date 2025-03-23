using Plantilla.Dto.Modelo.Identity.Rol;
using Plantilla.Dto.Modelo.Procesos.Clientes;
using Plantilla.Entidad.Interfaz;
using Plantilla.Infraestructura.Modelo.Respuestas;
using Plantilla.Infraestructura.Services.Sesion;
using Plantilla.Infraestructura.Utilidades.Generico;
using Plantilla.Infraestructura.Utilidades.Logger;
using Plantilla.Infraestructura.Utilidades.Mapeador;

namespace Plantilla.Negocio.Procesos.Clientes
{
    internal class NeClientes(IUnidadTrabajo unidadTrabajo) : INeClientes
    {
        public async Task<RespuestaGenericaDto> Actualizar(ClientesDto.ActualizarCliente actualizar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(actualizar);
                if (!esValido) return respuesta;

                // Procesamos
                var entidad = await unidadTrabajo
                    .Clientes
                    .ConsultarPorId(actualizar.Id!.Value);

                if (entidad is null) return RespuestaGenericaDto.ErrorComun("No se ha encontrado el entidad a actualizar");

                entidad.NumeroIdentificacion = actualizar.NumeroIdentificacion!;
                entidad.Descripcion = actualizar.Descripcion!;
                entidad.RazonSocial = actualizar.RazonSocial!;
                entidad.TipoImpuesto = actualizar.TipoImpuesto!.Value;

                await unidadTrabajo.Clientes.Actualizar(entidad);

                return RespuestaGenericaDto.ExitoComun();
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaDto.Excepcion();
            }
        }

        public async Task<RespuestaGenericaConsultaDto<ClientesDto>> ConsultarPorId(ClientesDto.ConsultarCliente consultar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(consultar);
                if (!esValido) return new(respuesta);

                // Procesamos
                var entidad = await unidadTrabajo
                    .Clientes
                    .ConsultarPorId(consultar.Id!.Value);

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultado = entidad?.Mapear<ClientesDto>(),
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaConsultasDto<ClientesDto>> ConsultarTodos(ClientesDto.ConsultarClientes consultar)
        {
            try
            {
                // Procesamos
                var entidades = await unidadTrabajo
                    .Clientes
                    .ConsultarTodos(consultar.NumeroIdentificacion, consultar.NombreContiene);

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultados = entidades?.Mapear<List<ClientesDto>>(),
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaDto> Crear(ClientesDto.CrearCliente crear)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(crear);
                if (!esValido) return respuesta;

                await unidadTrabajo
                    .Clientes
                    .Crear(new()
                    {
                        NumeroIdentificacion = crear.NumeroIdentificacion!,
                        RazonSocial = crear.RazonSocial!,
                        TipoImpuesto = crear.TipoImpuesto!.Value,
                        Descripcion = crear.Descripcion ?? string.Empty,
                        FechaCreacion = DateTime.Now,
                        Activo = true
                    });

                return RespuestaGenericaDto.ExitoComun();
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaDto.Excepcion();
            }
        }

        public async Task<RespuestaGenericaDto> Eliminar(ClientesDto.EliminarCliente eliminar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(eliminar);
                if (!esValido) return respuesta;

                // Procesamos
                var entidad = await unidadTrabajo
                    .Clientes
                    .ConsultarPorId(eliminar.Id!.Value);

                if (entidad is null) return RespuestaGenericaDto.ErrorComun("No se ha encontrado el entidad a eliminar");

                entidad.Activo = false;

                await unidadTrabajo.Clientes.Actualizar(entidad);

                return RespuestaGenericaDto.ExitoComun();
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaDto.Excepcion();
            }
        }
    }
}
