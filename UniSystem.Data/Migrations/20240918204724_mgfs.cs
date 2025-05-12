using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class mgfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectStudents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudents_ProjectId",
                table: "ProjectStudents",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStudents_Projects_ProjectId",
                table: "ProjectStudents",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStudents_Projects_ProjectId",
                table: "ProjectStudents");

            migrationBuilder.DropIndex(
                name: "IX_ProjectStudents_ProjectId",
                table: "ProjectStudents");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectStudents");
        }
    }
}
