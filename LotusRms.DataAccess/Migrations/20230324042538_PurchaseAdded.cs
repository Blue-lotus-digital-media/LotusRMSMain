using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Supplier_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Bill_Amount = table.Column<float>(type: "float", nullable: false),
                    Discount_Type = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    Payment_Mode = table.Column<int>(type: "int", nullable: false),
                    Paid_Amount = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Purchases_LotusRMS_Suppliers_Supplier_Id",
                        column: x => x.Supplier_Id,
                        principalTable: "LotusRMS_Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LotusRMS_PurchaseDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<float>(type: "float", nullable: false),
                    Rate = table.Column<float>(type: "float", nullable: false),
                    LotusRMS_PurchaseId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_PurchaseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_PurchaseDetail_LotusRMS_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "LotusRMS_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotusRMS_PurchaseDetail_LotusRMS_Purchases_LotusRMS_Purchase~",
                        column: x => x.LotusRMS_PurchaseId,
                        principalTable: "LotusRMS_Purchases",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_PurchaseDetail_LotusRMS_PurchaseId",
                table: "LotusRMS_PurchaseDetail",
                column: "LotusRMS_PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_PurchaseDetail_Product_Id",
                table: "LotusRMS_PurchaseDetail",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Purchases_Supplier_Id",
                table: "LotusRMS_Purchases",
                column: "Supplier_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_PurchaseDetail");

            migrationBuilder.DropTable(
                name: "LotusRMS_Purchases");
        }
    }
}
