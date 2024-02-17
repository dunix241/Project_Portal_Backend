using Domain;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.School;
using Domain.Student;
using Domain.Project;
using Domain.Semester;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    
    public DbSet<MockDomain> MockDomains { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<ProjectSemester> ProjectSemesters { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProjectSemester>().HasKey(entity => new { entity.ProjectId, entity.SemesterId });
    }
}