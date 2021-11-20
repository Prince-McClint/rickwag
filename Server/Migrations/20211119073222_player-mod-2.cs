using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordJumble.Server.Migrations
{
    public partial class playermod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "School",
                table: "AspNetUsers");
        }
    }
}
