using Domain;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.School;
using Domain.Student;
using Domain.Project;
using Domain.Semester;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Domain.File;
using Domain.Comment;
using Domain.Submission;
using Domain.Thesis;
using Domain.Person;
using System.Reflection.Emit;
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
    public DbSet<ProjectEnrollment> ProjectEnrollments { get; set; }

    public DbSet<File.File> Files { get; set; }

    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Thesis> Theses { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<CommentBase> CommentBases { get; set; }
    public DbSet<SubmissionComment> SubmissionComments { get; set; }
    public DbSet<ProjectSemesterRegistrationComment> ProjectSemesterRegistrationComments { get; set; }
    public DbSet<ProjectMilestone> ProjectMilestones { get; set; }
    public DbSet<ProjectMilestoneDetails> ProjectMilestoneDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProjectSemester>().HasKey(entity => new { entity.ProjectId, entity.SemesterId });
        builder.Entity<File.File>().HasIndex(x => x.FileName).IsUnique();
        builder.Entity<ProjectEnrollment>()
             .HasOne(pe => pe.ProjectSemester)
             .WithMany()
             .HasForeignKey(pe => new { pe.ProjectSemesterId, pe.OwnerId });


        builder.Entity<ProjectEnrollment>()
            .Property(pe => pe.Tags)
            .HasConversion(
                v => string.Join(',', v), // Convert List<string> to string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert string back to List<string>
            )
            .HasColumnType("TEXT");

        builder.Entity<Submission>()
            .HasOne(s => s.Enrollment).WithMany()
            .HasForeignKey(s => s.EnrollmentId);


        builder.Entity<Submission>()
            .HasOne(s => s.Thesis)
            .WithOne(t => t.Submission)
            .HasForeignKey<Thesis>(t => t.SubmissionId);

        builder.Entity<SubmissionComment>()
            .HasOne(sc => sc.Submission)
            .WithMany(s => s.SubmissionComments)
            .HasForeignKey(sc => sc.SubmissionId);

        builder.Entity<ProjectSemesterRegistrationComment>()
            .HasOne(psrc => psrc.ProjectSemesterRegistration)
            .WithMany()
            .HasForeignKey(psrc => psrc.ProjectSemesterRegistrationId);


    }
}