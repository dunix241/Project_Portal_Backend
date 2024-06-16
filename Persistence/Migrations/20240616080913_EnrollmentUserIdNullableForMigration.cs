using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentUserIdNullableForMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentMembers_AspNetUsers_UserId",
                table: "EnrollmentMembers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "EnrollmentMembers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentMembers_AspNetUsers_UserId",
                table: "EnrollmentMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentMembers_AspNetUsers_UserId",
                table: "EnrollmentMembers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "EnrollmentMembers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentMembers_AspNetUsers_UserId",
                table: "EnrollmentMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
