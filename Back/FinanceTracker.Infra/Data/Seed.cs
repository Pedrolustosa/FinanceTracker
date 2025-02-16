﻿using FinanceTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FinanceTracker.Infra.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
        if (users == null) return;
        var roles = new List<AppRole> { 
            new() { Name = "Admin" },
            new() { Name = "Member" },
            new() { Name = "Finance" },
            new() { Name = "Moderator" }
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        foreach (var user in users)
        {
            user.UserName = user.UserName!.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");
        };

        var admin = new AppUser
        {
            UserName = "admin",
            KnownAs = "admin",
            Gender = "",
            City = "",
            Country = ""
        };

        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, ["Admin", "Finance", "Moderator"]);
    }
}
