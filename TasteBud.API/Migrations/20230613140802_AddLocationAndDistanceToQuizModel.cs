using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteBud.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationAndDistanceToQuizModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Distance",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Quiz");
        }
    }
}
