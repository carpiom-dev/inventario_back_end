using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plantilla.RepositorioEfCore.Migrations
{
    /// <inheritdoc />
    public partial class agregarProductoSaldos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "INV");

            migrationBuilder.CreateTable(
                name: "Productos",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionProducto = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosInventario",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosInventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosInventario_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "INV",
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaldosProducto",
                schema: "INV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    StockTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaldosProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaldosProducto_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalSchema: "INV",
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosInventario_IdProducto",
                schema: "INV",
                table: "MovimientosInventario",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_SaldosProducto_IdProducto",
                schema: "INV",
                table: "SaldosProducto",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosInventario",
                schema: "INV");

            migrationBuilder.DropTable(
                name: "SaldosProducto",
                schema: "INV");

            migrationBuilder.DropTable(
                name: "Productos",
                schema: "INV");
        }
    }
}
