using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class mgaddingforproje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStudents_AspNetUsers_AppUserId1",
                table: "ProjectStudents");

            migrationBuilder.DropIndex(
                name: "IX_ProjectStudents_AppUserId1",
                table: "ProjectStudents");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "ProjectStudents");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "ProjectStudents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudents_AppUserId",
                table: "ProjectStudents",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStudents_AspNetUsers_AppUserId",
                table: "ProjectStudents",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStudents_AspNetUsers_AppUserId",
                table: "ProjectStudents");

            migrationBuilder.DropIndex(
                name: "IX_ProjectStudents_AppUserId",
                table: "ProjectStudents");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "ProjectStudents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "ProjectStudents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudents_AppUserId1",
                table: "ProjectStudents",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStudents_AspNetUsers_AppUserId1",
                table: "ProjectStudents",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
