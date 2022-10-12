using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Mangas_AnimeID",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_FanArts_Users_UsersID",
                table: "FanArts");

            migrationBuilder.DropIndex(
                name: "IX_FanArts_UsersID",
                table: "FanArts");

            migrationBuilder.DropColumn(
                name: "UsersID",
                table: "FanArts");

            migrationBuilder.CreateIndex(
                name: "IX_FanArts_UserID",
                table: "FanArts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_FanArts_Users_UserID",
                table: "FanArts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FanArts_Users_UserID",
                table: "FanArts");

            migrationBuilder.DropIndex(
                name: "IX_FanArts_UserID",
                table: "FanArts");

            migrationBuilder.AddColumn<int>(
                name: "UsersID",
                table: "FanArts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FanArts_UsersID",
                table: "FanArts",
                column: "UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Mangas_AnimeID",
                table: "Animes",
                column: "AnimeID",
                principalTable: "Mangas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FanArts_Users_UsersID",
                table: "FanArts",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
