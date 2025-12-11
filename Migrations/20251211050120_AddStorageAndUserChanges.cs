using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddStorageAndUserChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_UserId",
                table: "Storages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Users_UserId",
                table: "Storages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Users_UserId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_UserId",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "fullName",
                table: "Users");
        }
    }
}
