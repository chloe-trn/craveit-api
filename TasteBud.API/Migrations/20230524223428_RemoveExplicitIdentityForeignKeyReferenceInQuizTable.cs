using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteBud.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExplicitIdentityForeignKeyReferenceInQuizTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_AspNetUsers_UserId",
                table: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_UserId",
                table: "Quiz");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Quiz",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_UserId",
                table: "Quiz",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_AspNetUsers_UserId",
                table: "Quiz",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
