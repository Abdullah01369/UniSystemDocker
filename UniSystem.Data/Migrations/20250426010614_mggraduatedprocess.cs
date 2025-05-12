using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class mggraduatedprocess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradutedStatusStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsGpaGreatherThanTwo = table.Column<bool>(type: "bit", nullable: false),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPassedAllCourse = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ISOkeyIntern = table.Column<bool>(type: "bit", nullable: false),
                    CreditDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditGreather240 = table.Column<bool>(type: "bit", nullable: false),
                    AllExamsScoreEntered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradutedStatusStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradutedStatusStudents_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradutedStatusStudents_AppUserId",
                table: "GradutedStatusStudents",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradutedStatusStudents");
        }
    }
}
