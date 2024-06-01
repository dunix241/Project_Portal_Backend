using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestone_Schools_SchoolId",
                table: "ProjectMilestone");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestoneDetails_ProjectMilestone_ProjectMilestoneId",
                table: "ProjectMilestoneDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMilestone",
                table: "ProjectMilestone");

            migrationBuilder.RenameTable(
                name: "ProjectMilestone",
                newName: "ProjectMilestones");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMilestone_SchoolId",
                table: "ProjectMilestones",
                newName: "IX_ProjectMilestones_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMilestones",
                table: "ProjectMilestones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMilestoneDetails_ProjectMilestones_ProjectMilestoneId",
                table: "ProjectMilestoneDetails",
                column: "ProjectMilestoneId",
                principalTable: "ProjectMilestones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMilestones_Schools_SchoolId",
                table: "ProjectMilestones",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestoneDetails_ProjectMilestones_ProjectMilestoneId",
                table: "ProjectMilestoneDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestones_Schools_SchoolId",
                table: "ProjectMilestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMilestones",
                table: "ProjectMilestones");

            migrationBuilder.RenameTable(
                name: "ProjectMilestones",
                newName: "ProjectMilestone");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMilestones_SchoolId",
                table: "ProjectMilestone",
                newName: "IX_ProjectMilestone_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMilestone",
                table: "ProjectMilestone",
                column: "Id");

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
        }
    }
}
