using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reviews_UserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MangaLists_EpisodeID",
                table: "MangaLists",
                column: "EpisodeID");

            migrationBuilder.CreateIndex(
                name: "IX_MangaLists_MangaID",
                table: "MangaLists",
                column: "MangaID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeLists_AnimeID",
                table: "AnimeLists",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeLists_EpisodeID",
                table: "AnimeLists",
                column: "EpisodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeLists_AnimeEpisodes_EpisodeID",
                table: "AnimeLists",
                column: "EpisodeID",
                principalTable: "AnimeEpisodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeLists_Animes_AnimeID",
                table: "AnimeLists",
                column: "AnimeID",
                principalTable: "Animes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaLists_MangaEpisodes_EpisodeID",
                table: "MangaLists",
                column: "EpisodeID",
                principalTable: "MangaEpisodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaLists_Mangas_MangaID",
                table: "MangaLists",
                column: "MangaID",
                principalTable: "Mangas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeLists_AnimeEpisodes_EpisodeID",
                table: "AnimeLists");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeLists_Animes_AnimeID",
                table: "AnimeLists");

            migrationBuilder.DropForeignKey(
                name: "FK_MangaLists_MangaEpisodes_EpisodeID",
                table: "MangaLists");

            migrationBuilder.DropForeignKey(
                name: "FK_MangaLists_Mangas_MangaID",
                table: "MangaLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_MangaLists_EpisodeID",
                table: "MangaLists");

            migrationBuilder.DropIndex(
                name: "IX_MangaLists_MangaID",
                table: "MangaLists");

            migrationBuilder.DropIndex(
                name: "IX_AnimeLists_AnimeID",
                table: "AnimeLists");

            migrationBuilder.DropIndex(
                name: "IX_AnimeLists_EpisodeID",
                table: "AnimeLists");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserID",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reviews_UserID",
                table: "Users",
                column: "UserID",
                principalTable: "Reviews",
                principalColumn: "ID");
        }
    }
}
