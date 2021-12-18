using Microsoft.EntityFrameworkCore.Migrations;

namespace FuzzyFriendFinder.Migrations
{
    public partial class RenamedPetsSizeColumnNameToWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Pets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
