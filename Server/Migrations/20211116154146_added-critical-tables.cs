using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordJumble.Server.Migrations
{
    public partial class addedcriticaltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    DictionaryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DictionaryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.DictionaryID);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meaning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DictionaryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordID);
                    table.ForeignKey(
                        name: "FK_Words_Dictionaries_DictionaryID",
                        column: x => x.DictionaryID,
                        principalTable: "Dictionaries",
                        principalColumn: "DictionaryID");
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoreValue = table.Column<int>(type: "int", nullable: false),
                    WordID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreID);
                    table.ForeignKey(
                        name: "FK_Scores_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "WordID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_WordID",
                table: "Scores",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_DictionaryID",
                table: "Words",
                column: "DictionaryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Dictionaries");
        }
    }
}
