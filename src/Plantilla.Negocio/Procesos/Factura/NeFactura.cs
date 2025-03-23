using Plantilla.Dto.Modelo.Identity.Rol;
using Plantilla.Dto.Modelo.Procesos.Facturas;
using Plantilla.Entidad.Interfaz;
using Plantilla.Entidad.Modelo.Procesos;
using Plantilla.Infraestructura.Modelo.Respuestas;
using Plantilla.Infraestructura.Services.Sesion;
using Plantilla.Infraestructura.Utilidades.Generico;
using Plantilla.Infraestructura.Utilidades.Logger;
using Plantilla.Infraestructura.Utilidades.Mapeador;

namespace Plantilla.Negocio.Procesos.Factura
{
    internal class NeFactura(IUnidadTrabajo unidadTrabajo) : INeFactura
    {
        public async Task<RespuestaGenericaConsultaDto<FacturaDto.Detallado>> ConsultarPorId(FacturaDto.ConsultarFactura consultar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(consultar);
                if (!esValido) return new(respuesta);

                // Procesamos
                var (factura, detalles) = await unidadTrabajo
                    .Facturas
                    .ConsultarPorId(consultar.Id!.Value);

                if (factura is null) return new(RespuestaGenericaDto.ErrorComun("No se ha encontrado la factura"));
                if (detalles is null) return new(RespuestaGenericaDto.ErrorComun("No se ha encontrado detalles de las factura"));

                var facturaDto = new FacturaDto.Detallado()
                {
                    Id = factura.Id,
                    FechaFactura = factura.FechaFactura,
                    IdCliente = factura.IdCliente,
                    NombreCliente = factura.Clientes.RazonSocial,
                    Descuento = factura.Descuento,
                    Glosa = factura.Glosa,
                    Impuesto = factura.Impuesto,
                    Subtotal = factura.Subtotal,
                    Total = factura.Total,
                    Estado = factura.Activo ? "Activa" : "Eliminada",
                    Detalles = [.. detalles.Select(e => new FacturaDetalleDto()
                    {
                        Cantidad = e.Cantidad,
                        Total = e.Total,
                        Subtotal = e.Subtotal,
                        Impuesto = e.Impuesto,
                        Descuento = e.Descuento,
                        IdProducto = e.IdProducto,
                        NombreProducto = e.Productos.NombreProducto,
                        Precio = e.Precio,
                    })]
                };

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultado = facturaDto,
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaConsultasDto<FacturaDto>> ConsultarTodos()
        {
            try
            {
                // Procesamos
                var entidades = await unidadTrabajo
                    .Facturas
                    .ConsultarTodos();

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultados = entidades?.Select(factura => new FacturaDto()
                    {
                        Id = factura.Id,
                        FechaFactura = factura.FechaFactura,
                        IdCliente = factura.IdCliente,
                        NombreCliente = factura.Clientes.RazonSocial,
                        Descuento = factura.Descuento,
                        Glosa = factura.Glosa,
                        Impuesto = factura.Impuesto,
                        Subtotal = factura.Subtotal,
                        Total = factura.Total,
                        Estado = factura.Activo ? "Activa" : "Eliminada",
                    }).ToList(),
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaDto> Crear(FacturaDto.CrearFactura crear)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(crear);
                if (!esValido) return respuesta;

                var factura = new Facturas()
                {
                    Activo = true,
                    Descuento = 0,
                    FechaCreacion = DateTime.Now,
                    FechaFactura = crear.FechaFactura!.Value,
                    Glosa = crear.Glosa ?? string.Empty,
                    IdCliente = crear.IdCliente!.Value,
                    Impuesto = 0,
                    Subtotal = 0,
                    Total = 0,
                };

                var detalles = crear.Detalles!.Select(e => new FacturaDetalles()
                {
                    IdProducto = e.IdProducto,
                    Cantidad = e.Cantidad,
                    IdFactura = 0,
                    IdMovimiento = 0,
                    Precio = 0,
                    Impuesto = 0,
                    Subtotal = 0,
                    Total = 0,
                    Activo = true,
                    Descuento = 0,
                    FechaCreacion = DateTime.Now,
                });

                return await unidadTrabajo
                    .Facturas
                    .Crear(factura, [.. detalles]);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaDto.Excepcion();
            }
        }

        public async Task<RespuestaGenericaDto> Eliminar(FacturaDto.EliminarFactura eliminar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(eliminar);
                if (!esValido) return respuesta;

                // Procesamos
                return await unidadTrabajo
                     .Facturas
                     .Eliminar(eliminar.Id!.Value);

            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return RespuestaGenericaDto.Excepcion();
            }
        }
    }
}
