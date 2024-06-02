using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updateProjectEnreollmentMember2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnrollmentMembers_ProjectEnrollments_ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEnrollmentMembers_ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers");

            migrationBuilder.DropColumn(
                name: "ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectEnrollmentId",
                table: "ProjectEnrollmentMembers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnrollmentMembers_ProjectEnrollmentId",
                table: "ProjectEnrollmentMembers",
                column: "ProjectEnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnrollmentMembers_ProjectEnrollments_ProjectEnrollmentId",
                table: "ProjectEnrollmentMembers",
                column: "ProjectEnrollmentId",
                principalTable: "ProjectEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEnrollmentMembers_ProjectEnrollments_ProjectEnrollmentId",
                table: "ProjectEnrollmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Name",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectEnrollmentMembers_ProjectEnrollmentId",
                table: "ProjectEnrollmentMembers");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectEnrollmentId",
                table: "ProjectEnrollmentMembers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEnrollmentMembers_ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers",
                column: "ProjectEnrollmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEnrollmentMembers_ProjectEnrollments_ProjectEnrollmentId1",
                table: "ProjectEnrollmentMembers",
                column: "ProjectEnrollmentId1",
                principalTable: "ProjectEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
