﻿// <auto-generated />
using System;
using LotusRMS.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LotusRMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230315161017_CompanyUpdated")]
    partial class CompanyUpdated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LotusRMS.Models.ContactPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("LotusRMS_CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("LotusRMS_CompanyId");

                    b.ToTable("ContactPerson");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyRegistrationNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ContractDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PanOrVat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RegistrationDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RegistrationNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ServiceStartDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Tole")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ValidTill")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_FiscalYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("EndDateAD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EndDateBS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StartDateAD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StartDateBS")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("LotusRMS_FiscalYears");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Category_Id")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Item_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OrderTo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Rate")
                        .HasColumnType("float");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Type_Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Unit_Id")
                        .HasColumnType("char(36)");

                    b.Property<float>("Unit_Quantity")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Category_Id");

                    b.HasIndex("Type_Id");

                    b.HasIndex("Unit_Id");

                    b.ToTable("LotusRMS_Menus");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Menu_Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Category_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Type_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Type_Id");

                    b.ToTable("LotusRMS_Menu_Categories");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Menu_Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Type_Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Type_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LotusRMS_Menu_Types");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Menu_Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Unit_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Unit_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Unit_Symbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LotusRMS_Menu_Units");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Discount")
                        .HasColumnType("float");

                    b.Property<bool>("IsCheckout")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OrderBy")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Order_No")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Table_Id")
                        .HasColumnType("char(36)");

                    b.Property<float>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderBy");

                    b.HasIndex("Table_Id");

                    b.ToTable("LotusRMS_Orders");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Order_Details", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float>("GetTotal")
                        .HasColumnType("float");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsKitchenComplete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsQrOrder")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsQrVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LotusRMS_OrderId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("char(36)");

                    b.Property<float>("Quantity")
                        .HasColumnType("float");

                    b.Property<float>("Rate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("LotusRMS_OrderId");

                    b.HasIndex("MenuId");

                    b.ToTable("LotusRMS_Order_Details");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Product_Category_Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Product_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Product_Type_Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Product_Unit_Id")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<float?>("Unit_Quantity")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Product_Category_Id");

                    b.HasIndex("Product_Type_Id");

                    b.HasIndex("Product_Unit_Id");

                    b.ToTable("LotusRMS_Products");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Product_Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Category_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Type_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Type_Id");

                    b.ToTable("LotusRMS_Product_Categories");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Product_Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Type_Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Type_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LotusRMS_Product_Types");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("No_Of_Chair")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Table_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Table_No")
                        .HasColumnType("int");

                    b.Property<Guid>("Table_Type_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Table_Type_Id");

                    b.ToTable("LotusRMS_Tables");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Table_Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Type_Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Type_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LotusRMS_Table_Types");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Unit_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Unit_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Unit_Symbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LotusRMS_Units");
                });

            modelBuilder.Entity("LotusRMS.Models.RMSUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LotusRMS.Models.ContactPerson", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Company", null)
                        .WithMany("ContactPersons")
                        .HasForeignKey("LotusRMS_CompanyId");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Menu", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Menu_Category", "Menu_Category")
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotusRMS.Models.LotusRMS_Menu_Type", "Menu_Type")
                        .WithMany()
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotusRMS.Models.LotusRMS_Menu_Unit", "Menu_Unit")
                        .WithMany()
                        .HasForeignKey("Unit_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu_Category");

                    b.Navigation("Menu_Type");

                    b.Navigation("Menu_Unit");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Menu_Category", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Menu_Type", "Product_Type")
                        .WithMany()
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product_Type");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Order", b =>
                {
                    b.HasOne("LotusRMS.Models.RMSUser", "User")
                        .WithMany()
                        .HasForeignKey("OrderBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotusRMS.Models.LotusRMS_Table", "Table")
                        .WithMany()
                        .HasForeignKey("Table_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Order_Details", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Order", null)
                        .WithMany("Order_Details")
                        .HasForeignKey("LotusRMS_OrderId");

                    b.HasOne("LotusRMS.Models.LotusRMS_Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Product", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Product_Category", "Product_Category")
                        .WithMany()
                        .HasForeignKey("Product_Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotusRMS.Models.LotusRMS_Product_Type", "Product_Type")
                        .WithMany()
                        .HasForeignKey("Product_Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotusRMS.Models.LotusRMS_Unit", "Product_Unit")
                        .WithMany()
                        .HasForeignKey("Product_Unit_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product_Category");

                    b.Navigation("Product_Type");

                    b.Navigation("Product_Unit");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Product_Category", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Product_Type", "Product_Type")
                        .WithMany()
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product_Type");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Table", b =>
                {
                    b.HasOne("LotusRMS.Models.LotusRMS_Table_Type", "Table_Type")
                        .WithMany()
                        .HasForeignKey("Table_Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table_Type");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LotusRMS.Models.RMSUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LotusRMS.Models.RMSUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LotusRMS.Models.RMSUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LotusRMS.Models.RMSUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Company", b =>
                {
                    b.Navigation("ContactPersons");
                });

            modelBuilder.Entity("LotusRMS.Models.LotusRMS_Order", b =>
                {
                    b.Navigation("Order_Details");
                });
#pragma warning restore 612, 618
        }
    }
}
