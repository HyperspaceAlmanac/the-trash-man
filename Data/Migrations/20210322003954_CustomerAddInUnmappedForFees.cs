using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class CustomerAddInUnmappedForFees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2cbfcc31-f7ab-4ce0-a23c-76b959c59986");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7392f0a0-9f5f-43f0-9c1c-5a916d7413ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80759b58-861c-4f61-92a3-1701c1394148");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80759b58-861c-4f61-92a3-1701c1394148", "97514d47-9cb2-4d25-98d0-4f241d08c503", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7392f0a0-9f5f-43f0-9c1c-5a916d7413ca", "224d246b-dd8f-46d7-b992-c542f58de19c", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2cbfcc31-f7ab-4ce0-a23c-76b959c59986", "22c847fe-33bd-4444-b785-de88254ab581", "Employee", "EMPLOYEE" });
        }
    }
}
