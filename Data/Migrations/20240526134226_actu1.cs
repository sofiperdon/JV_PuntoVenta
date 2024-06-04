using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JV_PuntoVenta.Data.Migrations
{
    /// <inheritdoc />
    public partial class actu1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VentaProductos_ProductoId",
                table: "VentaProductos",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaProductos_Productos_ProductoId",
                table: "VentaProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaProductos_Productos_ProductoId",
                table: "VentaProductos");

            migrationBuilder.DropIndex(
                name: "IX_VentaProductos_ProductoId",
                table: "VentaProductos");
        }
    }
}
