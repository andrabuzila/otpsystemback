using Microsoft.EntityFrameworkCore;
using otpsystemback.Data.Entities;

namespace otpsystemback.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
    }
}
