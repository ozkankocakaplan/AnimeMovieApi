using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimeID",
                table: "Reviews",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "AnimeID",
                table: "FanArts",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RosetteID",
                table: "Rosettes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentID",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MangaListID",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MangaID",
                table: "MangaLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentID",
                table: "FanArts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnimeID",
                table: "Animes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeID",
                table: "AnimeLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserListContents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ListID = table.Column<int>(type: "int", nullable: false),
                    ContentID = table.Column<int>(type: "int", nullable: false),
                    EpisodeID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserListContents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "userLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListID = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_userLists_UserListContents_ListID",
                        column: x => x.ListID,
                        principalTable: "UserListContents",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserID",
                table: "Users",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Rosettes_RosetteID",
                table: "Rosettes",
                column: "RosetteID");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_MangaListID",
                table: "Mangas",
                column: "MangaListID");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_AnimeID",
                table: "Animes",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_userLists_ListID",
                table: "userLists",
                column: "ListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_AnimeLists_AnimeID",
                table: "Animes",
                column: "AnimeID",
                principalTable: "AnimeLists",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_MangaLists_MangaListID",
                table: "Mangas",
                column: "MangaListID",
                principalTable: "MangaLists",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rosettes_UserRosettes_RosetteID",
                table: "Rosettes",
                column: "RosetteID",
                principalTable: "UserRosettes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reviews_UserID",
                table: "Users",
                column: "UserID",
                principalTable: "Reviews",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AnimeLists_AnimeID",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_MangaLists_MangaListID",
                table: "Mangas");

            migrationBuilder.DropForeignKey(
                name: "FK_Rosettes_UserRosettes_RosetteID",
                table: "Rosettes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reviews_UserID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "userLists");

            migrationBuilder.DropTable(
                name: "UserListContents");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rosettes_RosetteID",
                table: "Rosettes");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_MangaListID",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Animes_AnimeID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RosetteID",
                table: "Rosettes");

            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MangaListID",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "MangaID",
                table: "MangaLists");

            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "FanArts");

            migrationBuilder.DropColumn(
                name: "AnimeID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "EpisodeID",
                table: "AnimeLists");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Reviews",
                newName: "AnimeID");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FanArts",
                newName: "AnimeID");
        }
    }
}
