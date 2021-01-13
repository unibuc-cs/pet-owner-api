using Microsoft.EntityFrameworkCore.Migrations;

namespace PetOwner.Migrations
{
    public partial class Migratie3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetTokens",
                table: "Achievement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetTokens",
                table: "Achievement");
        }
    }
}
