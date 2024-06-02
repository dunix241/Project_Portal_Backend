using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updatemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Semesters");

            migrationBuilder.RenameColumn(
                name: "StartRegistrationDate",
                table: "Semesters",
                newName: "RegisterTo");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Semesters",
                newName: "RegisterFrom");

            migrationBuilder.RenameColumn(
                name: "EndRegistrationDate",
                table: "Semesters",
                newName: "DueDate");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProjectSemesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectSemesters");

            migrationBuilder.RenameColumn(
                name: "RegisterTo",
                table: "Semesters",
                newName: "StartRegistrationDate");

            migrationBuilder.RenameColumn(
                name: "RegisterFrom",
                table: "Semesters",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Semesters",
                newName: "EndRegistrationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Semesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
