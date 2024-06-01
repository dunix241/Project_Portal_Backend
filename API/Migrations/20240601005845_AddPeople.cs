using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddPeople : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Image_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lecturers_LecturerId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Students_StudentId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Files_LecturerId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_StudentId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "People");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SchoolId",
                table: "People",
                newName: "IX_People_SchoolId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LecturerUserId",
                table: "Files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentUserId",
                table: "Files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "Files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolId",
                table: "People",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "People",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<long>(
                name: "IRN",
                table: "People",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "People",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Student_IsActive",
                table: "People",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Student_SchoolId",
                table: "People",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestones_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEnrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectSemesterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Vision = table.Column<string>(type: "TEXT", nullable: false),
                    Mission = table.Column<string>(type: "TEXT", nullable: false),
                    Feedback = table.Column<string>(type: "TEXT", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Stars = table.Column<int>(type: "INTEGER", nullable: false),
                    ForkedFromProjectId = table.Column<Guid>(type: "TEXT", nullable: true),
                    HeirFortunes = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEnrollments_ProjectSemesters_ProjectSemesterId_OwnerId",
                        columns: x => new { x.ProjectSemesterId, x.OwnerId },
                        principalTable: "ProjectSemesters",
                        principalColumns: new[] { "ProjectId", "SemesterId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectMilestoneId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Prerequisite = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilestoneDetails_Milestones_ProjectMilestoneId",
                        column: x => x.ProjectMilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilestoneDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_ProjectEnrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "ProjectEnrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentBases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PersonId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonUserId = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectSemesterRegistrationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SubmissionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentBases_People_PersonUserId",
                        column: x => x.PersonUserId,
                        principalTable: "People",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentBases_ProjectEnrollments_ProjectSemesterRegistrationId",
                        column: x => x.ProjectSemesterRegistrationId,
                        principalTable: "ProjectEnrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentBases_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_LecturerUserId",
                table: "Files",
                column: "LecturerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_StudentUserId",
                table: "Files",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_SubmissionId",
                table: "Files",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Student_SchoolId",
                table: "People",
                column: "Student_SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBases_PersonUserId",
                table: "CommentBases",
                column: "PersonUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBases_ProjectSemesterRegistrationId",
                table: "CommentBases",
                column: "ProjectSemesterRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBases_SubmissionId",
                table: "CommentBases",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneDetails_ProjectId",
                table: "MilestoneDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneDetails_ProjectMilestoneId",
                table: "MilestoneDetails",
                column: "ProjectMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_SchoolId",
                table: "Milestones",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnrollments_ProjectSemesterId_OwnerId",
                table: "ProjectEnrollments",
                columns: new[] { "ProjectSemesterId", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_EnrollmentId",
                table: "Submissions",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_People_LecturerUserId",
                table: "Files",
                column: "LecturerUserId",
                principalTable: "People",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_People_StudentUserId",
                table: "Files",
                column: "StudentUserId",
                principalTable: "People",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Submissions_SubmissionId",
                table: "Files",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Schools_SchoolId",
                table: "People",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Schools_Student_SchoolId",
                table: "People",
                column: "Student_SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_People_LecturerUserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_People_StudentUserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Submissions_SubmissionId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Schools_SchoolId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Schools_Student_SchoolId",
                table: "People");

            migrationBuilder.DropTable(
                name: "CommentBases");

            migrationBuilder.DropTable(
                name: "MilestoneDetails");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "ProjectEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_Files_LecturerUserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_StudentUserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_SubmissionId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_Student_SchoolId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "LecturerUserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Student_IsActive",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Student_SchoolId",
                table: "People");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_People_SchoolId",
                table: "Students",
                newName: "IX_Students_SchoolId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SchoolId",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IRN",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SchoolId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_LecturerId",
                table: "Files",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_StudentId",
                table: "Files",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_SchoolId",
                table: "Lecturers",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Image_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Lecturers_LecturerId",
                table: "Files",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Students_StudentId",
                table: "Files",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
