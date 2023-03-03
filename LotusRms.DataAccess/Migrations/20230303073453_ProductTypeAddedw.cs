using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class ProductTypeAddedw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Product_Type_Id",
                table: "LotusRMS_Products",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<float>(
                name: "Unit_Quantity",
                table: "LotusRMS_Products",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Products_Product_Type_Id",
                table: "LotusRMS_Products",
                column: "Product_Type_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Product_Types_Product_Type_Id",
                table: "LotusRMS_Products",
                column: "Product_Type_Id",
                principalTable: "LotusRMS_Product_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Products_LotusRMS_Product_Types_Product_Type_Id",
                table: "LotusRMS_Products");

            migrationBuilder.DropIndex(
                name: "IX_LotusRMS_Products_Product_Type_Id",
                table: "LotusRMS_Products");

            migrationBuilder.DropColumn(
                name: "Product_Type_Id",
                table: "LotusRMS_Products");

            migrationBuilder.DropColumn(
                name: "Unit_Quantity",
                table: "LotusRMS_Products");
        }
    }
}
