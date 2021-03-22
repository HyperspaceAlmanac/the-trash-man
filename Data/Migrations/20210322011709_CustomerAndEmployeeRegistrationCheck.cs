using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class CustomerAndEmployeeRegistrationCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05392a33-e1d6-4199-bb4f-4179161815b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b7b9f77-ee3a-41e5-8aff-ea72429b5386");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "922d0de7-e2a9-4765-86a4-c017151c7484");

            migrationBuilder.AddColumn<bool>(
                name: "CompletedRegistration",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CompletedRegistration",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0f3f8561-ba6a-4530-b5e4-9e66425089de", "a0b6873b-bcfd-4880-8987-3a403620f5f2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff2a00f0-9394-4ebf-9546-135cd2ab6500", "5d137252-b432-4cba-a5ec-6b1150addd1a", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "135fc814-e4c8-4423-b037-6b28a1eb96d0", "3a9c7601-e621-457d-8f50-7572109c09a7", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f3f8561-ba6a-4530-b5e4-9e66425089de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "135fc814-e4c8-4423-b037-6b28a1eb96d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff2a00f0-9394-4ebf-9546-135cd2ab6500");

            migrationBuilder.DropColumn(
                name: "CompletedRegistration",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CompletedRegistration",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b7b9f77-ee3a-41e5-8aff-ea72429b5386", "65d53d4c-cec6-45b0-82cd-8bdf8862df73", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05392a33-e1d6-4199-bb4f-4179161815b3", "e813f2f3-2ed0-4c61-901b-7c853a197ca9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "922d0de7-e2a9-4765-86a4-c017151c7484", "7a1f8cd0-b849-4d95-8376-064dd8aa122d", "Employee", "EMPLOYEE" });
        }
    }
}
