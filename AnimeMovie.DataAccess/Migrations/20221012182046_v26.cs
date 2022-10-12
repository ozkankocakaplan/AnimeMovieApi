using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FanArts_Animes_AnimeID",
                table: "FanArts");

            migrationBuilder.DropForeignKey(
                name: "FK_FanArts_Mangas_MangaID",
                table: "FanArts");

            migrationBuilder.DropForeignKey(
                name: "FK_FanArts_Users_UserID",
                table: "FanArts");

            migrationBuilder.DropIndex(
                name: "IX_FanArts_AnimeID",
                table: "FanArts");

            migrationBuilder.DropIndex(
                name: "IX_FanArts_UserID",
                table: "FanArts");

            migrationBuilder.DropColumn(
                name: "AnimeID",
                table: "FanArts");

            migrationBuilder.RenameColumn(
                name: "MangaID",
                table: "FanArts",
                newName: "UsersID");

            migrationBuilder.RenameIndex(
                name: "IX_FanArts_MangaID",
                table: "FanArts",
                newName: "IX_FanArts_UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_FanArts_Users_UsersID",
                table: "FanArts",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FanArts_Users_UsersID",
                table: "FanArts");

            migrationBuilder.RenameColumn(
                name: "UsersID",
                table: "FanArts",
                newName: "MangaID");

            migrationBuilder.RenameIndex(
                name: "IX_FanArts_UsersID",
                table: "FanArts",
                newName: "IX_FanArts_MangaID");

            migrationBuilder.AddColumn<int>(
                name: "AnimeID",
                table: "FanArts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FanArts_AnimeID",
                table: "FanArts",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_FanArts_UserID",
                table: "FanArts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_FanArts_Animes_AnimeID",
                table: "FanArts",
                column: "AnimeID",
                principalTable: "Animes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FanArts_Mangas_MangaID",
                table: "FanArts",
                column: "MangaID",
                principalTable: "Mangas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FanArts_Users_UserID",
                table: "FanArts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
