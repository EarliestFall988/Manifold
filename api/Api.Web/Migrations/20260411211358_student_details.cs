using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Web.Migrations
{
    /// <inheritdoc />
    public partial class student_details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GPA",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Email", "GPA", "Major" },
                values: new object[] { "", "0.0", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GPA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Students");
        }
    }
}
