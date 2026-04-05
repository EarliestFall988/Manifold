using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Web.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 4, 5), "Bracing", 30 },
                    { 2, new DateOnly(2026, 4, 6), "Warm", -11 },
                    { 3, new DateOnly(2026, 4, 7), "Chilly", -8 },
                    { 4, new DateOnly(2026, 4, 8), "Warm", 34 },
                    { 5, new DateOnly(2026, 4, 9), "Hot", -7 },
                    { 6, new DateOnly(2026, 4, 10), "Chilly", -3 },
                    { 7, new DateOnly(2026, 4, 11), "Cool", 17 },
                    { 8, new DateOnly(2026, 4, 12), "Chilly", 8 },
                    { 9, new DateOnly(2026, 4, 13), "Freezing", 18 },
                    { 10, new DateOnly(2026, 4, 14), "Warm", 41 },
                    { 11, new DateOnly(2026, 4, 15), "Bracing", 9 },
                    { 12, new DateOnly(2026, 4, 16), "Hot", -14 },
                    { 13, new DateOnly(2026, 4, 17), "Warm", 41 },
                    { 14, new DateOnly(2026, 4, 18), "Hot", -17 },
                    { 15, new DateOnly(2026, 4, 19), "Scorching", -9 },
                    { 16, new DateOnly(2026, 4, 20), "Warm", 31 },
                    { 17, new DateOnly(2026, 4, 21), "Freezing", -7 },
                    { 18, new DateOnly(2026, 4, 22), "Warm", 3 },
                    { 19, new DateOnly(2026, 4, 23), "Freezing", 41 },
                    { 20, new DateOnly(2026, 4, 24), "Sweltering", 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecasts");
        }
    }
}
