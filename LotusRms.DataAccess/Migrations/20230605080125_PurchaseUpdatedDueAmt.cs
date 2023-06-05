using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseUpdatedDueAmt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Due",
                table: "LotusRMS_Purchases",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Due",
                table: "LotusRMS_Purchases",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
