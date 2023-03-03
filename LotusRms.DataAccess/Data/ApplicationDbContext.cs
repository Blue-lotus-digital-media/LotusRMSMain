
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

        public object ToList()
        {
            throw new NotImplementedException();
        }
    }   
}