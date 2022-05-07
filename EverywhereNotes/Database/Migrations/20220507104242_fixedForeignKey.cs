using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EverywhereNotes.Migrations
{
    public partial class fixedForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_Id",
                table: "Notes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Notes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_userId",
                table: "Notes",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_userId",
                table: "Notes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_userId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_userId",
                table: "Notes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Notes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_Id",
                table: "Notes",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
