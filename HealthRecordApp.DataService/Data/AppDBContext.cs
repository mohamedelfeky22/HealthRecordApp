using HealthRecordApp.Entities.DBSets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthRecordApp.DataService.Data
{
    public class AppDBContext : IdentityDbContext
    {

        public virtual DbSet<User>  Users { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
    }
}

