using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductStockAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Unit_Quantity",
                table: "LotusRMS_Products",
                type: "double",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LotusRMS_Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StockQuantity = table.Column<double>(type: "double", nullable: false),
                    ReorderLevel = table.Column<double>(type: "double", nullable: false),
                    Remarks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Inventory_LotusRMS_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "LotusRMS_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Inventory_Product_Id",
                table: "LotusRMS_Inventory",
                column: "Product_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_Inventory");

            migrationBuilder.AlterColumn<float>(
                name: "Unit_Quantity",
                table: "LotusRMS_Products",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
