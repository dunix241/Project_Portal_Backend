using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionForeignKeyToThesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Submissions_SubmissionId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_SubmissionId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "ThesisId",
                table: "Submissions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ThesisId",
                table: "Submissions",
                column: "ThesisId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Files_ThesisId",
                table: "Submissions",
                column: "ThesisId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Files_ThesisId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ThesisId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Submissions");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "Files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_SubmissionId",
                table: "Files",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Submissions_SubmissionId",
                table: "Files",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
