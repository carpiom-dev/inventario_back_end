using Plantilla.Infraestructura.Modelo.Enums;
using System.ComponentModel.DataAnnotations;

namespace Plantilla.Dto.Modelo.Procesos.Clientes
{
    public class ClientesDto
    {
        public long Id { get; set; }
        public string NumeroIdentificacion { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public class CrearCliente
        {
            [Required(ErrorMessage = "Número de Identificación es obligatorio")]
            public string? NumeroIdentificacion { get; set; }

            [Required(ErrorMessage = "La razón social es obligatorio")]
            public string? RazonSocial { get; set; }

            [Required(ErrorMessage = "La descripción es obligatorio")]
            public string? Descripcion { get; set; }

            [Required(ErrorMessage = "El Tipo Impuesto es obligatorio")]
            public EnumTipoImpuestoAplicar? TipoImpuesto { get; set; }
        }

        public class ActualizarCliente
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "Número de Identificación es obligatorio")]
            public string? NumeroIdentificacion { get; set; }

            [Required(ErrorMessage = "La razón social es obligatorio")]
            public string? RazonSocial { get; set; }

            [Required(ErrorMessage = "La descripción es obligatorio")]
            public string? Descripcion { get; set; }

            [Required(ErrorMessage = "El Tipo Impuesto es obligatorio")]
            public EnumTipoImpuestoAplicar? TipoImpuesto { get; set; }
        }

        public class EliminarCliente
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ConsultarCliente
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ConsultarClientes
        {
            public string? NumeroIdentificacion { get; set; }
            public string? NombreContiene { get; set; }
        }
    }
}
