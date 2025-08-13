using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YoutubeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitCreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Youtubers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ChannelName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Subscriber = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Youtubers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ViewCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    LikeCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DislikeCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    YoutuberID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Youtubers_YoutuberID",
                        column: x => x.YoutuberID,
                        principalTable: "Youtubers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Youtubers",
                columns: new[] { "Id", "ChannelName", "DeletedAt", "Email", "IsDeleted", "Name", "Subscriber" },
                values: new object[,]
                {
                    { 1, "penguinz0", null, "Cr1TiKaLContact@gmail.com", false, "penguinz0", 17100000 },
                    { 2, "LinusTechTips", null, "support@lttstore.com", false, "Linus Tech Tips", 16500000 }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "DeletedAt", "Description", "DislikeCount", "IsDeleted", "LikeCount", "Title", "Url", "ViewCount", "YoutuberID" },
                values: new object[,]
                {
                    { 1, null, "Cr1TiKaL explores an underrated FPS game that’s being overlooked.", 1000, false, 155000, "This is the Best FPS Right Now But Everyone is Sleeping On It", "https://www.youtube.com/watch?v=0Qr6rhbAzh0", 955000, 1 },
                    { 2, null, "Cr1TiKaL asks—are we completely cooked? (food-themed humor).", 1000, false, 250000, "Are We Completely Cooked", "https://www.youtube.com/watch?v=KAKEPxVeTrE", 2100000, 1 },
                    { 3, null, "Building five PCs in the best-selling cases—Linus Tech Tips.", 1000, false, 215000, "Building 5 PCs in the 5 BEST selling Cases", "https://www.youtube.com/watch?v=iOC7-GPWxaU", 715000, 2 },
                    { 4, null, "A winner got a $5000 gaming PC—awkward moments unfold.", 1000, false, 317000, "He won this $5000 Gaming PC (But it was VERY Awkward)", "https://www.youtube.com/watch?v=gXVpAVwN8F0", 919000, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Url",
                table: "Videos",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_YoutuberID",
                table: "Videos",
                column: "YoutuberID");

            migrationBuilder.CreateIndex(
                name: "IX_Youtubers_Email",
                table: "Youtubers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Youtubers");
        }
    }
}
