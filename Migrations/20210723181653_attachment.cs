using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management.Migrations
{
    public partial class attachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Attachment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
