using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management.Migrations
{
    public partial class removedattachements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    IssueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.IssueID);
                    table.ForeignKey(
                        name: "FK_Attachment_Issue_IssueID1",
                        column: x => x.IssueID1,
                        principalTable: "Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_IssueID1",
                table: "Attachment",
                column: "IssueID1");
        }
    }
}
