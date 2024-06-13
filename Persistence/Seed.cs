using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace Persistence;

public class Seed
{
    private static readonly Random _random = new();
    private static DataContext _context;
    private static UserManager<User> _userManager;
    private static RoleManager<IdentityRole> _roleManager;

    private static List<User> _users;

    public static async Task SeedData(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // if (userManager.Users.Any()) return;

        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;

        SeedUsers();
        CreateUsers();

        await context.SaveChangesAsync();
    }

    public static void SeedUsers()
    {
        _users = new List<User>
        {
            new()
            {
                FirstName = "Admin",
                UserName = "admin@project-portal.com",
                Email = "admin@project-portal.com",
                Address = "1 at fake street"
            },
            new()
            {
                FirstName = "Amos Blanda",
                UserName = "amos",
                Email = "amos@test.com",
                Address = "1 at fake street"
            },
            new()
            {
                FirstName = "Brent Goodwin",
                UserName = "brent",
                Email = "brent@test.com",
                Address = "2 at fake street"
            },
            new()
            {
                FirstName = "Carol Koss",
                UserName = "carol",
                Email = "carol@test.com",
                Address = "3 at fake street"
            }
        };
    }

    public static async void CreateUsers()
    {
        foreach (var user in _users)
        {
            await _userManager.CreateAsync(user, "Password_123");
        }
        
        var admin = await _userManager.FindByEmailAsync("admin@project-portal.com");
        var superAdminRole = "SuperAdmins";
        if (!await _roleManager.RoleExistsAsync(superAdminRole))
        {
            await _roleManager.CreateAsync(new IdentityRole(superAdminRole));
        }
        await _userManager.AddToRoleAsync(admin, superAdminRole);

        await _context.SaveChangesAsync();
    }
    
}