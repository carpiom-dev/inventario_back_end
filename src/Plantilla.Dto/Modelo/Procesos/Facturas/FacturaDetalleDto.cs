namespace Plantilla.Dto.Modelo.Procesos.Facturas
{
    public class FacturaDetalleDto
    {
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public class CrearFacturaDetalle
        {
            public long IdProducto { get; set; }
            public decimal Cantidad { get; set; }
        }

    }
}
