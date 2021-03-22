using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class CustomerUnmappedCopletedPickups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
