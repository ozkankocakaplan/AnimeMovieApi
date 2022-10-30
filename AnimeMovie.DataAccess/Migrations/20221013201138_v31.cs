using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentID",
                table: "Likes",
                newName: "EpisodeID");

            migrationBuilder.CreateTable(
                name: "ContentNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ContentID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContentNotifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTypes_CategoryID",
                table: "CategoryTypes",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ContentNotifications_UserID",
                table: "ContentNotifications",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTypes_Categories_CategoryID",
                table: "CategoryTypes",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTypes_Categories_CategoryID",
                table: "CategoryTypes");

            migrationBuilder.DropTable(
                name: "ContentNotifications");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTypes_CategoryID",
                table: "CategoryTypes");

            migrationBuilder.RenameColumn(
                name: "EpisodeID",
                table: "Likes",
                newName: "ContentID");
        }
    }
}
