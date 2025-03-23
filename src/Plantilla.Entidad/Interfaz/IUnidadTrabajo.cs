using Plantilla.Entidad.Interfaz.Identity;
using Plantilla.Entidad.Interfaz.Procesos;

namespace Plantilla.Entidad.Interfaz
{
    public interface IUnidadTrabajo
    {
        public IRolRepositorio Roles { get; }
        public IUsuarioRepositorio Usuarios { get; }
        public ITaskItemRepositorio TaskItems { get; }
        public IProductosRepositorio Productos { get; }
        public IMovimientosInventarioRepositorio MovimientosInventario { get; }
        public ISaldosProductoRepositorio SaldosProducto { get; }
        public IClientesRepositorio Clientes { get; }
        public IFacturasRepositorio Facturas { get; }

        void Begin();

        void Commit();

        void Rollback();
    }
}