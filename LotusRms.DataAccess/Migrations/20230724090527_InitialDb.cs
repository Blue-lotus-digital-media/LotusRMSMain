using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotusRMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "company",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Province = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tole = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PanOrVat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistrationDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistrationNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyRegistrationNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContractDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceStartDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValidTill = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IpV4Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_billsettings",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BillPrefix = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BillTitle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BillAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BillNote = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPhone = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPanOrVat = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFiscalYear = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_billsettings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_customers",
                schema: "Identity",
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
                    table.PrimaryKey("PK_lotusrms_customers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_fiscalyears",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDateAD = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDateBS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndDateAD = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndDateBS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_fiscalyears", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_menu_types",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_menu_types", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_menu_units",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Unit_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit_Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_menu_units", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_product_types",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_product_types", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_suppliers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PanOrVat = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_suppliers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_table_types",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_table_types", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_units",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Unit_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit_Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_units", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfilePicture = table.Column<byte[]>(type: "longblob", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contactperson",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PersonName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LotusRMS_CompanyId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactperson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contactperson_company_LotusRMS_CompanyId",
                        column: x => x.LotusRMS_CompanyId,
                        principalSchema: "Identity",
                        principalTable: "company",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_menu_categories",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Category_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_menu_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_menu_categories_lotusrms_menu_types_Type_Id",
                        column: x => x.Type_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menu_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_unit_division",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_Menu_UnitId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_unit_division", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_unit_division_lotusrms_menu_units_LotusRMS_Menu_Uni~",
                        column: x => x.LotusRMS_Menu_UnitId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menu_units",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_product_categories",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Category_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_product_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_product_categories_lotusrms_product_types_Type_Id",
                        column: x => x.Type_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_product_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_purchases",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Supplier_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Bill_No = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bill_Amount = table.Column<double>(type: "double", nullable: false),
                    Discount_Type = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "double", nullable: false),
                    Payment_Mode = table.Column<int>(type: "int", nullable: false),
                    Paid_Amount = table.Column<double>(type: "double", nullable: false),
                    Due = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_purchases_lotusrms_suppliers_Supplier_Id",
                        column: x => x.Supplier_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_tables",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Table_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Table_No = table.Column<int>(type: "int", nullable: false),
                    No_Of_Chair = table.Column<int>(type: "int", nullable: false),
                    IsReserved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Table_Type_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_tables_lotusrms_table_types_Table_Type_Id",
                        column: x => x.Table_Type_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_table_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roleclaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roleclaims_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_gallas",
                schema: "Identity",
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
                    table.PrimaryKey("PK_lotusrms_gallas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_gallas_users_Cashier",
                        column: x => x.Cashier,
                        principalSchema: "Identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userclaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userclaims_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userlogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_userlogins_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userroles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_userroles_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userroles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usertokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_usertokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_menus",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Item_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Category_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Image = table.Column<byte[]>(type: "longblob", nullable: true),
                    OrderTo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_menus_lotusrms_menu_categories_Category_Id",
                        column: x => x.Category_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menu_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_menus_lotusrms_menu_types_Type_Id",
                        column: x => x.Type_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menu_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_menus_lotusrms_menu_units_Unit_Id",
                        column: x => x.Unit_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menu_units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_products",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Product_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Product_Unit_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Unit_Quantity = table.Column<double>(type: "double", nullable: true),
                    Product_Category_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Type_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_products_lotusrms_product_categories_Product_Catego~",
                        column: x => x.Product_Category_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_product_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_products_lotusrms_product_types_Product_Type_Id",
                        column: x => x.Product_Type_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_product_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_products_lotusrms_units_Product_Unit_Id",
                        column: x => x.Product_Unit_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_orders",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Order_No = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Table_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrderBy = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Total = table.Column<float>(type: "float", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    IsCheckout = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_orders_lotusrms_tables_Table_Id",
                        column: x => x.Table_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_orders_users_OrderBy",
                        column: x => x.OrderBy,
                        principalSchema: "Identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_galladetail",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Withdrawl = table.Column<double>(type: "double", nullable: false),
                    Deposit = table.Column<double>(type: "double", nullable: false),
                    Balance = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_GallaId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_galladetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_galladetail_lotusrms_gallas_LotusRMS_GallaId",
                        column: x => x.LotusRMS_GallaId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_gallas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_menudetail",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Rate = table.Column<double>(type: "double", nullable: false),
                    Default = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LotusRMS_MenuId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_menudetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_menudetail_lotusrms_menus_LotusRMS_MenuId",
                        column: x => x.LotusRMS_MenuId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lotusrms_menudetail_lotusrms_unit_division_Quantity",
                        column: x => x.Quantity,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_unit_division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_inventory",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StockQuantity = table.Column<double>(type: "double", nullable: false),
                    ReorderLevel = table.Column<double>(type: "double", nullable: false),
                    IsPurchased = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Remarks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_inventory_lotusrms_products_Product_Id",
                        column: x => x.Product_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_menuincredians",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Unit_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LotusRMS_MenuId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_menuincredians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_menuincredians_lotusrms_menus_LotusRMS_MenuId",
                        column: x => x.LotusRMS_MenuId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lotusrms_menuincredians_lotusrms_products_Product_Id",
                        column: x => x.Product_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_menuincredians_lotusrms_units_Unit_Id",
                        column: x => x.Unit_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_purchasedetail",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Product_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Rate = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_PurchaseId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_purchasedetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_purchasedetail_lotusrms_products_Product_Id",
                        column: x => x.Product_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_purchasedetail_lotusrms_purchases_LotusRMS_Purchase~",
                        column: x => x.LotusRMS_PurchaseId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_purchases",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_checkout",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Customer_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Customer_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Customer_Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Customer_Contact = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Total = table.Column<double>(type: "double", nullable: false),
                    Discount_Type = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    Paid_Amount = table.Column<float>(type: "float", nullable: false),
                    Payment_Mode = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Invoice_No = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_checkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_checkout_lotusrms_orders_Order_Id",
                        column: x => x.Order_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_order_details",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Quantity_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Rate = table.Column<double>(type: "double", nullable: false),
                    Remarks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsComplete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsKitchenComplete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsQrOrder = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsQrVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPrinted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MenuId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LotusRMS_OrderId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_order_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_order_details_lotusrms_menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_order_details_lotusrms_orders_LotusRMS_OrderId",
                        column: x => x.LotusRMS_OrderId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_orders",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_invoices",
                schema: "Identity",
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
                    table.PrimaryKey("PK_lotusrms_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_invoices_lotusrms_billsettings_BillSetting_Id",
                        column: x => x.BillSetting_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_billsettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_invoices_lotusrms_checkout_Checkout_Id",
                        column: x => x.Checkout_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_checkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lotusrms_invoices_lotusrms_fiscalyears_FiscalYear_Id",
                        column: x => x.FiscalYear_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_fiscalyears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lotusrms_duebooks",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DueDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Invoice_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Invoice_Amount = table.Column<double>(type: "double", nullable: false),
                    DueAmount = table.Column<double>(type: "double", nullable: false),
                    PaidAmount = table.Column<double>(type: "double", nullable: false),
                    BalanceDue = table.Column<double>(type: "double", nullable: false),
                    LotusRMS_CustomerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotusrms_duebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lotusrms_duebooks_lotusrms_customers_LotusRMS_CustomerId",
                        column: x => x.LotusRMS_CustomerId,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lotusrms_duebooks_lotusrms_invoices_Invoice_Id",
                        column: x => x.Invoice_Id,
                        principalSchema: "Identity",
                        principalTable: "lotusrms_invoices",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_contactperson_LotusRMS_CompanyId",
                schema: "Identity",
                table: "contactperson",
                column: "LotusRMS_CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_checkout_Order_Id",
                schema: "Identity",
                table: "lotusrms_checkout",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_duebooks_Invoice_Id",
                schema: "Identity",
                table: "lotusrms_duebooks",
                column: "Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_duebooks_LotusRMS_CustomerId",
                schema: "Identity",
                table: "lotusrms_duebooks",
                column: "LotusRMS_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_galladetail_LotusRMS_GallaId",
                schema: "Identity",
                table: "lotusrms_galladetail",
                column: "LotusRMS_GallaId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_gallas_Cashier",
                schema: "Identity",
                table: "lotusrms_gallas",
                column: "Cashier");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_inventory_Product_Id",
                schema: "Identity",
                table: "lotusrms_inventory",
                column: "Product_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_invoices_BillSetting_Id",
                schema: "Identity",
                table: "lotusrms_invoices",
                column: "BillSetting_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_invoices_Checkout_Id",
                schema: "Identity",
                table: "lotusrms_invoices",
                column: "Checkout_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_invoices_FiscalYear_Id",
                schema: "Identity",
                table: "lotusrms_invoices",
                column: "FiscalYear_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menu_categories_Type_Id",
                schema: "Identity",
                table: "lotusrms_menu_categories",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menudetail_LotusRMS_MenuId",
                schema: "Identity",
                table: "lotusrms_menudetail",
                column: "LotusRMS_MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menudetail_Quantity",
                schema: "Identity",
                table: "lotusrms_menudetail",
                column: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menuincredians_LotusRMS_MenuId",
                schema: "Identity",
                table: "lotusrms_menuincredians",
                column: "LotusRMS_MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menuincredians_Product_Id",
                schema: "Identity",
                table: "lotusrms_menuincredians",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menuincredians_Unit_Id",
                schema: "Identity",
                table: "lotusrms_menuincredians",
                column: "Unit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menus_Category_Id",
                schema: "Identity",
                table: "lotusrms_menus",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menus_Type_Id",
                schema: "Identity",
                table: "lotusrms_menus",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_menus_Unit_Id",
                schema: "Identity",
                table: "lotusrms_menus",
                column: "Unit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_order_details_LotusRMS_OrderId",
                schema: "Identity",
                table: "lotusrms_order_details",
                column: "LotusRMS_OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_order_details_MenuId",
                schema: "Identity",
                table: "lotusrms_order_details",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_orders_OrderBy",
                schema: "Identity",
                table: "lotusrms_orders",
                column: "OrderBy");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_orders_Table_Id",
                schema: "Identity",
                table: "lotusrms_orders",
                column: "Table_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_product_categories_Type_Id",
                schema: "Identity",
                table: "lotusrms_product_categories",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_products_Product_Category_Id",
                schema: "Identity",
                table: "lotusrms_products",
                column: "Product_Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_products_Product_Type_Id",
                schema: "Identity",
                table: "lotusrms_products",
                column: "Product_Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_products_Product_Unit_Id",
                schema: "Identity",
                table: "lotusrms_products",
                column: "Product_Unit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_purchasedetail_LotusRMS_PurchaseId",
                schema: "Identity",
                table: "lotusrms_purchasedetail",
                column: "LotusRMS_PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_purchasedetail_Product_Id",
                schema: "Identity",
                table: "lotusrms_purchasedetail",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_purchases_Supplier_Id",
                schema: "Identity",
                table: "lotusrms_purchases",
                column: "Supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_tables_Table_Type_Id",
                schema: "Identity",
                table: "lotusrms_tables",
                column: "Table_Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_lotusrms_unit_division_LotusRMS_Menu_UnitId",
                schema: "Identity",
                table: "lotusrms_unit_division",
                column: "LotusRMS_Menu_UnitId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roleclaims_RoleId",
                schema: "Identity",
                table: "roleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userclaims_UserId",
                schema: "Identity",
                table: "userclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userlogins_UserId",
                schema: "Identity",
                table: "userlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userroles_RoleId",
                schema: "Identity",
                table: "userroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactperson",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_duebooks",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_galladetail",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_inventory",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_menudetail",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_menuincredians",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_order_details",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_purchasedetail",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "roleclaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "userclaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "userlogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "userroles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "usertokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "company",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_customers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_invoices",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_gallas",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_unit_division",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_menus",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_products",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_purchases",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_billsettings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_checkout",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_fiscalyears",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_menu_categories",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_menu_units",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_product_categories",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_units",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_suppliers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_orders",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_menu_types",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_product_types",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_tables",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "users",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "lotusrms_table_types",
                schema: "Identity");
        }
    }
}
