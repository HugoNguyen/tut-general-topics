using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstoreapp.api.Migrations
{
    public partial class SeededDefaultUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7519705c-1728-4494-8abd-d2e2516e445e", "61429727-904c-490a-9a62-7c16f355fdc4", "Administrator", "ADMINISTRATOR" },
                    { "aa37bb5b-6b00-4eca-a031-d4fa54c51dd5", "d346c870-0bb9-4cbb-8e09-9949d88fb6dd", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "35a2d921-b22a-4793-a181-ad6892bb61e2", 0, "5df1a124-827b-43f9-a639-294e2446531d", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEPx0LBcfv7dVejD0dW4RNhaxASm1VeQF8bByfnmC3gWGyNb6rlH/smDydysZ+THzMg==", null, false, "cc966321-b47b-4a84-bdb0-c7f8f7182842", false, "admin@bookstore.com" },
                    { "c71fa412-941e-45f4-87d2-faecbdeda4c0", 0, "4849d622-ba82-4b10-9ba5-dc130bb0f907", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEOTiWOgHg7PdEAwEKrhrnBnMw9gylBXRW+6z5AORESjfkremroGSS6HT7QXHGxOFUA==", null, false, "e3384f9b-13fd-4bb9-8ce8-13fbe3554f9e", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7519705c-1728-4494-8abd-d2e2516e445e", "35a2d921-b22a-4793-a181-ad6892bb61e2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "aa37bb5b-6b00-4eca-a031-d4fa54c51dd5", "c71fa412-941e-45f4-87d2-faecbdeda4c0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7519705c-1728-4494-8abd-d2e2516e445e", "35a2d921-b22a-4793-a181-ad6892bb61e2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "aa37bb5b-6b00-4eca-a031-d4fa54c51dd5", "c71fa412-941e-45f4-87d2-faecbdeda4c0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7519705c-1728-4494-8abd-d2e2516e445e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa37bb5b-6b00-4eca-a031-d4fa54c51dd5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35a2d921-b22a-4793-a181-ad6892bb61e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c71fa412-941e-45f4-87d2-faecbdeda4c0");
        }
    }
}
