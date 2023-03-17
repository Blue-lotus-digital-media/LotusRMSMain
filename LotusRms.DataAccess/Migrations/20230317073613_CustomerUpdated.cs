using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.Migrations
{
    /// <inheritdoc />
    public partial class CustomerUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_DueBooks_LotusRMS_Invoices_Invoice_Id",
                table: "LotusRMS_DueBooks");

            migrationBuilder.AlterColumn<Guid>(
                name: "Invoice_Id",
                table: "LotusRMS_DueBooks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<float>(
                name: "Invoice_Amount",
                table: "LotusRMS_DueBooks",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "Customer_Contact",
                table: "LotusRMS_Checkout",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Customer_Address",
                table: "LotusRMS_Checkout",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_DueBooks_LotusRMS_Invoices_Invoice_Id",
                table: "LotusRMS_DueBooks",
                column: "Invoice_Id",
                principalTable: "LotusRMS_Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotusRMS_DueBooks_LotusRMS_Invoices_Invoice_Id",
                table: "LotusRMS_DueBooks");

            migrationBuilder.DropColumn(
                name: "Invoice_Amount",
                table: "LotusRMS_DueBooks");

            migrationBuilder.AlterColumn<Guid>(
                name: "Invoice_Id",
                table: "LotusRMS_DueBooks",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "LotusRMS_Checkout",
                keyColumn: "Customer_Contact",
                keyValue: null,
                column: "Customer_Contact",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Customer_Contact",
                table: "LotusRMS_Checkout",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "LotusRMS_Checkout",
                keyColumn: "Customer_Address",
                keyValue: null,
                column: "Customer_Address",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Customer_Address",
                table: "LotusRMS_Checkout",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_LotusRMS_DueBooks_LotusRMS_Invoices_Invoice_Id",
                table: "LotusRMS_DueBooks",
                column: "Invoice_Id",
                principalTable: "LotusRMS_Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
