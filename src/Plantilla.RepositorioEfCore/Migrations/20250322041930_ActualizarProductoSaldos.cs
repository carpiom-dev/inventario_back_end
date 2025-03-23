using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plantilla.RepositorioEfCore.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarProductoSaldos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockTotal",
                schema: "INV",
                table: "SaldosProducto");

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                schema: "INV",
                table: "SaldosProducto",
                type: "decimal(16,4)",
                precision: 16,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioPromedio",
                schema: "INV",
                table: "SaldosProducto",
                type: "decimal(16,4)",
                precision: 16,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                schema: "INV",
                table: "SaldosProducto",
                type: "decimal(16,4)",
                precision: 16,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                schema: "INV",
                table: "MovimientosInventario",
                type: "decimal(16,4)",
                precision: 16,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                schema: "INV",
                table: "MovimientosInventario",
                type: "decimal(16,4)",
                precision: 16,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                schema: "INV",
                table: "MovimientosInventario",
                type: "decimal(16,4)",
                precision: 16,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                schema: "INV",
                table: "SaldosProducto");

            migrationBuilder.DropColumn(
                name: "PrecioPromedio",
                schema: "INV",
                table: "SaldosProducto");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "INV",
                table: "SaldosProducto");

            migrationBuilder.AddColumn<decimal>(
                name: "StockTotal",
                schema: "INV",
                table: "SaldosProducto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                schema: "INV",
                table: "MovimientosInventario",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,4)",
                oldPrecision: 16,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                schema: "INV",
                table: "MovimientosInventario",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,4)",
                oldPrecision: 16,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                schema: "INV",
                table: "MovimientosInventario",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,4)",
                oldPrecision: 16,
                oldScale: 4);
        }
    }
}
