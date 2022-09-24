using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscordUrl",
                table: "SiteDescription");

            migrationBuilder.DropColumn(
                name: "SiteRating",
                table: "Animes");

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

            migrationBuilder.AddColumn<int>(
                name: "ContentID",
                table: "ContentComplaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "ContentComplaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MalRating",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "AnimeImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeID = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MangaImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaID = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaImages", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeImages");

            migrationBuilder.DropTable(
                name: "MangaImages");

            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "Rosettes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Rosettes");

            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "ContentComplaints");

            migrationBuilder.DropColumn(
                name: "type",
                table: "ContentComplaints");

            migrationBuilder.AddColumn<string>(
                name: "DiscordUrl",
                table: "SiteDescription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "MalRating",
                table: "Animes",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "SiteRating",
                table: "Animes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
