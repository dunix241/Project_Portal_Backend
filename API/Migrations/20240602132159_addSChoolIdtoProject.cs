using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addSChoolIdtoProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SchoolId",
                table: "Projects",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Schools_SchoolId",
                table: "Projects",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Schools_SchoolId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_SchoolId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Projects");
        }
    }
}
