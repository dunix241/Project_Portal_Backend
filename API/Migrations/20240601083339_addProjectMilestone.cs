using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addProjectMilestone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBases_People_PersonUserId",
                table: "CommentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_People_LecturerUserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_People_StudentUserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneDetails_Milestones_ProjectMilestoneId",
                table: "MilestoneDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MilestoneDetails_Projects_ProjectId",
                table: "MilestoneDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Schools_SchoolId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Schools_SchoolId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Schools_Student_SchoolId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Files_LecturerUserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_StudentUserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_CommentBases_PersonUserId",
                table: "CommentBases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MilestoneDetails",
                table: "MilestoneDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_Student_SchoolId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LecturerUserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "CommentBases");

            migrationBuilder.DropColumn(
                name: "PersonUserId",
                table: "CommentBases");

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
                name: "Milestones",
                newName: "ProjectMilestone");

            migrationBuilder.RenameTable(
                name: "MilestoneDetails",
                newName: "ProjectMilestoneDetails");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_Milestones_SchoolId",
                table: "ProjectMilestone",
                newName: "IX_ProjectMilestone_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_MilestoneDetails_ProjectMilestoneId",
                table: "ProjectMilestoneDetails",
                newName: "IX_ProjectMilestoneDetails_ProjectMilestoneId");

            migrationBuilder.RenameIndex(
                name: "IX_MilestoneDetails_ProjectId",
                table: "ProjectMilestoneDetails",
                newName: "IX_ProjectMilestoneDetails_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_People_SchoolId",
                table: "Students",
                newName: "IX_Students_SchoolId");

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "CommentBases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "CommentBases",
                type: "TEXT",
                nullable: true);

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
                name: "PK_ProjectMilestone",
                table: "ProjectMilestone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMilestoneDetails",
                table: "ProjectMilestoneDetails",
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
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    SchoolId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "IX_CommentBases_LecturerId",
                table: "CommentBases",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBases_StudentId",
                table: "CommentBases",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_SchoolId",
                table: "Lecturers",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBases_Lecturers_LecturerId",
                table: "CommentBases",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBases_Students_StudentId",
                table: "CommentBases",
                column: "StudentId",
                principalTable: "Students",
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
                name: "FK_ProjectMilestone_Schools_SchoolId",
                table: "ProjectMilestone",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMilestoneDetails_ProjectMilestone_ProjectMilestoneId",
                table: "ProjectMilestoneDetails",
                column: "ProjectMilestoneId",
                principalTable: "ProjectMilestone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMilestoneDetails_Projects_ProjectId",
                table: "ProjectMilestoneDetails",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBases_Lecturers_LecturerId",
                table: "CommentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentBases_Students_StudentId",
                table: "CommentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lecturers_LecturerId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Students_StudentId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestone_Schools_SchoolId",
                table: "ProjectMilestone");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestoneDetails_ProjectMilestone_ProjectMilestoneId",
                table: "ProjectMilestoneDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestoneDetails_Projects_ProjectId",
                table: "ProjectMilestoneDetails");

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

            migrationBuilder.DropIndex(
                name: "IX_CommentBases_LecturerId",
                table: "CommentBases");

            migrationBuilder.DropIndex(
                name: "IX_CommentBases_StudentId",
                table: "CommentBases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMilestoneDetails",
                table: "ProjectMilestoneDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMilestone",
                table: "ProjectMilestone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "CommentBases");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CommentBases");

            migrationBuilder.RenameTable(
                name: "ProjectMilestoneDetails",
                newName: "MilestoneDetails");

            migrationBuilder.RenameTable(
                name: "ProjectMilestone",
                newName: "Milestones");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "People");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMilestoneDetails_ProjectMilestoneId",
                table: "MilestoneDetails",
                newName: "IX_MilestoneDetails_ProjectMilestoneId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMilestoneDetails_ProjectId",
                table: "MilestoneDetails",
                newName: "IX_MilestoneDetails_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMilestone_SchoolId",
                table: "Milestones",
                newName: "IX_Milestones_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SchoolId",
                table: "People",
                newName: "IX_People_SchoolId");

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
                name: "PersonId",
                table: "CommentBases",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PersonUserId",
                table: "CommentBases",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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
                name: "PK_MilestoneDetails",
                table: "MilestoneDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Milestones",
                table: "Milestones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_LecturerUserId",
                table: "Files",
                column: "LecturerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_StudentUserId",
                table: "Files",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBases_PersonUserId",
                table: "CommentBases",
                column: "PersonUserId");

            migrationBuilder.CreateIndex(
                name: "IX_People_Student_SchoolId",
                table: "People",
                column: "Student_SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBases_People_PersonUserId",
                table: "CommentBases",
                column: "PersonUserId",
                principalTable: "People",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_MilestoneDetails_Milestones_ProjectMilestoneId",
                table: "MilestoneDetails",
                column: "ProjectMilestoneId",
                principalTable: "Milestones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilestoneDetails_Projects_ProjectId",
                table: "MilestoneDetails",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Schools_SchoolId",
                table: "Milestones",
                column: "SchoolId",
                principalTable: "Schools",
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
    }
}
