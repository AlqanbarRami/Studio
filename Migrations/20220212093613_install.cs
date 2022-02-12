using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApi.Migrations
{
    public partial class install : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmId);
                });

            migrationBuilder.CreateTable(
                name: "FilmStudios",
                columns: table => new
                {
                    FilmStudioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmStudios", x => x.FilmStudioId);
                });

            migrationBuilder.CreateTable(
                name: "FilmCopies",
                columns: table => new
                {
                    FilmCopyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    StudioId = table.Column<int>(type: "int", nullable: false),
                    RentedOut = table.Column<bool>(type: "bit", nullable: false),
                    FilmStudioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmCopies", x => x.FilmCopyId);
                    table.ForeignKey(
                        name: "FK_FilmCopies_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmCopies_FilmStudios_FilmStudioId",
                        column: x => x.FilmStudioId,
                        principalTable: "FilmStudios",
                        principalColumn: "FilmStudioId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmStudioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmStudioId1 = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_FilmStudios_FilmStudioId1",
                        column: x => x.FilmStudioId1,
                        principalTable: "FilmStudios",
                        principalColumn: "FilmStudioId");
                });

            migrationBuilder.InsertData(
                table: "FilmStudios",
                columns: new[] { "FilmStudioId", "City", "Name" },
                values: new object[,]
                {
                    { 1, "Holllywood", "MGM" },
                    { 2, "Hollywood", "News Corporation" }
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "FilmId", "Country", "Director", "Name", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, "France", "Jean-Paul Salomé", "Mama Weed", "2020" },
                    { 2, "Sweden", "Lars Dimming, Bo Harringer", "Den siste cafépianisten", "2012" },
                    { 3, "USA", "Joel Coen", "The Tragedy of Macbeth", "2021" },
                    { 4, "Sweden", "Jesper Klevenås", "Shop", "2020" },
                    { 5, "Malta", "Alex Camilleri", "Luzzu", "2021" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FilmStudioId", "FilmStudioId1", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Token", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[,]
                {
                    { "185790a0-c48f-4900-b7e4-d1f9d5e3f6da", 0, "cab12eaf-257a-468a-aaf5-9d9e832d93a4", null, false, "1", null, false, null, null, null, "Studio123!", "AQAAAAEAACcQAAAAEJubvSWy/Dao/lZXXi5nCbG1fksuBUKgNGBD+8UAnUK4T/RkuB+w1NgVCqPmdYkawg==", null, false, "filmstudio", "153d02f3-8645-4396-9087-7063881b5113", null, false, 0, "Studio" },
                    { "ea2672ef-f25d-4d4f-bf75-68472d3aa960", 0, "2c870bc6-8ef7-44b6-b2e4-c7242197cd94", null, false, null, null, false, null, null, null, "Admin123!", "AQAAAAEAACcQAAAAEJ/6JqVa3Px/lZ763YVh50WIVwrTMpCTcYlzo5EP6WdYUY1U+sLdjhagTy0WyMoz9w==", null, false, "admin", "89991584-9627-4a3b-b45b-35cecc875532", null, false, 1, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "FilmCopies",
                columns: new[] { "FilmCopyId", "FilmId", "FilmStudioId", "RentedOut", "StudioId" },
                values: new object[,]
                {
                    { 1, 1, null, false, 0 },
                    { 2, 1, null, false, 0 },
                    { 3, 2, null, false, 0 },
                    { 4, 2, null, false, 0 },
                    { 5, 3, null, false, 0 },
                    { 6, 4, null, false, 0 },
                    { 7, 4, null, false, 0 },
                    { 8, 5, null, false, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmCopies_FilmId",
                table: "FilmCopies",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmCopies_FilmStudioId",
                table: "FilmCopies",
                column: "FilmStudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FilmStudioId1",
                table: "Users",
                column: "FilmStudioId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmCopies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "FilmStudios");
        }
    }
}
