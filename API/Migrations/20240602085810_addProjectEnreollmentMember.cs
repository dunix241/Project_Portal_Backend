using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addProjectEnreollmentMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnrollments_ProjectSemesters_ProjectSemesterId_OwnerId",
                table: "ProjectEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEnrollments_ProjectSemesterId_OwnerId",
                table: "ProjectEnrollments");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "ProjectEnrollments",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectSemesterProjectId",
                table: "ProjectEnrollments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectSemesterSemesterId",
                table: "ProjectEnrollments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProjectEnrollmentMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: true),
                    RejectReason = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectEnrollmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectEnrollmentId1 = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEnrollmentMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEnrollmentMembers_ProjectEnrollments_ProjectEnrollmentId1",
                        column: x => x.ProjectEnrollmentId1,
                        principalTable: "ProjectEnrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnrollments_ProjectSemesterProjectId_ProjectSemesterSemesterId",
                table: "ProjectEnrollments",
                columns: new[] { "ProjectSemesterProjectId", "ProjectSemesterSemesterId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnrollmentMembers_ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers",
                column: "ProjectEnrollmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnrollments_ProjectSemesters_ProjectSemesterProjectId_ProjectSemesterSemesterId",
                table: "ProjectEnrollments",
                columns: new[] { "ProjectSemesterProjectId", "ProjectSemesterSemesterId" },
                principalTable: "ProjectSemesters",
                principalColumns: new[] { "ProjectId", "SemesterId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnrollments_ProjectSemesters_ProjectSemesterProjectId_ProjectSemesterSemesterId",
                table: "ProjectEnrollments");

            migrationBuilder.DropTable(
                name: "ProjectEnrollmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEnrollments_ProjectSemesterProjectId_ProjectSemesterSemesterId",
                table: "ProjectEnrollments");

            migrationBuilder.DropColumn(
                name: "ProjectSemesterProjectId",
                table: "ProjectEnrollments");

            migrationBuilder.DropColumn(
                name: "ProjectSemesterSemesterId",
                table: "ProjectEnrollments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectEnrollments",
                newName: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnrollments_ProjectSemesterId_OwnerId",
                table: "ProjectEnrollments",
                columns: new[] { "ProjectSemesterId", "OwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnrollments_ProjectSemesters_ProjectSemesterId_OwnerId",
                table: "ProjectEnrollments",
                columns: new[] { "ProjectSemesterId", "OwnerId" },
                principalTable: "ProjectSemesters",
                principalColumns: new[] { "ProjectId", "SemesterId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
