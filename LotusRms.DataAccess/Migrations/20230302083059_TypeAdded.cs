using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class TypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Type_Id",
                table: "LotusRMS_Product_Categories",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "LotusRMS_Product_Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Product_Types", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Product_Categories_Type_Id",
                table: "LotusRMS_Product_Categories",
                column: "Type_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Product_Categories_LotusRMS_Product_Types_Type_Id",
                table: "LotusRMS_Product_Categories",
                column: "Type_Id",
                principalTable: "LotusRMS_Product_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Product_Categories_LotusRMS_Product_Types_Type_Id",
                table: "LotusRMS_Product_Categories");

            migrationBuilder.DropTable(
                name: "LotusRMS_Product_Types");

            migrationBuilder.DropIndex(
                name: "IX_LotusRMS_Product_Categories_Type_Id",
                table: "LotusRMS_Product_Categories");

            migrationBuilder.DropColumn(
                name: "Type_Id",
                table: "LotusRMS_Product_Categories");
        }
    }
}
