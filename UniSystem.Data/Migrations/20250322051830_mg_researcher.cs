using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class mg_researcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResearcherEducationInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postgraduate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostgraduateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Undergraduate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UndergraduateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForegnLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForegnLangII = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearcherEducationInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Researchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WoSResearchAreas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialResearchAreas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResearcherEducationInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Researchers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Researchers_ResearcherEducationInformations_ResearcherEducationInformationId",
                        column: x => x.ResearcherEducationInformationId,
                        principalTable: "ResearcherEducationInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchAreas_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearcherExps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBegEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Universty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duty = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearcherExps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearcherExps_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearcherMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitationWoS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIndexWoS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitationScopus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIndexScopus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitationScholar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIndexScholar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitiationTrDizin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIndexTrDizin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitiationSumOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThesisAdvisory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenAccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResearcherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearcherMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearcherMetrics_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearcherPublications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    PublishedArea = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearcherPublications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearcherPublications_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearcherTheses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supporter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearcherTheses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearcherTheses_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Universty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResearcherPublicationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationMembers_ResearcherPublications_ResearcherPublicationsId",
                        column: x => x.ResearcherPublicationsId,
                        principalTable: "ResearcherPublications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicationMembers_ResearcherPublicationsId",
                table: "PublicationMembers",
                column: "ResearcherPublicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAreas_ResearcherId",
                table: "ResearchAreas",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherExps_ResearcherId",
                table: "ResearcherExps",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherMetrics_ResearcherId",
                table: "ResearcherMetrics",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherPublications_ResearcherId",
                table: "ResearcherPublications",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_AppUserId",
                table: "Researchers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_ResearcherEducationInformationId",
                table: "Researchers",
                column: "ResearcherEducationInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherTheses_ResearcherId",
                table: "ResearcherTheses",
                column: "ResearcherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationMembers");

            migrationBuilder.DropTable(
                name: "ResearchAreas");

            migrationBuilder.DropTable(
                name: "ResearcherExps");

            migrationBuilder.DropTable(
                name: "ResearcherMetrics");

            migrationBuilder.DropTable(
                name: "ResearcherTheses");

            migrationBuilder.DropTable(
                name: "ResearcherPublications");

            migrationBuilder.DropTable(
                name: "Researchers");

            migrationBuilder.DropTable(
                name: "ResearcherEducationInformations");
        }
    }
}
