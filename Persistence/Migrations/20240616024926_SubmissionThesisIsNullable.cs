using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SubmissionThesisIsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Files_ThesisId",
                table: "Submissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ThesisId",
                table: "Submissions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Files_ThesisId",
                table: "Submissions",
                column: "ThesisId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Files_ThesisId",
                table: "Submissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ThesisId",
                table: "Submissions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Files_ThesisId",
                table: "Submissions",
                column: "ThesisId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
