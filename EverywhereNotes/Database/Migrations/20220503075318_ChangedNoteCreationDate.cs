using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EverywhereNotes.Migrations
{
    public partial class ChangedNoteCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Notes",
                newName: "CreationDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDateTime",
                table: "Notes",
                newName: "CreationDate");
        }
    }
}
