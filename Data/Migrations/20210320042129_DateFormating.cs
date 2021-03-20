using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class DateFormating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5698eb35-a281-41a2-a471-2fdc50d3786c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae441b70-8a09-4ebd-a8d3-888d306f4b87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9094786-3154-4978-8db0-6b28663ddb69");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d9ee533-2e12-43cc-b339-d76c8270f8ec", "45feb973-a462-4644-b3ea-59af201d748c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51464283-4920-4fde-b246-497ec71b7b1d", "5e3438c0-93c6-4856-a225-25cd616a996d", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8aff85be-a806-4b2c-a04c-d1f782c8579a", "ab96a70d-1f67-4501-ae2d-83da4575e49a", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d9ee533-2e12-43cc-b339-d76c8270f8ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51464283-4920-4fde-b246-497ec71b7b1d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8aff85be-a806-4b2c-a04c-d1f782c8579a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5698eb35-a281-41a2-a471-2fdc50d3786c", "88a9a4cd-6bbb-46e0-b624-9474ea35929d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae441b70-8a09-4ebd-a8d3-888d306f4b87", "a630e7f2-1f8c-4ec9-a146-709347d0ab77", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9094786-3154-4978-8db0-6b28663ddb69", "5f6720b1-6ddb-4ef4-bd08-45f61d188a69", "Employee", "EMPLOYEE" });
        }
    }
}
