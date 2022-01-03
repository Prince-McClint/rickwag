using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordJumble.Server.Migrations
{
    public partial class wordlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Words");
        }
    }
}
