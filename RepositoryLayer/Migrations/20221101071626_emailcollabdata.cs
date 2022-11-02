using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class emailcollabdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollabratorTable",
                columns: table => new
                {
                    Collabratorid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    noteid = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabratorTable", x => x.Collabratorid);
                    table.ForeignKey(
                        name: "FK_CollabratorTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabratorTable_NotesTable_noteid",
                        column: x => x.noteid,
                        principalTable: "NotesTable",
                        principalColumn: "noteid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabratorTable_UserId",
                table: "CollabratorTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollabratorTable_noteid",
                table: "CollabratorTable",
                column: "noteid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabratorTable");
        }
    }
}
