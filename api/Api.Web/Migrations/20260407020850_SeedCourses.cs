using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Archived", "Credits", "Description", "Inserted", "InsertedBy", "InstructorId", "Name", "StudentId", "Updated", "UpdatedBy", "Weeks" },
                values: new object[,]
                {
                    { 2, false, 3, "Fundamentals of programming and computational thinking.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Introduction to Computer Science", null, null, null, 16 },
                    { 3, false, 3, "Arrays, linked lists, trees, graphs, and algorithm analysis.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Data Structures & Algorithms", null, null, null, 16 },
                    { 4, false, 4, "Limits, derivatives, and an introduction to integration.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Calculus I", null, null, null, 16 },
                    { 5, false, 3, "Vectors, matrices, eigenvalues, and linear transformations.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Linear Algebra", null, null, null, 16 },
                    { 6, false, 3, "HTML, CSS, JavaScript, and modern frontend frameworks.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Web Development", null, null, null, 12 },
                    { 7, false, 3, "Relational databases, SQL, normalization, and query optimization.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Database Systems", null, null, null, 16 },
                    { 8, false, 3, "Processes, memory management, file systems, and concurrency.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Operating Systems", null, null, null, 16 },
                    { 9, false, 4, "Supervised and unsupervised learning, neural networks, and model evaluation.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Machine Learning", null, null, null, 16 },
                    { 10, false, 3, "Agile methodologies, system design, testing, and project management.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Software Engineering", null, null, null, 16 },
                    { 11, false, 3, "TCP/IP, routing, protocols, and network security fundamentals.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed", null, "Computer Networks", null, null, null, 16 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
