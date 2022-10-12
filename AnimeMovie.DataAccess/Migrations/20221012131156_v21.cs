using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimeID",
                table: "Mangas",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Mangas_MangaID",
                table: "Animes");

            migrationBuilder.DropIndex(
                name: "IX_Animes_MangaID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "AnimeID",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "MangaID",
                table: "Animes");
        }
    }
}
