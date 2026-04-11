using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "Archived", "Inserted", "InsertedBy", "Name", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 20, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Alice Johnson", null, null },
                    { 2, 22, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Ben Carter", null, null },
                    { 3, 21, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Clara Nguyen", null, null },
                    { 4, 23, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "David Kim", null, null },
                    { 5, 20, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Elena Rossi", null, null },
                    { 6, 24, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Frank Osei", null, null },
                    { 7, 19, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Grace Liu", null, null },
                    { 8, 22, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Henry Martínez", null, null },
                    { 9, 21, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "Isla Patel", null, null },
                    { 10, 25, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", "James Okafor", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
