using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addThesisToEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ThesisId",
                table: "Enrollments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ThesisId",
                table: "Enrollments",
                column: "ThesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Files_ThesisId",
                table: "Enrollments",
                column: "ThesisId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Files_ThesisId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_ThesisId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Enrollments");
        }
    }
}
