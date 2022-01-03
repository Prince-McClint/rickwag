using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordJumble.Server.Migrations
{
    public partial class added_levels_count : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelsCount",
                table: "Dictionaries",
                type: "int",
                nullable: false,
                defaultValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelsCount",
                table: "Dictionaries");
        }
    }
}
