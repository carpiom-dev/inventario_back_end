using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantilla.Entidad.Modelo.Procesos
{
    [Table(nameof(FacturaDetalles), Schema = "INV")]
    public class FacturaDetalles : AuditFields
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Facturas))]
        public required long IdFactura { get; set; }
        public virtual Facturas Facturas { get; set; } = null!;


        [ForeignKey(nameof(Productos))]
        public required long IdProducto { get; set; }
        public virtual Productos Productos { get; set; } = null!;


        [Precision(16, 4)]
        public required decimal Cantidad { get; set; }


        [Precision(16, 4)]
        public required decimal Precio { get; set; }


        [Precision(16, 4)]
        public required decimal Subtotal { get; set; }


        [Precision(16, 4)]
        public required decimal Descuento { get; set; }


        [Precision(16, 4)]
        public required decimal Impuesto { get; set; }


        [Precision(16, 4)]
        public required decimal Total { get; set; }

        [ForeignKey(nameof(Movimientos))]
        public required long IdMovimiento { get; set; } 
        public virtual MovimientosInventario Movimientos { get; set; } = null!;

    }
}
