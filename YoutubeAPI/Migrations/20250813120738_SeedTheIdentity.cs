using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YoutubeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedTheIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1b2c3d4-e5f6-7890-abcd-123456789abc", "role-admin-stamp", "Admin", "ADMIN" },
                    { "b2c3d4e5-f6g7-8901-bcde-234567890def", "role-user-stamp", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DeletedAt", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c3d4e5f6-g7h8-9012-cdef-345678901ghi", 0, "role-admin-stamp", null, "admin@youtube.com", true, false, false, null, "ADMIN@YOUTUBE.COM", "ADMIN", "AQAAAAEAACcQAAAAENQjeGx4Pe5dqDKN8rTAMqyjqIV9W2aiP8SsuH3wXaHL9XyeAp/b8sJXwVZCMls4Bg==", null, false, "d4e5f6g7-h8i9-0123-defg-456789012jkl", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a1b2c3d4-e5f6-7890-abcd-123456789abc", "c3d4e5f6-g7h8-9012-cdef-345678901ghi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2c3d4e5-f6g7-8901-bcde-234567890def");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1b2c3d4-e5f6-7890-abcd-123456789abc", "c3d4e5f6-g7h8-9012-cdef-345678901ghi" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-7890-abcd-123456789abc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-g7h8-9012-cdef-345678901ghi");
        }
    }
}
