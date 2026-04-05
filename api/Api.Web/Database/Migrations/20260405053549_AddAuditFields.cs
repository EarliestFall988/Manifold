using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Web.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "WeatherForecasts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "WeatherForecasts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InsertedBy",
                table: "WeatherForecasts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "WeatherForecasts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "WeatherForecasts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });

            migrationBuilder.UpdateData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Archived", "Inserted", "InsertedBy", "Updated", "UpdatedBy" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "InsertedBy",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "WeatherForecasts");
        }
    }
}
