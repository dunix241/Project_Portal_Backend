using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAvatarId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId1",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId1",
                table: "AspNetUsers",
                column: "AvatarId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId1",
                table: "AspNetUsers",
                column: "AvatarId1",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarId1",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
