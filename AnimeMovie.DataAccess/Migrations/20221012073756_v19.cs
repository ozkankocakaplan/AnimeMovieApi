using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "HomeSliders");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "HomeSliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "HomeSliders");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "HomeSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
