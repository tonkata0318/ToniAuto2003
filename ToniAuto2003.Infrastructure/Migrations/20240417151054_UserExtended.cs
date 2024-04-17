using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToniAuto2003.Infrastructure.Migrations
{
    public partial class UserExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad075bb3-86d7-4d87-8c7e-dde12edc44d9", "AQAAAAEAACcQAAAAEEaUtQGBh0hVVeTvDhJGvGy6Q6fkAlJyx7c0+i5R/DEb1QBDfebKTI2eUnOJyD0/xA==", "2f727617-77a5-4f74-9c92-d9906b987dd9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9085e706-dd60-439d-abea-d651e827c6cc", "AQAAAAEAACcQAAAAEFFqnkE4PaBYSduJsX/35StZMUNFH2OL7hM40LQt+IeiAKKw2UA9z3wEaZ20d0VWmw==", "970952e4-987d-48c4-91d8-70f2973cb1b4" });
        }
    }
}
