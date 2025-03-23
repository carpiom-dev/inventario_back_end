using System.ComponentModel.DataAnnotations;

namespace Plantilla.Dto.Modelo.Procesos.Facturas
{
    public class FacturaDto
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime FechaFactura { get; set; }
        public string Glosa { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;

        public class Detallado : FacturaDto
        {
            public List<FacturaDetalleDto> Detalles { get; set; } = [];
        }


        public class ConsultarFactura
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }
        public class EliminarFactura
        {
            [Required(ErrorMessage = "Id es obligatorio")]
            public long? Id { get; set; }
        }
        public class CrearFactura
        {
            [Required(ErrorMessage = "Cliente es obligatorio")]
            public long? IdCliente { get; set; }

            [Required(ErrorMessage = "Fecha de Factura es obligatorio")]
            public DateTime? FechaFactura { get; set; }

            [Required(ErrorMessage = "Glosa es obligatorio")]
            public string? Glosa { get; set; } = string.Empty;

            [Required(ErrorMessage = "Detalles son obligatorios")]
            public List<FacturaDetalleDto.CrearFacturaDetalle>? Detalles { get; set; }

        }
    }
}
