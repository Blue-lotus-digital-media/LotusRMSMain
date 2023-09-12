using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class logoadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                schema: "Identity",
                table: "company",
                type: "longblob",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                schema: "Identity",
                table: "company");
        }
    }
}
