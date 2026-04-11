using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Web.Migrations
{
    /// <inheritdoc />
    public partial class studentTakeCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Weeks = table.Column<int>(type: "INTEGER", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    Inserted = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InsertedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    Inserted = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InsertedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    CoursesListJson = table.Column<string>(type: "TEXT", nullable: false),
                    Inserted = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InsertedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    Inserted = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InsertedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "Archived", "Date", "Inserted", "InsertedBy", "Summary", "TemperatureC", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, false, new DateOnly(2026, 4, 7), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Bracing", 30, null, null },
                    { 2, false, new DateOnly(2026, 4, 8), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Warm", -11, null, null },
                    { 3, false, new DateOnly(2026, 4, 9), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Chilly", -8, null, null },
                    { 4, false, new DateOnly(2026, 4, 10), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Warm", 34, null, null },
                    { 5, false, new DateOnly(2026, 4, 11), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Hot", -7, null, null },
                    { 6, false, new DateOnly(2026, 4, 12), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Chilly", -3, null, null },
                    { 7, false, new DateOnly(2026, 4, 13), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Cool", 17, null, null },
                    { 8, false, new DateOnly(2026, 4, 14), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Chilly", 8, null, null },
                    { 9, false, new DateOnly(2026, 4, 15), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Freezing", 18, null, null },
                    { 10, false, new DateOnly(2026, 4, 16), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Warm", 41, null, null },
                    { 11, false, new DateOnly(2026, 4, 17), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Bracing", 9, null, null },
                    { 12, false, new DateOnly(2026, 4, 18), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Hot", -14, null, null },
                    { 13, false, new DateOnly(2026, 4, 19), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Warm", 41, null, null },
                    { 14, false, new DateOnly(2026, 4, 20), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Hot", -17, null, null },
                    { 15, false, new DateOnly(2026, 4, 21), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Scorching", -9, null, null },
                    { 16, false, new DateOnly(2026, 4, 22), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Warm", 31, null, null },
                    { 17, false, new DateOnly(2026, 4, 23), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Freezing", -7, null, null },
                    { 18, false, new DateOnly(2026, 4, 24), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Warm", 3, null, null },
                    { 19, false, new DateOnly(2026, 4, 25), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Freezing", 41, null, null },
                    { 20, false, new DateOnly(2026, 4, 26), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Sweltering", 20, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "WeatherForecasts");
        }
    }
}
