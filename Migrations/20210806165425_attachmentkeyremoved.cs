using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management.Migrations
{
    public partial class attachmentkeyremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Issue_IssueID1",
                table: "Attachment");

            migrationBuilder.AlterColumn<int>(
                name: "IssueID1",
                table: "Attachment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Attachment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Issue_IssueID1",
                table: "Attachment",
                column: "IssueID1",
                principalTable: "Issue",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Issue_IssueID1",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Attachment");

            migrationBuilder.AlterColumn<int>(
                name: "IssueID1",
                table: "Attachment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Issue_IssueID1",
                table: "Attachment",
                column: "IssueID1",
                principalTable: "Issue",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
