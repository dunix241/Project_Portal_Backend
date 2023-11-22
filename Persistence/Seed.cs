using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class Seed
{
    private static readonly Random _random = new();
    private static DataContext _context;
    private static UserManager<User> _userManager;

    private static List<Image> _images;
    private static List<Image> _avatars;

    private static List<User> _users;

    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        if (userManager.Users.Any()) return;

        _context = context;
        _userManager = userManager;

        _avatars = SeedImages("/assets/images/avatars", "avatar", 24);

        SeedUsers();
        SeedAvatars();
        CreateUsers();

        context.AttachRange(_images);

        await context.SaveChangesAsync();
    }

    public static List<Image> SeedImages(string url, string name, int total)
    {
        var list = new List<Image>();
        for (var i = 1; i <= total; i++)
            list.Add(new Image
            {
                Url = url,
                Name = name + "_" + i,
                Extension = "jpg"
            });

        _images ??= new List<Image>();
        _images.AddRange(list);
        return list;
    }

    public static void SeedUsers()
    {
        _users = new List<User>
        {
            new()
            {
                Name = "Amos Blanda",
                UserName = "amos",
                Email = "amos@test.com",
                Address = "1 at fake street"
            },
            new()
            {
                Name = "Brent Goodwin",
                UserName = "brent",
                Email = "brent@test.com",
                Address = "2 at fake street"
            },
            new()
            {
                Name = "Carol Koss",
                UserName = "carol",
                Email = "carol@test.com",
                Address = "3 at fake street"
            }
        };
    }

    public static void SeedAvatars()
    {
        for (int i = 0, len = _users.Count; i < len; i++) _users[i].Avatar = _avatars[i + 1];
    }

    public static async void CreateUsers()
    {
        foreach (var user in _users)
        {
            await _userManager.CreateAsync(user, "Password_123");
        }

        await _context.SaveChangesAsync();
    }
    
}