using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class CheckoutCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Orders_Table_Id",
                table: "LotusRMS_Orders",
                column: "Table_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_Orders_LotusRMS_Tables_Table_Id",
                table: "LotusRMS_Orders",
                column: "Table_Id",
                principalTable: "LotusRMS_Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_Orders_LotusRMS_Tables_Table_Id",
                table: "LotusRMS_Orders");

            migrationBuilder.DropIndex(
                name: "IX_LotusRMS_Orders_Table_Id",
                table: "LotusRMS_Orders");
        }
    }
}
