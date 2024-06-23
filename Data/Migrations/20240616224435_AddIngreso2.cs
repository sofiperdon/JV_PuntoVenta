using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JV_PuntoVenta.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIngreso2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingreso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngresoProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngresoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresoProducto_Ingreso_IngresoId",
                        column: x => x.IngresoId,
                        principalTable: "Ingreso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresoProducto_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngresoProducto_IngresoId",
                table: "IngresoProducto",
                column: "IngresoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresoProducto_ProductoId",
                table: "IngresoProducto",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngresoProducto");

            migrationBuilder.DropTable(
                name: "Ingreso");
        }
    }
}
