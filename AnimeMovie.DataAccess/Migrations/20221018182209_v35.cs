using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arrangement",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Arrangement",
                table: "Animes");

            migrationBuilder.AddColumn<int>(
                name: "ContentID",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "Likes");

            migrationBuilder.AddColumn<string>(
                name: "Arrangement",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Arrangement",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
