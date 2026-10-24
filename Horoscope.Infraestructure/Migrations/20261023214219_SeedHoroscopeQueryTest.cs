using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horoscope.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedHoroscopeQueryTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HoroscopeQueries",
                columns: new[] { "Id", "BirthDate", "DateQueried", "Email", "Gender", "Lang", "Name", "ResponseJson", "Sign" },
                values: new object[] { 3, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "pocho@gmail.com", 1, "es", "pocho", "", "Leo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HoroscopeQueries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
