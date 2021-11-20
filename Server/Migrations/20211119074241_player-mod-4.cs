using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordJumble.Server.Migrations
{
    public partial class playermod4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Scores",
                type: "nvarchar(450)",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Dictionaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_PlayerId",
                table: "Scores",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dictionaries_PlayerId",
                table: "Dictionaries",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionaries_AspNetUsers_PlayerId",
                table: "Dictionaries",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_AspNetUsers_PlayerId",
                table: "Scores",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dictionaries_AspNetUsers_PlayerId",
                table: "Dictionaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_AspNetUsers_PlayerId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_PlayerId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Dictionaries_PlayerId",
                table: "Dictionaries");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Dictionaries");
        }
    }
}
