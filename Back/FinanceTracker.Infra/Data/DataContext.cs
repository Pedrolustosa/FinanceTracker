using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Infra.Data;

public class DataContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, int, 
    IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, 
    IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        modelBuilder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId)
            .IsRequired();
    }
}
