using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasteBud.API.Migrations
{
    /// <inheritdoc />
    public partial class EditForeignKeyReferencesInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_AspNetUsers_UserId",
                table: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Result_UserId",
                table: "Result");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Result",
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
                table: "Result",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Result_UserId",
                table: "Result",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_AspNetUsers_UserId",
                table: "Result",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
