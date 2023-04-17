using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GallaAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_Gallas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Cashier = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Balance = table.Column<double>(type: "double", nullable: false),
                    Closing_Balance = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Gallas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Gallas_AspNetUsers_Cashier",
                        column: x => x.Cashier,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Gallas_Cashier",
                table: "LotusRMS_Gallas",
                column: "Cashier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_Gallas");
        }
    }
}
