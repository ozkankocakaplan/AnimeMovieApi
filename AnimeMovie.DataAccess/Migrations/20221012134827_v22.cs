using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Mangas_MangaID",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_MangaLists_MangaListID",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Animes_MangaID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "MangaID",
                table: "Animes");

            migrationBuilder.RenameColumn(
                name: "MangaListID",
                table: "Mangas",
                newName: "MangaID");

            migrationBuilder.RenameIndex(
                name: "IX_Mangas_MangaListID",
                table: "Mangas",
                newName: "IX_Mangas_MangaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Mangas_AnimeID",
                table: "Animes",
                column: "AnimeID",
                principalTable: "Mangas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_MangaLists_MangaID",
                table: "Mangas",
                column: "MangaID",
                principalTable: "MangaLists",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Mangas_AnimeID",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_MangaLists_MangaID",
                table: "Mangas");

            migrationBuilder.RenameColumn(
                name: "MangaID",
                table: "Mangas",
                newName: "MangaListID");

            migrationBuilder.RenameIndex(
                name: "IX_Mangas_MangaID",
                table: "Mangas",
                newName: "IX_Mangas_MangaListID");

            migrationBuilder.AddColumn<int>(
                name: "MangaID",
                table: "Animes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animes_MangaID",
                table: "Animes",
                column: "MangaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Mangas_MangaID",
                table: "Animes",
                column: "MangaID",
                principalTable: "Mangas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_MangaLists_MangaListID",
                table: "Mangas",
                column: "MangaListID",
                principalTable: "MangaLists",
                principalColumn: "ID");
        }
    }
}
