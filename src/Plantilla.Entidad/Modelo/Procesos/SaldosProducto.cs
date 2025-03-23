using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantilla.Entidad.Modelo.Procesos
{
    [Table(nameof(SaldosProducto), Schema = "INV")]
    public class SaldosProducto
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Producto))]
        public required long IdProducto { get; set; }
        public virtual Productos Producto { get; set; } = null!;

        [Precision(16, 4)]
        public required decimal PrecioPromedio { get; set; }

        [Precision(16, 4)]
        public required decimal Cantidad { get; set; }

        [Precision(16, 4)]
        public required decimal Total { get; set; }
        public required bool Activo { get; set; }
    }
}
