using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Menuunitupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_Unit_Division",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_Menu_UnitId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Unit_Division", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Unit_Division_LotusRMS_Menu_Units_LotusRMS_Menu_Uni~",
                        column: x => x.LotusRMS_Menu_UnitId,
                        principalTable: "LotusRMS_Menu_Units",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Unit_Division_LotusRMS_Menu_UnitId",
                table: "LotusRMS_Unit_Division",
                column: "LotusRMS_Menu_UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_Unit_Division");
        }
    }
}
