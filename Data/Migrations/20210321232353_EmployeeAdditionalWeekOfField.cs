using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class EmployeeAdditionalWeekOfField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5af323be-992b-4edf-a069-604691f1413f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71338c71-b2b8-44f2-9dc7-b5e396aade8a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84a7a650-cadf-4e9e-bcc8-468759a3b81f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63a7b279-ba02-493f-aa17-3f1a735a4b3c", "e9d8d22d-6c65-4779-8258-28a9082db5d6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0dc7300f-9a67-4bcf-a298-3c8e6ba030a9", "3d3667be-c343-4c0b-8c16-721559da15f9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29b30e73-aaba-427d-8eb6-eada5be4ebbd", "e43642d1-c6f2-4e15-b398-2f269738b499", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dc7300f-9a67-4bcf-a298-3c8e6ba030a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29b30e73-aaba-427d-8eb6-eada5be4ebbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63a7b279-ba02-493f-aa17-3f1a735a4b3c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84a7a650-cadf-4e9e-bcc8-468759a3b81f", "048b64c3-fdaa-4a9b-bd4c-b04334b86bd5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5af323be-992b-4edf-a069-604691f1413f", "1d6a5a07-ec25-4c83-a306-b8f13370fd5f", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71338c71-b2b8-44f2-9dc7-b5e396aade8a", "a87d9d3e-2668-402a-a218-616a79893d20", "Employee", "EMPLOYEE" });
        }
    }
}
