using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GallaDetailAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_GallaDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Withdrawl = table.Column<double>(type: "double", nullable: false),
                    Deposit = table.Column<double>(type: "double", nullable: false),
                    Balance = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_GallaId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_GallaDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_GallaDetail_LotusRMS_Gallas_LotusRMS_GallaId",
                        column: x => x.LotusRMS_GallaId,
                        principalTable: "LotusRMS_Gallas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_GallaDetail_LotusRMS_GallaId",
                table: "LotusRMS_GallaDetail",
                column: "LotusRMS_GallaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_GallaDetail");
        }
    }
}
