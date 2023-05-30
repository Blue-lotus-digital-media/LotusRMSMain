using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UnitUpdatedInIncredian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Unit_Id",
                table: "LotusRMS_MenuIncredians",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_MenuIncredians_Unit_Id",
                table: "LotusRMS_MenuIncredians",
                column: "Unit_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_MenuIncredians_LotusRMS_Units_Unit_Id",
                table: "LotusRMS_MenuIncredians",
                column: "Unit_Id",
                principalTable: "LotusRMS_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_MenuIncredians_LotusRMS_Units_Unit_Id",
                table: "LotusRMS_MenuIncredians");

            migrationBuilder.DropIndex(
                name: "IX_LotusRMS_MenuIncredians_Unit_Id",
                table: "LotusRMS_MenuIncredians");

            migrationBuilder.DropColumn(
                name: "Unit_Id",
                table: "LotusRMS_MenuIncredians");
        }
    }
}
