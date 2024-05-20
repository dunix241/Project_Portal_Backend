using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addFileType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Projects_ProjectId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ImgId",
                table: "Files");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Files",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "FileType",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Projects_ProjectId",
                table: "Files",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Projects_ProjectId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgId",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Projects_ProjectId",
                table: "Files",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
