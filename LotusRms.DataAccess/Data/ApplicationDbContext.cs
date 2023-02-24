
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

        public object ToList()
        {
            throw new NotImplementedException();
        }
    }   
}