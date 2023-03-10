
using LotusRMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LotusRMS.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<RMSUser>
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

       
    }   
}