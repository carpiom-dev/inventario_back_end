using System.ComponentModel.DataAnnotations;

namespace Plantilla.Dto.Modelo.Procesos.Movimientos
{
    public class KardexValorizadoDto
    {
        public DateTime FechaMovimiento { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public string CantidadIngreso { get; set; } = string.Empty;
        public string DolaresIngreso { get; set; } = string.Empty;
        public string CantidadEgreso { get; set; } = string.Empty;
        public string DolaresEgreso { get; set; } = string.Empty;
        public string CantidadSaldo { get; set; } = string.Empty;
        public string DolaresSaldo { get; set; } = string.Empty;

        public class ConsultarKardexValorizado
        {
            [Required(ErrorMessage = "El producto es obligatorio")]
            public long? IdProducto { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
        }

    }
}
