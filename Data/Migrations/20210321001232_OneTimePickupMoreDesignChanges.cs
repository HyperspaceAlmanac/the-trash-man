using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class OneTimePickupMoreDesignChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41e21dbd-d12d-40ed-843f-a2f0260493d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bdb113f-2cb0-48b0-b2a7-00dbf32e4c2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec34a3b0-46d5-4b96-85e8-7e3b829ad383");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0950c3d-52cc-4550-8349-92fcbe774cf8", "9a99b221-11e6-4be3-8667-83417ac2e1f6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e2140f5-aa75-4be2-b623-d886b2118322", "7d7ec01c-0321-48d5-9cd5-1e0cbce13dbb", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd7a9198-dd77-4c71-99df-5bcd935bc395", "f930eaab-f9bc-4c10-b0fc-a8d82b02ad23", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e2140f5-aa75-4be2-b623-d886b2118322");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0950c3d-52cc-4550-8349-92fcbe774cf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd7a9198-dd77-4c71-99df-5bcd935bc395");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ec34a3b0-46d5-4b96-85e8-7e3b829ad383", "2c41a7bc-2bec-4911-b680-b631fa26f7ae", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41e21dbd-d12d-40ed-843f-a2f0260493d4", "b975d0fd-6dbc-4872-82bb-d72ddb9387ad", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bdb113f-2cb0-48b0-b2a7-00dbf32e4c2c", "cb96ed1e-9d18-4ce5-a9cb-9e88af00068f", "Employee", "EMPLOYEE" });
        }
    }
}
