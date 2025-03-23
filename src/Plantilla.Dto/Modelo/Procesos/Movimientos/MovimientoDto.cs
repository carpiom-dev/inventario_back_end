using System.ComponentModel.DataAnnotations;

namespace Plantilla.Dto.Modelo.Procesos.Movimientos
{
    public class MovimientoDto
    {
        public class CrearMovimiento
        {
            [Required(ErrorMessage = "El producto es obligatorio")]
            public long? IdProducto { get; set; }

            [Required(ErrorMessage = "La cantidad es obligatorio")]
            public decimal? Cantidad { get; set; }

            [Required(ErrorMessage = "El precio unitario es obligatorio")]
            public decimal? PrecioUnitario { get; set; }
        }
    }
}
