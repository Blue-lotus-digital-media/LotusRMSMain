
using LotusRMS.DataAccess.Constants;
using LotusRMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LotusRMS.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<RMSUser,IdentityRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
        public DbSet<LotusRMS_Unit> LotusRMS_Units { get; set; }
        public DbSet<LotusRMS_Product> LotusRMS_Products { get; set; }
        public DbSet<LotusRMS_Product_Category> LotusRMS_Product_Categories { get; set; }
        public DbSet<LotusRMS_Product_Type> LotusRMS_Product_Types { get; set; }

        public DbSet<LotusRMS_Menu_Type> LotusRMS_Menu_Types { get; set; }
        public DbSet<LotusRMS_Menu_Unit> LotusRMS_Menu_Units { get; set; }
        public DbSet<LotusRMS_Menu_Category> LotusRMS_Menu_Categories { get; set; }
        public DbSet<LotusRMS_Table_Type> LotusRMS_Table_Types { get; set; }
        public DbSet<LotusRMS_Table> LotusRMS_Tables { get; set; }
        public DbSet<LotusRMS_Menu> LotusRMS_Menus { get; set; }
        public DbSet<LotusRMS_Order> LotusRMS_Orders { get; set; }
        public DbSet<LotusRMS_FiscalYear> LotusRMS_FiscalYears { get; set; }
        public DbSet<LotusRMS_BillSetting> LotusRMS_BillSettings { get; set; }
        public DbSet<LotusRMS_Company> Company { get; set; }
        public DbSet<LotusRMS_Invoice> LotusRMS_Invoices { get; set; }
        public DbSet<LotusRMS_Customer> LotusRMS_Customers { get; set; }
        public DbSet<LotusRMS_DueBook> LotusRMS_DueBooks { get; set; }
        public DbSet<LotusRMS_Supplier> LotusRMS_Suppliers { get; set; }
        public DbSet<LotusRMS_Purchase> LotusRMS_Purchases { get; set; }
        public DbSet<LotusRMS_Galla> LotusRMS_Gallas { get; set; }

        // Specify DbSet properties etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<RMSUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            // add your own configuration here
            modelBuilder.Entity<RMSUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(100));
            modelBuilder.Entity<RMSUser>(entity => entity.Property(m => m.Id).HasMaxLength(100));
            modelBuilder.Entity<RMSUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(100));


            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(100));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(100));

            //modelBuilder.Entity<AspNetUserLogins>(entity => entity.Property(m => m.Id).HasMaxLength(200));



            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(100));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(100));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(100));
            

            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(100));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(100));


            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(80));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(80));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(80));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(100));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(100));



         
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());
            }
        


    }

    }   
}