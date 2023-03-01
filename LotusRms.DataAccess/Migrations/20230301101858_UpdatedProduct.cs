using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Product_Categories_Product_Unit_Id",
                table: "LotusRMS_Products");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Products_Product_Category_Id",
                table: "LotusRMS_Products",
                column: "Product_Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Product_Categories_Product_Catego~",
                table: "LotusRMS_Products",
                column: "Product_Category_Id",
                principalTable: "LotusRMS_Product_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Product_Categories_Product_Catego~",
                table: "LotusRMS_Products");

            migrationBuilder.DropIndex(
                name: "IX_LotusRMS_Products_Product_Category_Id",
                table: "LotusRMS_Products");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Product_Categories_Product_Unit_Id",
                table: "LotusRMS_Products",
                column: "Product_Unit_Id",
                principalTable: "LotusRMS_Product_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
