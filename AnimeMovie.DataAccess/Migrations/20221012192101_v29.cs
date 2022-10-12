using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rosettes_UserRosettes_RosetteID",
                table: "Rosettes");

            migrationBuilder.DropIndex(
                name: "IX_Rosettes_RosetteID",
                table: "Rosettes");

            migrationBuilder.DropColumn(
                name: "RosetteID",
                table: "Rosettes");

            migrationBuilder.CreateIndex(
                name: "IX_UserRosettes_RosetteID",
                table: "UserRosettes",
                column: "RosetteID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRosettes_Rosettes_RosetteID",
                table: "UserRosettes",
                column: "RosetteID",
                principalTable: "Rosettes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRosettes_Rosettes_RosetteID",
                table: "UserRosettes");

            migrationBuilder.DropIndex(
                name: "IX_UserRosettes_RosetteID",
                table: "UserRosettes");

            migrationBuilder.AddColumn<int>(
                name: "RosetteID",
                table: "Rosettes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rosettes_RosetteID",
                table: "Rosettes",
                column: "RosetteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rosettes_UserRosettes_RosetteID",
                table: "Rosettes",
                column: "RosetteID",
                principalTable: "UserRosettes",
                principalColumn: "ID");
        }
    }
}
