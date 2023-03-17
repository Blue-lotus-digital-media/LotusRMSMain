using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_Checkout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Customer_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Customer_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Customer_Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Customer_Contact = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Total = table.Column<float>(type: "float", nullable: false),
                    Discount_Type = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    Paid_Amount = table.Column<float>(type: "float", nullable: false),
                    Payment_Mode = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Invoice_No = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Checkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Checkout_LotusRMS_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "LotusRMS_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LotusRMS_Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Invoice_String = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Invoice_No = table.Column<int>(type: "int", nullable: false),
                    Print_Count = table.Column<int>(type: "int", nullable: false),
                    Checkout_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FiscalYear_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BillSetting_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Invoices_LotusRMS_BillSettings_BillSetting_Id",
                        column: x => x.BillSetting_Id,
                        principalTable: "LotusRMS_BillSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Invoices_LotusRMS_Checkout_Checkout_Id",
                        column: x => x.Checkout_Id,
                        principalTable: "LotusRMS_Checkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Invoices_LotusRMS_FiscalYears_FiscalYear_Id",
                        column: x => x.FiscalYear_Id,
                        principalTable: "LotusRMS_FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Checkout_Order_Id",
                table: "LotusRMS_Checkout",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Invoices_BillSetting_Id",
                table: "LotusRMS_Invoices",
                column: "BillSetting_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Invoices_Checkout_Id",
                table: "LotusRMS_Invoices",
                column: "Checkout_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Invoices_FiscalYear_Id",
                table: "LotusRMS_Invoices",
                column: "FiscalYear_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_Invoices");

            migrationBuilder.DropTable(
                name: "LotusRMS_Checkout");
        }
    }
}
