using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Plantilla.Dto.Modelo.Procesos.Movimientos
{
    public class KardexDto
    {
        [Display(Name = "Fecha Movimiento")]
        public DateTime FechaMovimiento { get; set; }

        [Display(Name = "Tipo Movimiento")]
        public string TipoMovimiento { get; set; } = string.Empty;

        [Display(Name = "Producto")]
        public string NombreProducto { get; set; } = string.Empty;

        [Display(Name = "Ingreso")]
        public string CantidadIngreso { get; set; } = string.Empty;

        [Display(Name = "Egreso")]
        public string CantidadEgreso { get; set; } = string.Empty;

        [Display(Name = "Saldo")]
        public string CantidadSaldo { get; set; } = string.Empty;

        public class ConsultarKardex
        {
            [Required(ErrorMessage = "El producto es obligatorio")]
            public long? IdProducto { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
        }

    }
}
