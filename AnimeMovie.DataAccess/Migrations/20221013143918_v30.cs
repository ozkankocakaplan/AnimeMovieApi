using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userLists_UserListContents_ListID",
                table: "userLists");

            migrationBuilder.DropIndex(
                name: "IX_userLists_ListID",
                table: "userLists");

            migrationBuilder.DropColumn(
                name: "ListID",
                table: "userLists");

            migrationBuilder.CreateIndex(
                name: "IX_UserListContents_ListID",
                table: "UserListContents",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_UserListContents_UserID",
                table: "UserListContents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RosetteContents_RosetteID",
                table: "RosetteContents",
                column: "RosetteID");

            migrationBuilder.AddForeignKey(
                name: "FK_RosetteContents_Rosettes_RosetteID",
                table: "RosetteContents",
                column: "RosetteID",
                principalTable: "Rosettes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserListContents_userLists_ListID",
                table: "UserListContents",
                column: "ListID",
                principalTable: "userLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserListContents_Users_UserID",
                table: "UserListContents",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RosetteContents_Rosettes_RosetteID",
                table: "RosetteContents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserListContents_userLists_ListID",
                table: "UserListContents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserListContents_Users_UserID",
                table: "UserListContents");

            migrationBuilder.DropIndex(
                name: "IX_UserListContents_ListID",
                table: "UserListContents");

            migrationBuilder.DropIndex(
                name: "IX_UserListContents_UserID",
                table: "UserListContents");

            migrationBuilder.DropIndex(
                name: "IX_RosetteContents_RosetteID",
                table: "RosetteContents");

            migrationBuilder.AddColumn<int>(
                name: "ListID",
                table: "userLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userLists_ListID",
                table: "userLists",
                column: "ListID");

            migrationBuilder.AddForeignKey(
                name: "FK_userLists_UserListContents_ListID",
                table: "userLists",
                column: "ListID",
                principalTable: "UserListContents",
                principalColumn: "ID");
        }
    }
}
