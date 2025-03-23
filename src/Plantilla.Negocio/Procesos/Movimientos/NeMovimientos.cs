using Plantilla.Dto.Modelo.Procesos.Movimientos;
using Plantilla.Entidad.Interfaz;
using Plantilla.Infraestructura.Modelo.Respuestas;
using Plantilla.Infraestructura.Utilidades.Generico;
using Plantilla.Infraestructura.Utilidades.Logger;
using System.Globalization;
using Plantilla.Infraestructura.Modelo.Excel;
using Plantilla.Infraestructura.Utilidades.Excel;
using Plantilla.Infraestructura.Modelo.Enums;

namespace Plantilla.Negocio.Procesos.Movimientos
{
    public class NeMovimientos(IUnidadTrabajo unidadTrabajo) : INeMovimientos
    {
        public async Task<RespuestaGenericaDto> CargaInicialInventario(MovimientoDto.CrearMovimiento crear)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(crear);
                if (!esValido) return respuesta;

                // Procesamos
                var fecha = DateTime.Today;
                var saldo = await unidadTrabajo
                    .SaldosProducto
                    .ConsultarPorProducto(crear.IdProducto!.Value);
                var total = (crear.Cantidad ?? 0) * (crear.PrecioUnitario ?? 0);
                unidadTrabajo.Begin();

                var entidad = await unidadTrabajo
                    .MovimientosInventario
                    .Crear(new()
                    {
                        IdProducto = crear.IdProducto!.Value,
                        TipoMovimiento = EnumTipoMovimiento.Ingreso,
                        Cantidad = crear.Cantidad ?? 0,
                        PrecioUnitario = crear.PrecioUnitario ?? 0m,
                        Total = total,
                        FechaMovimiento = GetIntDate(fecha),
                        Activo = true,
                        FechaCreacion = DateTime.Today,
                    });

                // Actualizamos el saldo del producto
                if (saldo is null)
                {
                    await unidadTrabajo.SaldosProducto.Crear(new()
                    {
                        IdProducto = crear.IdProducto!.Value,
                        Cantidad = crear.Cantidad!.Value,
                        PrecioPromedio = crear.PrecioUnitario!.Value,
                        Total = total,
                        Activo = true,
                    });
                }
                else
                {
                    saldo.Cantidad += crear.Cantidad!.Value;
                    saldo.Total += total;
                    saldo.PrecioPromedio += saldo.Cantidad != 0
                        ? saldo.Total / saldo.Cantidad : 0m;

                    await unidadTrabajo.SaldosProducto.Actualizar(saldo);
                }


                unidadTrabajo.Commit();

                return RespuestaGenericaDto.ExitoComun();
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return RespuestaGenericaDto.Excepcion();
            }
        }

        public async Task<RespuestaGenericaConsultaDto<ExcelDto>> GenerarKardex(KardexDto.ConsultarKardex consultar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(consultar);
                if (!esValido) return new(respuesta);


                // Preparamos el kardex
                var datos = await unidadTrabajo
                    .MovimientosInventario
                    .ConsultarTodos(consultar.IdProducto, GetIntDate(consultar.FechaInicio), GetIntDate(consultar.FechaFin));

                decimal saldo = 0m;
                List<KardexDto> excelData = [];
                foreach (var e in datos.OrderBy(e => e.FechaMovimiento))
                {
                    saldo += e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Cantidad : -e.Cantidad;

                    excelData.Add(new()
                    {
                        TipoMovimiento = e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? "I" : "E",
                        NombreProducto = e.Producto?.NombreProducto ?? string.Empty,
                        FechaMovimiento = GetFromIntDate(e.FechaMovimiento),
                        CantidadIngreso = e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Cantidad.ToString("N2") : string.Empty,
                        CantidadEgreso = e.TipoMovimiento == EnumTipoMovimiento.Egreso ? e.Cantidad.ToString("N2") : string.Empty,
                        CantidadSaldo = saldo.ToString("N2"),
                    });
                }

                var (excel, error) = ExcelHelper.ExportToExcel(excelData);
                if (!string.IsNullOrEmpty(error))
                {
                    return new(RespuestaGenericaDto.ErrorComun(error));
                }

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultado = excel!,
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        public async Task<RespuestaGenericaConsultaDto<ExcelDto>> GenerarKardexValorizado(KardexValorizadoDto.ConsultarKardexValorizado consultar)
        {
            try
            {
                // Validamos la entrada al servicio
                var (respuesta, esValido) = ValidarDataAnnotation.Validar(consultar);
                if (!esValido) return new(respuesta);


                // Preparamos el kardex
                var datos = await unidadTrabajo
                    .MovimientosInventario
                    .ConsultarTodos(consultar.IdProducto, GetIntDate(consultar.FechaInicio), GetIntDate(consultar.FechaFin));

                decimal saldo = 0m, dolares = 0m;
                List<KardexValorizadoDto> excelData = [];
                foreach (var e in datos.OrderBy(e => e.FechaMovimiento))
                {
                    saldo += e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Cantidad : -e.Cantidad;
                    dolares += e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Total : -e.Total;

                    excelData.Add(new()
                    {
                        TipoMovimiento = e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? "I" : "E",
                        NombreProducto = e.Producto?.NombreProducto ?? string.Empty,
                        FechaMovimiento = GetFromIntDate(e.FechaMovimiento),
                        CantidadIngreso = e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Cantidad.ToString("N2") : string.Empty,
                        DolaresIngreso = e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Total.ToString("N2") : string.Empty,
                        CantidadEgreso = e.TipoMovimiento == EnumTipoMovimiento.Egreso ? e.Cantidad.ToString("N2") : string.Empty,
                        DolaresEgreso = e.TipoMovimiento == EnumTipoMovimiento.Ingreso ? e.Total.ToString("N2") : string.Empty,
                        CantidadSaldo = saldo.ToString("N2"),
                        DolaresSaldo = dolares.ToString("N2")
                    });
                }

                var (excel, error) = ExcelHelper.ExportToExcel(excelData);
                if (!string.IsNullOrEmpty(error))
                {
                    return new(RespuestaGenericaDto.ErrorComun(error));
                }

                return new()
                {
                    Respuesta = RespuestaGenericaDto.ExitoComun(),
                    Resultado = excel!,
                };
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return new(RespuestaGenericaDto.Excepcion());
            }
        }

        private static int GetIntDate(DateTime fecha)
        {
            return Convert.ToInt32($"{fecha.Year}{fecha.Month:00}{fecha.Day:00}");
        }
        private static int? GetIntDate(DateTime? fecha)
        {
            if (fecha is null) return null;

            return Convert.ToInt32($"{fecha.Value.Year}{fecha.Value.Month:00}{fecha.Value.Day:00}");
        }
        private static DateTime GetFromIntDate(int fechaInt)
        {
            string fechaStr = fechaInt.ToString(); // Convertir a string

            if (DateTime.TryParseExact(fechaStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
            {
                return fecha;
            }
            else
            {
                throw new InvalidCastException("Error al convertir numero en fecha");
            }
        }
    }
}
