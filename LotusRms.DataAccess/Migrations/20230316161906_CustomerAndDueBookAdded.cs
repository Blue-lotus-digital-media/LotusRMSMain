using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class CustomerAndDueBookAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PanOrVat = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Customers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LotusRMS_DueBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DueDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Invoice_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DueAmount = table.Column<float>(type: "float", nullable: false),
                    PaidAmount = table.Column<float>(type: "float", nullable: false),
                    BalanceDue = table.Column<float>(type: "float", nullable: false),
                    LotusRMS_CustomerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_DueBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_DueBooks_LotusRMS_Customers_LotusRMS_CustomerId",
                        column: x => x.LotusRMS_CustomerId,
                        principalTable: "LotusRMS_Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LotusRMS_DueBooks_LotusRMS_Invoices_Invoice_Id",
                        column: x => x.Invoice_Id,
                        principalTable: "LotusRMS_Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_DueBooks_Invoice_Id",
                table: "LotusRMS_DueBooks",
                column: "Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_DueBooks_LotusRMS_CustomerId",
                table: "LotusRMS_DueBooks",
                column: "LotusRMS_CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_DueBooks");

            migrationBuilder.DropTable(
                name: "LotusRMS_Customers");
        }
    }
}
