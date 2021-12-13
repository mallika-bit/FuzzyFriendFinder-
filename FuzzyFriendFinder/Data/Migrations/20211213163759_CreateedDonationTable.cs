using Microsoft.EntityFrameworkCore.Migrations;

namespace FuzzyFriendFinder.Data.Migrations
{
    public partial class CreateedDonationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_Pet_PetId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_AspNetUsers_UserId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Category_CategoryId",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoption",
                table: "Adoption");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "Pets");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Adoption",
                newName: "Adoptions");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_CategoryId",
                table: "Pets",
                newName: "IX_Pets_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoption_UserId",
                table: "Adoptions",
                newName: "IX_Adoptions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoption_PetId",
                table: "Adoptions",
                newName: "IX_Adoptions_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Pets_PetId",
                table: "Adoptions",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_AspNetUsers_UserId",
                table: "Adoptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Categories_CategoryId",
                table: "Pets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Pets_PetId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_AspNetUsers_UserId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Categories_CategoryId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions");

            migrationBuilder.RenameTable(
                name: "Pets",
                newName: "Pet");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Adoptions",
                newName: "Adoption");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_CategoryId",
                table: "Pet",
                newName: "IX_Pet_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoption",
                newName: "IX_Adoption_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoptions_PetId",
                table: "Adoption",
                newName: "IX_Adoption_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoption",
                table: "Adoption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_Pet_PetId",
                table: "Adoption",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_AspNetUsers_UserId",
                table: "Adoption",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Category_CategoryId",
                table: "Pet",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
