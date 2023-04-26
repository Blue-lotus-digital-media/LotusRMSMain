using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DoubleUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "LotusRMS_Order_Details",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "LotusRMS_Order_Details",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "Quantity_Id",
                table: "LotusRMS_Order_Details",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<double>(
                name: "PaidAmount",
                table: "LotusRMS_DueBooks",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Invoice_Amount",
                table: "LotusRMS_DueBooks",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DueAmount",
                table: "LotusRMS_DueBooks",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "BalanceDue",
                table: "LotusRMS_DueBooks",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "LotusRMS_Checkout",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity_Id",
                table: "LotusRMS_Order_Details");

            migrationBuilder.AlterColumn<float>(
                name: "Rate",
                table: "LotusRMS_Order_Details",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "LotusRMS_Order_Details",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "PaidAmount",
                table: "LotusRMS_DueBooks",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Invoice_Amount",
                table: "LotusRMS_DueBooks",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "DueAmount",
                table: "LotusRMS_DueBooks",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "BalanceDue",
                table: "LotusRMS_DueBooks",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "LotusRMS_Checkout",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
