using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MenuDetailUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "LotusRMS_Menus");

            migrationBuilder.DropColumn(
                name: "Unit_Quantity",
                table: "LotusRMS_Menus");

            migrationBuilder.CreateTable(
                name: "LotusRMS_MenuDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Rate = table.Column<double>(type: "double", nullable: false),
                    Default = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LotusRMS_MenuId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_MenuDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_MenuDetail_LotusRMS_Menus_LotusRMS_MenuId",
                        column: x => x.LotusRMS_MenuId,
                        principalTable: "LotusRMS_Menus",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_MenuDetail_LotusRMS_MenuId",
                table: "LotusRMS_MenuDetail",
                column: "LotusRMS_MenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_MenuDetail");

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "LotusRMS_Menus",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Unit_Quantity",
                table: "LotusRMS_Menus",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
