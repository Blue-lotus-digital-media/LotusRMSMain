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
    [Migration("20230305074253_MenuAdded")]
    partial class MenuAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
#pragma warning restore 612, 618
        }
    }
}
