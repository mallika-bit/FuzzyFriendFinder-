using Microsoft.EntityFrameworkCore.Migrations;

namespace FuzzyFriendFinder.Migrations
{
    public partial class RenamedPetsImageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "Pets",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Pets",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }
    }
}
