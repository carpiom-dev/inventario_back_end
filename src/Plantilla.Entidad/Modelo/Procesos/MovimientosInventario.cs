
using Microsoft.EntityFrameworkCore;
using Plantilla.Infraestructura.Modelo.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantilla.Entidad.Modelo.Procesos
{
    [Table(nameof(MovimientosInventario), Schema = "INV")]
    public class MovimientosInventario : AuditFields
    {
        [Key]
        public long Id { get; set; }
        public required EnumTipoMovimiento TipoMovimiento { get; set; }
        public required int FechaMovimiento { get; set; }

        [ForeignKey(nameof(Producto))]
        public required long IdProducto { get; set; }
        public virtual Productos Producto { get; set; } = null!;

        [Precision(16, 4)]
        public required decimal PrecioUnitario { get; set; }

        [Precision(16, 4)]
        public required decimal Cantidad { get; set; }

        [Precision(16, 4)]
        public required decimal Total { get; set; }
    }
}
