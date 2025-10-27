using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horoscope.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "HoroscopeQueries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "HoroscopeQueries");
        }
    }
}
