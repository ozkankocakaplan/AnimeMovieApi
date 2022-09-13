using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiteRaning",
                table: "Animes",
                newName: "SiteRating");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoUrl",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "SeoUrl",
                table: "Animes");

            migrationBuilder.RenameColumn(
                name: "SiteRating",
                table: "Animes",
                newName: "SiteRaning");
        }
    }
}
