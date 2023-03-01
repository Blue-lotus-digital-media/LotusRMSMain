using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Products_Product_Unit_Id",
                table: "LotusRMS_Products");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Units_Product_Unit_Id",
                table: "LotusRMS_Products",
                column: "Product_Unit_Id",
                principalTable: "LotusRMS_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Units_Product_Unit_Id",
                table: "LotusRMS_Products");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Products_Product_Unit_Id",
                table: "LotusRMS_Products",
                column: "Product_Unit_Id",
                principalTable: "LotusRMS_Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
