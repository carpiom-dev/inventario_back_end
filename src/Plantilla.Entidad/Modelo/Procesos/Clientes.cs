using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plantilla.Infraestructura.Modelo.Enums;

namespace Plantilla.Entidad.Modelo.Procesos
{
    [Table(nameof(Clientes), Schema = "INV")]
    public class Clientes : AuditFields
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(40)]
        public required string NumeroIdentificacion { get; set; }

        [MaxLength(100)]
        public required string RazonSocial { get; set; }

        [MaxLength(500)]
        public required string Descripcion { get; set; }

        public required EnumTipoImpuestoAplicar TipoImpuesto { get; set; }
    }
}
