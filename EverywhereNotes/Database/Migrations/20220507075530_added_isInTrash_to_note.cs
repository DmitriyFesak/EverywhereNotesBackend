using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EverywhereNotes.Migrations
{
    public partial class added_isInTrash_to_note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInTrash",
                table: "Notes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInTrash",
                table: "Notes");
        }
    }
}
