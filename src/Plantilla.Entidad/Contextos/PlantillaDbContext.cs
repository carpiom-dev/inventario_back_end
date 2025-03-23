using System.Reflection;
using Plantilla.Entidad.Modelo.Identity;
using Plantilla.Entidad.Modelo.Procesos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Plantilla.Entidad.Contextos
{
    public class PlantillaDbContext(DbContextOptions<PlantillaDbContext> options) : IdentityDbContext<AuthUser, AuthRol, long>(options)
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<MovimientosInventario> MovimientosInventario { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<SaldosProducto> SaldosProducto { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<FacturaDetalles> FacturaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Llama la configuración base

            modelBuilder.Entity<FacturaDetalles>()
                .HasOne(fd => fd.Productos)
                .WithMany()
                .HasForeignKey(fd => fd.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);  // Restringe la eliminación en cascada

            modelBuilder.Entity<FacturaDetalles>()
            .HasOne(fd => fd.Facturas)
                .WithMany()
                .HasForeignKey(fd => fd.IdFactura)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}