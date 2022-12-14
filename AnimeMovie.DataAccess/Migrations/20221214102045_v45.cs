using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteRating",
                table: "Mangas");

            migrationBuilder.AddColumn<string>(
                name: "fansub",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fansub",
                table: "Mangas");

            migrationBuilder.AddColumn<string>(
                name: "SiteRating",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
