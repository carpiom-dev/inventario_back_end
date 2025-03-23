using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantilla.Entidad.Modelo.Procesos
{
    [Table(nameof(Productos), Schema = "INV")]
    public class Productos : AuditFields
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public required string NombreProducto { get; set; }

        [MaxLength(250)]
        public required string DescripcionProducto { get; set; }
    }
}
