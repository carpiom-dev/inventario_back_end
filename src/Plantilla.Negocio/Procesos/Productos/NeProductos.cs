using Plantilla.Dto.Modelo.Identity.Rol;
using Plantilla.Dto.Modelo.Procesos.Productos;
using Plantilla.Entidad.Interfaz;
using Plantilla.Infraestructura.Modelo.Respuestas;
using Plantilla.Infraestructura.Services.Sesion;
using Plantilla.Infraestructura.Utilidades.Generico;
using Plantilla.Infraestructura.Utilidades.Logger;
using Plantilla.Infraestructura.Utilidades.Mapeador;

namespace Plantilla.Negocio.Procesos.Productos
{
    internal class NeProductos(IUnidadTrabajo unidadTrabajo) : INeProductos
    {
        public async Task<RespuestaGenericaDto> Actualizar(ProductosDto.ActualizarProducto actualizar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(actualizar);
                if (!esValido) return respuesta;

                // Procesamos
                var entidad = await unidadTrabajo
                    .Productos
                    .ConsultarPorId(actualizar.Id!.Value);

                if (entidad is null) return RespuestaGenericaDto.ErrorComun("No se ha encontrado el entidad a actualizar");

                entidad.NombreProducto = actualizar.NombreProducto;
                entidad.DescripcionProducto = actualizar.DescripcionProducto;

                await unidadTrabajo.Productos.Actualizar(entidad);

                return RespuestaGenericaDto.ExitoComun();
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return RespuestaGenericaDto.Excepcion();
            }
        }

        public async Task<RespuestaGenericaConsultaDto<ProductosDto>> ConsultarPorId(ProductosDto.ConsultarProducto consultar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(consultar);
                if (!esValido) return new(respuesta);

                // Procesamos
                var entidad = await unidadTrabajo
                    .Productos
                    .ConsultarPorId(consultar.Id!.Value);

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultado = entidad?.Mapear<ProductosDto>(),
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaConsultasDto<ProductosDto>> ConsultarTodos()
        {
            try
            {
                // Procesamos
                var entidades = await unidadTrabajo
                    .Productos
                    .ConsultarTodos();

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultados = entidades?.Mapear<List<ProductosDto>>(),
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaDto> Crear(ProductosDto.CrearProducto crear)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(crear);
                if (!esValido) return respuesta;

                await unidadTrabajo
                    .Productos
                    .Crear(new()
                    {
                        Id = 0,
                        DescripcionProducto =  crear.DescripcionProducto,
                        NombreProducto = crear.NombreProducto,
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

        public async Task<RespuestaGenericaDto> Eliminar(ProductosDto.EliminarProducto eliminar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(eliminar);
                if (!esValido) return respuesta;

                // Procesamos
                var entidad = await unidadTrabajo
                    .Productos
                    .ConsultarPorId(eliminar.Id!.Value);

                if (entidad is null) return RespuestaGenericaDto.ErrorComun("No se ha encontrado el entidad a eliminar");

                entidad.Activo = false;

                await unidadTrabajo.Productos.Actualizar(entidad);

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
