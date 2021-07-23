using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management.Migrations
{
    public partial class descriptionadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Issue");
        }
    }
}
