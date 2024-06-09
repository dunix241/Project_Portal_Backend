using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Lecturers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lecturers",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<long>(
                name: "IRN",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lecturers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "EnrollmentPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentPlans_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SemesterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Vision = table.Column<string>(type: "TEXT", nullable: true),
                    HeirFortunes = table.Column<string>(type: "TEXT", nullable: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ForkedFromId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ForkFromId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Enrollments_ForkFromId",
                        column: x => x.ForkFromId,
                        principalTable: "Enrollments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Enrollments_ProjectSemesters_ProjectId_SemesterId",
                        columns: x => new { x.ProjectId, x.SemesterId },
                        principalTable: "ProjectSemesters",
                        principalColumns: new[] { "ProjectId", "SemesterId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FileOriginalName = table.Column<string>(type: "TEXT", nullable: false),
                    FileType = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StudentUserId = table.Column<string>(type: "TEXT", nullable: true),
                    LecturerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LecturerUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Lecturers_LecturerUserId",
                        column: x => x.LecturerUserId,
                        principalTable: "Lecturers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Files_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Files_Students_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Students",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentPlanDetailsEnumerable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnrollmentPlanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PrerequisiteProjectId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentPlanDetailsEnumerable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentPlanDetailsEnumerable_EnrollmentPlans_EnrollmentPlanId",
                        column: x => x.EnrollmentPlanId,
                        principalTable: "EnrollmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentPlanDetailsEnumerable_Projects_PrerequisiteProjectId",
                        column: x => x.PrerequisiteProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentPlanDetailsEnumerable_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsAccepted = table.Column<bool>(type: "INTEGER", nullable: true),
                    RejectReason = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentMembers_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId1",
                table: "Students",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_UserId1",
                table: "Lecturers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentMembers_EnrollmentId",
                table: "EnrollmentMembers",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPlanDetailsEnumerable_EnrollmentPlanId_ProjectId_PrerequisiteProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                columns: new[] { "EnrollmentPlanId", "ProjectId", "PrerequisiteProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPlanDetailsEnumerable_PrerequisiteProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                column: "PrerequisiteProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPlanDetailsEnumerable_ProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPlans_SchoolId",
                table: "EnrollmentPlans",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ForkFromId",
                table: "Enrollments",
                column: "ForkFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_OwnerId",
                table: "Enrollments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ProjectId_SemesterId",
                table: "Enrollments",
                columns: new[] { "ProjectId", "SemesterId" });

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileName",
                table: "Files",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_LecturerUserId",
                table: "Files",
                column: "LecturerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ProjectId",
                table: "Files",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_StudentUserId",
                table: "Files",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_AspNetUsers_UserId1",
                table: "Lecturers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId1",
                table: "Students",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_AspNetUsers_UserId1",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId1",
                table: "Students");

            migrationBuilder.DropTable(
                name: "EnrollmentMembers");

            migrationBuilder.DropTable(
                name: "EnrollmentPlanDetailsEnumerable");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "EnrollmentPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId1",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_UserId1",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IRN",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Lecturers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lecturers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Lecturers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Lecturers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Lecturers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers",
                column: "Id");
        }
    }
}
