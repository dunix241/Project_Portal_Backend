using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBases_AspNetUsers_UserId",
                table: "CommentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentBases_Enrollments_EnrollmentId",
                table: "CommentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentBases_Submissions_SubmissionId",
                table: "CommentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPlanDetailsEnumerable_EnrollmentPlans_EnrollmentPlanId",
                table: "EnrollmentPlanDetailsEnumerable");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPlanDetailsEnumerable_Projects_PrerequisiteProjectId",
                table: "EnrollmentPlanDetailsEnumerable");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPlanDetailsEnumerable_Projects_ProjectId",
                table: "EnrollmentPlanDetailsEnumerable");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentPlanDetailsEnumerable",
                table: "EnrollmentPlanDetailsEnumerable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentBases",
                table: "CommentBases");

            migrationBuilder.RenameTable(
                name: "EnrollmentPlanDetailsEnumerable",
                newName: "EnrollmentPlanDetails");

            migrationBuilder.RenameTable(
                name: "CommentBases",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentPlanDetailsEnumerable_ProjectId",
                table: "EnrollmentPlanDetails",
                newName: "IX_EnrollmentPlanDetails_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentPlanDetailsEnumerable_PrerequisiteProjectId",
                table: "EnrollmentPlanDetails",
                newName: "IX_EnrollmentPlanDetails_PrerequisiteProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentPlanDetailsEnumerable_EnrollmentPlanId_ProjectId_PrerequisiteProjectId",
                table: "EnrollmentPlanDetails",
                newName: "IX_EnrollmentPlanDetails_EnrollmentPlanId_ProjectId_PrerequisiteProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentBases_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentBases_SubmissionId",
                table: "Comments",
                newName: "IX_Comments_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentBases_EnrollmentId",
                table: "Comments",
                newName: "IX_Comments_EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentPlanDetails",
                table: "EnrollmentPlanDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Enrollments_EnrollmentId",
                table: "Comments",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Submissions_SubmissionId",
                table: "Comments",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPlanDetails_EnrollmentPlans_EnrollmentPlanId",
                table: "EnrollmentPlanDetails",
                column: "EnrollmentPlanId",
                principalTable: "EnrollmentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPlanDetails_Projects_PrerequisiteProjectId",
                table: "EnrollmentPlanDetails",
                column: "PrerequisiteProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPlanDetails_Projects_ProjectId",
                table: "EnrollmentPlanDetails",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Enrollments_EnrollmentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Submissions_SubmissionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPlanDetails_EnrollmentPlans_EnrollmentPlanId",
                table: "EnrollmentPlanDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPlanDetails_Projects_PrerequisiteProjectId",
                table: "EnrollmentPlanDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPlanDetails_Projects_ProjectId",
                table: "EnrollmentPlanDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentPlanDetails",
                table: "EnrollmentPlanDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "EnrollmentPlanDetails",
                newName: "EnrollmentPlanDetailsEnumerable");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "CommentBases");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentPlanDetails_ProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                newName: "IX_EnrollmentPlanDetailsEnumerable_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentPlanDetails_PrerequisiteProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                newName: "IX_EnrollmentPlanDetailsEnumerable_PrerequisiteProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentPlanDetails_EnrollmentPlanId_ProjectId_PrerequisiteProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                newName: "IX_EnrollmentPlanDetailsEnumerable_EnrollmentPlanId_ProjectId_PrerequisiteProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "CommentBases",
                newName: "IX_CommentBases_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SubmissionId",
                table: "CommentBases",
                newName: "IX_CommentBases_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_EnrollmentId",
                table: "CommentBases",
                newName: "IX_CommentBases_EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentPlanDetailsEnumerable",
                table: "EnrollmentPlanDetailsEnumerable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentBases",
                table: "CommentBases",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Extension = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBases_AspNetUsers_UserId",
                table: "CommentBases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBases_Enrollments_EnrollmentId",
                table: "CommentBases",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBases_Submissions_SubmissionId",
                table: "CommentBases",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPlanDetailsEnumerable_EnrollmentPlans_EnrollmentPlanId",
                table: "EnrollmentPlanDetailsEnumerable",
                column: "EnrollmentPlanId",
                principalTable: "EnrollmentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPlanDetailsEnumerable_Projects_PrerequisiteProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                column: "PrerequisiteProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPlanDetailsEnumerable_Projects_ProjectId",
                table: "EnrollmentPlanDetailsEnumerable",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
