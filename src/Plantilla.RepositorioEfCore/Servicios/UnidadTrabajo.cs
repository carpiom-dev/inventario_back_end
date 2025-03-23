using System.Data;
using Plantilla.Entidad.Contextos;
using Plantilla.Entidad.Interfaz;
using Plantilla.Entidad.Interfaz.Identity;
using Plantilla.Entidad.Interfaz.Procesos;
using Plantilla.Entidad.Modelo.Identity;
using Plantilla.RepositorioEfCore.Servicios.Identity;
using Plantilla.RepositorioEfCore.Servicios.Procesos;
using Microsoft.AspNetCore.Identity;

namespace Plantilla.RepositorioEfCore.Servicios
{
    internal class UnidadTrabajo(IDbConnection connection,
        RoleManager<AuthRol> roleManager, UserManager<AuthUser> userManager,
        PlantillaDbContext context) : IUnidadTrabajo
    {
        private readonly IDbConnection _connection = connection;
        private IDbTransaction? _transaction;

        public IRolRepositorio Roles => new RolRepositorio(roleManager);
        public IUsuarioRepositorio Usuarios => new UsuarioRepositorio(userManager, context);
        public ITaskItemRepositorio TaskItems => new TaskItemRepositorio(context);

        public IProductosRepositorio Productos => new ProductosRepositorio(context);

        public IMovimientosInventarioRepositorio MovimientosInventario => new MovimientosInventarioRepositorio(context);

        public ISaldosProductoRepositorio SaldosProducto => new SaldosProductoRepositorio(context);
        public IClientesRepositorio Clientes => new ClientesRepositorio(context);
        public IFacturasRepositorio Facturas => new FacturasRepositorio(context);

        public void Begin()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No hay una transacci�n en curso.");

            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
                _connection.Close();
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
                return;

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
                _connection.Close();
            }
        }
    }
}