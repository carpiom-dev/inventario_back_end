using System.ComponentModel.DataAnnotations;

namespace Plantilla.Entidad.Modelo
{
    public class AuditFields
    {
        public required bool Activo { get; set; }
        public required DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
