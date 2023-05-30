using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MenuIncrredianAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_MenuIncredians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_MenuId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_MenuIncredians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_MenuIncredians_LotusRMS_Menus_LotusRMS_MenuId",
                        column: x => x.LotusRMS_MenuId,
                        principalTable: "LotusRMS_Menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LotusRMS_MenuIncredians_LotusRMS_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "LotusRMS_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_MenuIncredians_LotusRMS_MenuId",
                table: "LotusRMS_MenuIncredians",
                column: "LotusRMS_MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_MenuIncredians_Product_Id",
                table: "LotusRMS_MenuIncredians",
                column: "Product_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_MenuIncredians");
        }
    }
}
