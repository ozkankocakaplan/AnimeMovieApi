using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_MangaLists_MangaID",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_MangaID",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "MangaID",
                table: "Mangas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MangaID",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_MangaID",
                table: "Mangas",
                column: "MangaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_MangaLists_MangaID",
                table: "Mangas",
                column: "MangaID",
                principalTable: "MangaLists",
                principalColumn: "ID");
        }
    }
}
