using Microsoft.EntityFrameworkCore.Migrations;

namespace PetOwner.Migrations
{
    public partial class Migratie4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Tips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Tips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Tips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "Species",
                table: "Tips");
        }
    }
}
