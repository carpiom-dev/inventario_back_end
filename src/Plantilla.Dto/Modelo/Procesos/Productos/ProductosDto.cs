using System.ComponentModel.DataAnnotations;

namespace Plantilla.Dto.Modelo.Procesos.Productos
{
    public class ProductosDto
    {
        public long Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string DescripcionProducto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public class CrearProducto
        {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string? NombreProducto { get; set; }

            [Required(ErrorMessage = "La descripción es obligatorio")]
            public string? DescripcionProducto { get; set; }
        }

        public class ActualizarProducto
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public long? Id { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string? NombreProducto { get; set; }

            [Required(ErrorMessage = "La descripción es obligatorio")]
            public string? DescripcionProducto { get; set; }
        }

        public class EliminarProducto
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public long? Id { get; set; }
        }

        public class ConsultarProducto
        {
            [Required(ErrorMessage = "El id es obligatorio")]
            public long? Id { get; set; }
        }
    }
}
