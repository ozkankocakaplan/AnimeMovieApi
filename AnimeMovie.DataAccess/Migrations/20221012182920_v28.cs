using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AnimeLists_AnimeID",
                table: "Animes");

            migrationBuilder.DropIndex(
                name: "IX_Animes_AnimeID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "AnimeID",
                table: "Animes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimeID",
                table: "Animes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animes_AnimeID",
                table: "Animes",
                column: "AnimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_AnimeLists_AnimeID",
                table: "Animes",
                column: "AnimeID",
                principalTable: "AnimeLists",
                principalColumn: "ID");
        }
    }
}
