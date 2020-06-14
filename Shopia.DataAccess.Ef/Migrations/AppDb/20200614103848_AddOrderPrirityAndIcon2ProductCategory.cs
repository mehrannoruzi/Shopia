using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopia.DataAccess.Ef.Migrations.AppDb
{
    public partial class AddOrderPrirityAndIcon2ProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "Base",
                table: "ProductCategory",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderPriority",
                schema: "Base",
                table: "ProductCategory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                schema: "Base",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "OrderPriority",
                schema: "Base",
                table: "ProductCategory");
        }
    }
}
