using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class refactorDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Image_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_File_Lecturers_LecturerId",
                table: "File");

            migrationBuilder.DropForeignKey(
                name: "FK_File_Projects_ProjectId",
                table: "File");

            migrationBuilder.DropForeignKey(
                name: "FK_File_Students_StudentId",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_File_StudentId",
                table: "Files",
                newName: "IX_Files_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_File_ProjectId",
                table: "Files",
                newName: "IX_Files_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_File_LecturerId",
                table: "Files",
                newName: "IX_Files_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_File_FileName",
                table: "Files",
                newName: "IX_Files_FileName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Lecturers_LecturerId",
                table: "Files",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Projects_ProjectId",
                table: "Files",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Students_StudentId",
                table: "Files",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lecturers_LecturerId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Projects_ProjectId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Students_StudentId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.RenameIndex(
                name: "IX_Files_StudentId",
                table: "File",
                newName: "IX_File_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ProjectId",
                table: "File",
                newName: "IX_File_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_LecturerId",
                table: "File",
                newName: "IX_File_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_FileName",
                table: "File",
                newName: "IX_File_FileName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Image_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Lecturers_LecturerId",
                table: "File",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Projects_ProjectId",
                table: "File",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Students_StudentId",
                table: "File",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
