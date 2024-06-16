using Domain;
using Domain.Enrollment;
using Domain.EnrollmentPlan;
using Domain.Lecturer;
using Domain.Project;
using Domain.School;
using Domain.Semester;
using Domain.Student;
using Domain.Submission;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.Seed.DTOs;
using File = Domain.File.File;

namespace Persistence.Seed;

public class Seeder
{
    private static readonly Random _random = new();
    private static DataContext _context;
    private static UserManager<User> _userManager;
    private static RoleManager<IdentityRole> _roleManager;

    private static List<User> _users;
    private static string filePath = "../Persistence/Seed/Data";
    
    public static async Task SeedData(DataContext context, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;

        await SeedUsers();
        await SeedEntitiesFromJson<School>(_context, _context.Schools, "schools.json");
        await SeedEntitiesFromJson<Student>(_context, _context.Students, "students.json");
        await SeedEntitiesFromJson<Lecturer>(_context, _context.Lecturers, "lecturers.json");
        await SeedEntitiesFromJson<Project>(_context, _context.Projects, "projects.json");
        await SeedEntitiesFromJson<Semester>(_context, _context.Semesters, "semesters.json");
        await SeedEntitiesFromJson<ProjectSemester>(_context, _context.ProjectSemesters, "project-semesters.json");
        await SeedEntitiesFromJson<EnrollmentPlan>(_context, _context.EnrollmentPlans, "enrollment-plans.json");
        await SeedEntitiesFromJson<EnrollmentPlanDetails>(_context, _context.EnrollmentPlanDetails, "enrollment-plan-details.json");
        await SeedEntitiesFromJson<Enrollment>(_context, _context.Enrollments, "enrollments.json");
        await SeedEntitiesFromJson<EnrollmentMember>(_context, _context.EnrollmentMembers, "enrollment-members.json");
        await SeedEntitiesFromJson<File>(_context, _context.Files, "files.json");
        await SeedEntitiesFromJson<Submission>(_context, _context.Submissions, "submissions.json");
    }
    
    private static async Task SeedUsers()
    {
        if (!await _context.Users.AnyAsync())
        {
            var users = await JsonToEntities<UserData>("users.json");
            foreach (var userData in users)
            {
                var user = new User
                {
                    Id = userData.Id,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    UserName = userData.UserName,
                    Email = userData.Email,
                    PhoneNumber = userData.PhoneNumber
                };
                await _userManager.CreateAsync(user, userData.Password);

                if (!await _roleManager.RoleExistsAsync(userData.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(userData.Role));
                }

                await _userManager.AddToRoleAsync(user, userData.Role);
            }
        }
    }

    private static async Task SeedEntitiesFromJson<T>(DataContext dataContext, DbSet<T> dbSet, string filePath)
        where T : class
    {
        if (!await dbSet.AnyAsync())
        {
            var entities = await JsonToEntities<T>(filePath);
            dbSet.AddRange(entities);
            await dataContext.SaveChangesAsync();
        }
    }

    private static async Task<List<T>?> JsonToEntities<T>(string fileName)
    {
        var json = await System.IO.File.ReadAllTextAsync($"{filePath}/{fileName}");
        var entities = JsonConvert.DeserializeObject<List<T>>(json);
        return entities;
    }

}