using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeMovie.DataAccess.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMediaManagers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserEmailVertifications");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "MangaEpisodes");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "MessageID",
                table: "UserMessages",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Read",
                table: "MangaEpisodes",
                newName: "MangaID");

            migrationBuilder.RenameColumn(
                name: "Complainant",
                table: "ComplaintLists",
                newName: "ComplainantID");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserEmailVertifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "SiteDescription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isReadInfo",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SeoUrl",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ContentOrder",
                table: "MangaEpisodeContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ContentComplaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ContentID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isSpoiler",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ContentID = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "socialMediaAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GmailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_socialMediaAccounts", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "socialMediaAccounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserEmailVertifications");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "SiteDescription");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "isReadInfo",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SeoUrl",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "ContentOrder",
                table: "MangaEpisodeContents");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "ContentComplaints");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContentID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "isSpoiler",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "UserMessages",
                newName: "MessageID");

            migrationBuilder.RenameColumn(
                name: "MangaID",
                table: "MangaEpisodes",
                newName: "Read");

            migrationBuilder.RenameColumn(
                name: "ComplainantID",
                table: "ComplaintLists",
                newName: "Complainant");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Comments",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "UserEmailVertifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "MangaEpisodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SocialMediaManagers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GmailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaManagers", x => x.ID);
                });
        }
    }
}
