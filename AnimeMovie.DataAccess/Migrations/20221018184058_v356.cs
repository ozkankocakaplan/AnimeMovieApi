using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v356 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Animes");

            migrationBuilder.AddColumn<string>(
                name: "SiteRating",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteRating",
                table: "Animes");

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Animes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Animes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
