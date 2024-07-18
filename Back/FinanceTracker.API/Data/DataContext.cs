using FinanceTracker.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
