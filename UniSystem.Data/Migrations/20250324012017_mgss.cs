using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class mgss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researchers_ResearcherEducationInformations_ResearcherEducationInformationId",
                table: "Researchers");

            migrationBuilder.DropIndex(
                name: "IX_Researchers_ResearcherEducationInformationId",
                table: "Researchers");

            migrationBuilder.RenameColumn(
                name: "DateBegEnd",
                table: "ResearcherExps",
                newName: "DateEnd");

            migrationBuilder.AlterColumn<int>(
                name: "ResearcherEducationInformationId",
                table: "Researchers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DateBeg",
                table: "ResearcherExps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_ResearcherEducationInformationId",
                table: "Researchers",
                column: "ResearcherEducationInformationId",
                unique: true,
                filter: "[ResearcherEducationInformationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Researchers_ResearcherEducationInformations_ResearcherEducationInformationId",
                table: "Researchers",
                column: "ResearcherEducationInformationId",
                principalTable: "ResearcherEducationInformations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researchers_ResearcherEducationInformations_ResearcherEducationInformationId",
                table: "Researchers");

            migrationBuilder.DropIndex(
                name: "IX_Researchers_ResearcherEducationInformationId",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "DateBeg",
                table: "ResearcherExps");

            migrationBuilder.RenameColumn(
                name: "DateEnd",
                table: "ResearcherExps",
                newName: "DateBegEnd");

            migrationBuilder.AlterColumn<int>(
                name: "ResearcherEducationInformationId",
                table: "Researchers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_ResearcherEducationInformationId",
                table: "Researchers",
                column: "ResearcherEducationInformationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Researchers_ResearcherEducationInformations_ResearcherEducationInformationId",
                table: "Researchers",
                column: "ResearcherEducationInformationId",
                principalTable: "ResearcherEducationInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
