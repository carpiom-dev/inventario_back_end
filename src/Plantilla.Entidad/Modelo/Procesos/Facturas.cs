using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantilla.Entidad.Modelo.Procesos
{
    [Table(nameof(Facturas), Schema = "INV")]
    public class Facturas : AuditFields
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(Clientes))]
        public required long IdCliente { get; set; }
        public virtual Clientes Clientes { get; set; } = null!;

        public required DateTime FechaFactura { get; set; }
        
        public required string Glosa { get; set; }
        
        [Precision(16, 4)]
        public required decimal Subtotal { get; set; }

        [Precision(16, 4)]
        public required decimal Descuento { get; set; }
        
        [Precision(16, 4)]
        public required decimal Impuesto { get; set; }
        
        [Precision(16, 4)]
        public required decimal Total { get; set; }

    }
}
