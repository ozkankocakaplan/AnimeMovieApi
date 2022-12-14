using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_socialMediaAccounts_UserID",
                table: "socialMediaAccounts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_socialMediaAccounts_Users_UserID",
                table: "socialMediaAccounts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_socialMediaAccounts_Users_UserID",
                table: "socialMediaAccounts");

            migrationBuilder.DropIndex(
                name: "IX_socialMediaAccounts_UserID",
                table: "socialMediaAccounts");
        }
    }
}
