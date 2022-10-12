using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "Rosettes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Rosettes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentID",
                table: "Rosettes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Rosettes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
