using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class OrderAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotusRMS_Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Order_No = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderBy = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Total = table.Column<float>(type: "float", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    IsCheckout = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Orders_AspNetUsers_OrderBy",
                        column: x => x.OrderBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LotusRMS_Order_Details",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<float>(type: "float", nullable: false),
                    Rate = table.Column<float>(type: "float", nullable: false),
                    GetTotal = table.Column<float>(type: "float", nullable: false),
                    IsComplete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsQrOrder = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsQrVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MenuId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LotusRMS_OrderId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotusRMS_Order_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Order_Details_LotusRMS_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "LotusRMS_Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotusRMS_Order_Details_LotusRMS_Orders_LotusRMS_OrderId",
                        column: x => x.LotusRMS_OrderId,
                        principalTable: "LotusRMS_Orders",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Order_Details_LotusRMS_OrderId",
                table: "LotusRMS_Order_Details",
                column: "LotusRMS_OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Order_Details_MenuId",
                table: "LotusRMS_Order_Details",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_LotusRMS_Orders_OrderBy",
                table: "LotusRMS_Orders",
                column: "OrderBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotusRMS_Order_Details");

            migrationBuilder.DropTable(
                name: "LotusRMS_Orders");
        }
    }
}
