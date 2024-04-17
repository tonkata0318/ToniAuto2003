using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToniAuto2003.Infrastructure.Migrations
{
    public partial class AdminAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cf5efba-22e4-4e83-9fef-cc5bf0f8d43d", "Guest", "Guestov", "AQAAAAEAACcQAAAAECJdu9o4QP/fufGmLzGeXp13Ae9RvbfniaZOE7EHLbHYfC5kT0q+ICPgQxEXPIzUsw==", "cc18ea7e-18c3-4bcc-a67e-c116a4fdf996" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bba5506-34ab-4832-86df-705335ec2e07", "Agent", "Agentov", "AQAAAAEAACcQAAAAEJ291AtGYca9wG2py6PhZVoXcJ9OAAxqRJQLR1MgQcvu2wKBASHw18wCkcyLI7QyNQ==", "db4b9a34-f096-41f7-9976-abf2c03fb3c5" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7e568a55-bc9c-4547-85b9-a9e4170f5ba2", 0, "90221158-ab62-4720-9b88-02dd4bbf4d9e", "admin@mail.com", false, "Admin", "Adminov", false, null, "admin@mail.com", "admin@mail.com", "AQAAAAEAACcQAAAAEFqy8glKrriljTVJ5+wU+CXt1nRk+fcje5wtQE9tyr6yXoQnNxSDBHAymwwtP1RRNg==", null, false, "7c83750c-746c-4d05-a718-f8c688489dbd", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 3, "+3598517788", "7e568a55-bc9c-4547-85b9-a9e4170f5ba2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e568a55-bc9c-4547-85b9-a9e4170f5ba2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b7ec34b-617f-439f-88af-282143ed64e7", "", "", "AQAAAAEAACcQAAAAEAxwii2vFD8f4Olkh6XQ13sILUAukLQRm98pU8FZF8L+RZ67sYZd9MImnkQa3rwUxA==", "5fcfbca4-28fe-4019-9064-bf305413b6a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f2b4246-7e34-4af4-92ed-229c9822516f", "", "", "AQAAAAEAACcQAAAAEPw4pPJiWjqaTgmm8bWq3XzVs1U/a1W6iIpoRI3yHRbpM+Kcdq+tRzV26rH/IJ0rww==", "f382642b-9789-4858-b663-0fe7bd0cef2d" });
        }
    }
}
