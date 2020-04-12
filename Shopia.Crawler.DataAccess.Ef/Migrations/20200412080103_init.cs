using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopia.Crawler.DataAccess.Ef.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Instagram");

            migrationBuilder.CreateTable(
                name: "Page",
                schema: "Instagram",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostCount = table.Column<int>(nullable: false),
                    FolowerCount = table.Column<int>(nullable: false),
                    FolowingCount = table.Column<int>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    IsBusinessAccount = table.Column<bool>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    ModifyDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    ModifyDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    UniqueId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    FullName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Bio = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    BioUrl = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "Instagram",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    CommentCount = table.Column<int>(nullable: false),
                    IsAlbum = table.Column<bool>(nullable: false),
                    CreateDateMi = table.Column<DateTime>(nullable: false),
                    UniqueId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Page_PageId",
                        column: x => x.PageId,
                        principalSchema: "Instagram",
                        principalTable: "Page",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostAsset",
                schema: "Instagram",
                columns: table => new
                {
                    PostAssetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    InsertDateMi = table.Column<DateTime>(nullable: false),
                    InsertDateSh = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    Dimension = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: true),
                    UniqueId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FileUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAsset", x => x.PostAssetId);
                    table.ForeignKey(
                        name: "FK_PostAsset_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "Instagram",
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                schema: "Instagram",
                table: "Page",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PageId",
                schema: "Instagram",
                table: "Post",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueId",
                schema: "Instagram",
                table: "Post",
                column: "UniqueId",
                unique: true,
                filter: "[UniqueId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PostAsset_PostId",
                schema: "Instagram",
                table: "PostAsset",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueId",
                schema: "Instagram",
                table: "PostAsset",
                column: "UniqueId",
                unique: true,
                filter: "[UniqueId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostAsset",
                schema: "Instagram");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "Instagram");

            migrationBuilder.DropTable(
                name: "Page",
                schema: "Instagram");
        }
    }
}
