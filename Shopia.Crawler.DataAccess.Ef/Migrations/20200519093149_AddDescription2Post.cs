using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopia.Crawler.DataAccess.Ef.Migrations
{
    public partial class AddDescription2Post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Instagram",
                table: "Post",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Instagram",
                table: "Post");
        }
    }
}
