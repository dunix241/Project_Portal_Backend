using Domain;
using Domain.Enrollment;
using Domain.EnrollmentPlan;
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
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Thesis> Theses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<SubmissionComment> SubmissionComments { get; set; }
    public DbSet<EnrollmentComment> EnrollmentComments { get; set; }
    public DbSet<File.File> Files { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<EnrollmentMember> EnrollmentMembers { get; set; }
    public DbSet<EnrollmentPlan> EnrollmentPlans { get; set; }
    public DbSet<EnrollmentPlanDetails> EnrollmentPlanDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProjectSemester>().HasKey(entity => new { entity.ProjectId, entity.SemesterId });

        builder.Entity<Student>()
            .HasOne(entity => entity.User)
            .WithMany()
            .HasForeignKey(entity => entity.UserId);
        
        builder.Entity<Lecturer>()
            .HasOne(entity => entity.User)
            .WithMany()
            .HasForeignKey(entity => entity.UserId);
        
        builder.Entity<Enrollment>()
            .HasOne(entity => entity.ProjectSemester)
            .WithMany(entity => entity.Enrollments)
            .HasForeignKey(entity => new { entity.ProjectId, entity.SemesterId });
        
        // builder.Entity<EnrollmentPlanDetails>().HasKey(entity =>
        //     new { entity.EnrollmentPlanId, entity.ProjectId, entity.PrerequisiteProjectId });

        builder.Entity<EnrollmentPlanDetails>()
            .HasIndex(nameof(Domain.EnrollmentPlan.EnrollmentPlanDetails.EnrollmentPlanId), nameof(Domain.EnrollmentPlan.EnrollmentPlanDetails.ProjectId), nameof(Domain.EnrollmentPlan.EnrollmentPlanDetails.PrerequisiteProjectId))
            .IsUnique();
        
        builder.Entity<File.File>().HasIndex(x => x.Name).IsUnique();

        builder.Entity<Submission>()
            .HasOne(s => s.Enrollment).WithMany()
            .HasForeignKey(s => s.EnrollmentId);

        builder.Entity<Comment>()
            .HasOne(entity => entity.User)
            .WithMany()
            .HasForeignKey(entity => entity.UserId);
        
        builder.Entity<Submission>()
            .HasOne(s => s.Thesis)
            .WithOne(t => t.Submission)
            .HasForeignKey<Thesis>(t => t.SubmissionId);

        builder.Entity<SubmissionComment>()
            .HasOne(sc => sc.Submission)
            .WithMany(s => s.SubmissionComments)
            .HasForeignKey(sc => sc.SubmissionId);

        builder.Entity<EnrollmentComment>()
            .HasOne(psrc => psrc.Enrollment)
            .WithMany()
            .HasForeignKey(psrc => psrc.EnrollmentId);


    }
}