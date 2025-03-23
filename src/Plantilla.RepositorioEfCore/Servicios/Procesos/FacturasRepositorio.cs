using Plantilla.Entidad.Contextos;
using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Procesos;
using Microsoft.EntityFrameworkCore;
using Plantilla.Infraestructura.Modelo.Respuestas;
using Plantilla.Infraestructura.Modelo.Enums;

namespace Plantilla.RepositorioEfCore.Servicios.Procesos
{
    public class FacturasRepositorio(PlantillaDbContext context) : IFacturasRepositorio
    {
        public async Task<(Facturas? factura, IEnumerable<FacturaDetalles>? detalles)> ConsultarPorId(long id)
        {
            var factura = await context
                .Facturas
                .Where(e => e.Id == id)
                .Include(e => e.Clientes)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            var detallesFactura = await context
                .FacturaDetalles
                .Where(e => e.IdFactura == id && e.Activo)
                .Include(e => e.Productos)
                .AsSplitQuery()
                .ToListAsync();

            return (factura, detallesFactura);
        }

        public async Task<IEnumerable<Facturas>> ConsultarTodos()
        {
            return await context
                .Facturas
                .Include(e => e.Clientes)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<RespuestaGenericaDto> Crear(Facturas taskItem, List<FacturaDetalles> detalles)
        {
            // Validamos stock de los detalles
            var cliente = await context
                .Clientes
                .FirstOrDefaultAsync(e => e.Id == taskItem.IdCliente);
            if (cliente == null) return RespuestaGenericaDto.ErrorComun("No existe el cliente a facturar");

            List<SaldosProducto> saldos = [];
            for (int i = 0; i < detalles.Count; i++)
            {
                var saldo = await context
                    .SaldosProducto
                    .FirstOrDefaultAsync(e => e.IdProducto == detalles[i].IdProducto);

                if (saldo is null)
                    return RespuestaGenericaDto.ErrorComun($"No existe stock para el producto: {detalles[i].IdProducto}");

                if (saldo.Cantidad < detalles[i].Cantidad)
                    return RespuestaGenericaDto.ErrorComun($"No existe stock para el producto: {detalles[i].IdProducto}");

                // Calculamos los valores
                detalles[i].Precio = saldo.PrecioPromedio;
                detalles[i].Subtotal = (detalles[i].Cantidad * detalles[i].Precio) - detalles[i].Descuento;
                detalles[i].Impuesto = CalcularImpuesto(cliente.TipoImpuesto, detalles[i].Subtotal);
                detalles[i].Total = detalles[i].Subtotal - detalles[i].Impuesto;

                saldos.Add(saldo);
            }

            // Creamos la cabecera
            var transaction = await context.Database.BeginTransactionAsync();

            // Creamos la factura
            taskItem.Subtotal = detalles.Sum(s => s.Subtotal);
            taskItem.Descuento = detalles.Sum(s => s.Descuento);
            taskItem.Impuesto = detalles.Sum(s => s.Impuesto);
            taskItem.Total = detalles.Sum(s => s.Total);

            await context.Facturas.AddAsync(taskItem);
            await context.SaveChangesAsync();

            //foreach (var detalle in detalles)
            for (int i = 0; i < detalles.Count; i++)
            {
                var detalle = detalles[i];

                // Creamos el movimiento de inventario
                var movimiento = new MovimientosInventario()
                {
                    FechaMovimiento = GetIntDate(taskItem.FechaFactura),
                    TipoMovimiento = EnumTipoMovimiento.Egreso,
                    IdProducto = detalle.IdProducto,
                    PrecioUnitario = detalle.Precio,
                    Total = detalle.Cantidad * detalle.Precio,

                    Cantidad = detalle.Cantidad,
                    Activo = detalle.Activo,
                    FechaCreacion = DateTime.Now,

                };
                await context.MovimientosInventario.AddAsync(movimiento);
                await context.SaveChangesAsync();

                // creamos el detalle
                detalle.IdFactura = taskItem.Id;
                detalle.IdMovimiento = movimiento.Id;
                await context.FacturaDetalles.AddAsync(detalle);
                await context.SaveChangesAsync();

                // Actualizamos el saldo actual
                var saldo = saldos.First(e => e.IdProducto == detalle.IdProducto);
                saldo.Cantidad -= detalle.Cantidad;
                saldo.Total -= detalle.Cantidad * detalle.Precio;
                saldo.PrecioPromedio = saldo.Cantidad > 0
                    ? saldo.Total / saldo.Cantidad
                    : 0m;

                context.SaldosProducto.Update(saldo);
                await context.SaveChangesAsync();

            }

            await transaction.CommitAsync();

            return RespuestaGenericaDto.ExitoComun();
        }

        public async Task<RespuestaGenericaDto> Eliminar(long idFacturar)
        {
            var factura = await context
                .Facturas
                .FirstOrDefaultAsync(e => e.Id == idFacturar);
            if (factura == null) return RespuestaGenericaDto.ErrorComun("La factura no existe");

            var detalles = await context
                .FacturaDetalles
                .Include(e => e.Productos)
                .Include(e => e.Movimientos)
                .AsSplitQuery()
                .Where(e => e.IdFactura == idFacturar && e.Activo)
                .ToListAsync();
            var idsProducto = detalles.Select(e => e.IdProducto);

            var saldos = await context
                .SaldosProducto
                .Where(e => idsProducto.Contains(e.IdProducto))
                .ToListAsync();

            var transaction = await context.Database.BeginTransactionAsync();

            factura.Activo = false;
            factura.FechaModificacion = DateTime.Now;

            context.Facturas.Update(factura);
            await context.SaveChangesAsync();

            foreach (var detalle in detalles)
            {
                var movimiento = detalle.Movimientos;

                movimiento.Activo = false;
                movimiento.FechaModificacion = DateTime.Now;
                context.MovimientosInventario.Update(movimiento);

                var saldo = saldos.FirstOrDefault(e => e.IdProducto == detalle.IdProducto);
                if (saldo is null)
                    return RespuestaGenericaDto.ErrorComun($"No existe stock para el producto: {detalle.IdProducto}");

                saldo.Cantidad += movimiento.Cantidad;
                saldo.Total += movimiento.Total;
                saldo.PrecioPromedio = saldo.Cantidad > 0
                    ? saldo.Total / saldo.Cantidad : 0;
                context.SaldosProducto.Update(saldo);

                detalle.Activo = false;
                detalle.FechaModificacion = DateTime.Now;
                context.FacturaDetalles.Update(detalle);
                await context.SaveChangesAsync();
            }


            await transaction.CommitAsync();

            return RespuestaGenericaDto.ExitoComun();
        }

        private static int GetIntDate(DateTime fecha)
        {
            return Convert.ToInt32($"{fecha.Year}{fecha.Month:00}{fecha.Day:00}");
        }

        private static decimal CalcularImpuesto(EnumTipoImpuestoAplicar enumTipo, decimal subtotal)
        {
            return enumTipo switch
            {
                EnumTipoImpuestoAplicar.IVA => subtotal * 0.15m,
                EnumTipoImpuestoAplicar.IGV => subtotal * 0.18m,
                _ => throw new InvalidOperationException("Tipo de impuesto no reconocido")
            };
        }
    }
}
