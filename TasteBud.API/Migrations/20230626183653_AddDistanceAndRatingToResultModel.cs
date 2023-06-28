using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteBud.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDistanceAndRatingToResultModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Distance",
                table: "Result",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Result",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Result");
        }
    }
}
