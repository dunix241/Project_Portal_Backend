using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentMemberKeepsUserIdInsteadOfEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "EnrollmentMembers",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentMembers_UserId",
                table: "EnrollmentMembers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentMembers_AspNetUsers_UserId",
                table: "EnrollmentMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentMembers_AspNetUsers_UserId",
                table: "EnrollmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentMembers_UserId",
                table: "EnrollmentMembers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EnrollmentMembers",
                newName: "Email");
        }
    }
}
