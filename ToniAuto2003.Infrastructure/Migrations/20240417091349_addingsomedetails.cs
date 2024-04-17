using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToniAuto2003.Infrastructure.Migrations
{
    public partial class addingsomedetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leasings_Agents_AgentId",
                table: "Leasings");

            migrationBuilder.AddColumn<int>(
                name: "AgentId1",
                table: "Leasings",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Leasings_AgentId1",
                table: "Leasings",
                column: "AgentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Leasings_Agents_AgentId",
                table: "Leasings",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leasings_Agents_AgentId1",
                table: "Leasings",
                column: "AgentId1",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leasings_Agents_AgentId",
                table: "Leasings");

            migrationBuilder.DropForeignKey(
                name: "FK_Leasings_Agents_AgentId1",
                table: "Leasings");

            migrationBuilder.DropIndex(
                name: "IX_Leasings_AgentId1",
                table: "Leasings");

            migrationBuilder.DropColumn(
                name: "AgentId1",
                table: "Leasings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21affc8c-cfdd-4ad9-90a9-885435c5986f", "AQAAAAEAACcQAAAAEOeLn+q4/OXH5oTemBxG7hIdk+327QqXBNrRwVC3hyRrBbNeNGsgMSKy1AK1Wd/bTQ==", "44b999e6-594b-4c3e-843b-470fa636ce34" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4ffd113-eddb-454e-a244-124ec7403580", "AQAAAAEAACcQAAAAEPuRQIqNjtergBxJISKmuPasRNka0noGD6NrGtKbgzz3GjDObfgBmzP/qkHVywQuGw==", "e663d0c4-6083-44a1-8d6c-c9a1f04fa625" });

            migrationBuilder.AddForeignKey(
                name: "FK_Leasings_Agents_AgentId",
                table: "Leasings",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
