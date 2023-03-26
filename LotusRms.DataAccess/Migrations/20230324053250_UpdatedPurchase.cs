using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bill_No",
                table: "LotusRMS_Purchases",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bill_No",
                table: "LotusRMS_Purchases");
        }
    }
}
