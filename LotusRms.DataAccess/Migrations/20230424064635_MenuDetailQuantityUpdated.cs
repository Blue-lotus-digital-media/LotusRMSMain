using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MenuDetailQuantityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Quantity",
                table: "LotusRMS_MenuDetail",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_MenuDetail_Quantity",
                table: "LotusRMS_MenuDetail",
                column: "Quantity");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_MenuDetail_LotusRMS_Unit_Division_Quantity",
                table: "LotusRMS_MenuDetail",
                column: "Quantity",
                principalTable: "LotusRMS_Unit_Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_MenuDetail_LotusRMS_Unit_Division_Quantity",
                table: "LotusRMS_MenuDetail");

            migrationBuilder.DropIndex(
                name: "IX_LotusRMS_MenuDetail_Quantity",
                table: "LotusRMS_MenuDetail");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "LotusRMS_MenuDetail",
                type: "double",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
