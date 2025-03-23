using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plantilla.RepositorioEfCore.Migrations
{
    /// <inheritdoc />
    public partial class CrearFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TipoImpuesto = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<long>(type: "bigint", nullable: false),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Glosa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalSchema: "INV",
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacturaDetalles",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFactura = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(16,4)", precision: 16, scale: 4, nullable: false),
                    IdMovimiento = table.Column<long>(type: "bigint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaDetalles_Facturas_IdFactura",
                        column: x => x.IdFactura,
                        principalSchema: "INV",
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturaDetalles_MovimientosInventario_IdMovimiento",
                        column: x => x.IdMovimiento,
                        principalSchema: "INV",
                        principalTable: "MovimientosInventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaDetalles_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "INV",
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalles_IdFactura",
                schema: "INV",
                table: "FacturaDetalles",
                column: "IdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalles_IdMovimiento",
                schema: "INV",
                table: "FacturaDetalles",
                column: "IdMovimiento");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalles_IdProducto",
                schema: "INV",
                table: "FacturaDetalles",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdCliente",
                schema: "INV",
                table: "Facturas",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaDetalles",
                schema: "INV");

            migrationBuilder.DropTable(
                name: "Facturas",
                schema: "INV");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "INV");
        }
    }
}
