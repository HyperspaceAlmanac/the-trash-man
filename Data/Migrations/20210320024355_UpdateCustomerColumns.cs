using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class UpdateCustomerColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "237e9daf-ee9b-4d21-983d-a75bb624dea9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a7be373-4b67-4540-a44b-77b3307de8a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f041d30e-7d01-4eb4-8b00-97f70697bde3");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Customers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "963a8c64-1775-4d8c-9dbe-7e328942bd3c", "5efaf794-a65b-456d-b8ef-bab0de22ceb3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e69e52c-5b8d-437b-99a1-f96c03cbb06b", "d8036dfb-d69f-42cb-80e4-b12b8aaef0a0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dfb43890-2b34-4d81-a4f8-fecbe2c3e54d", "54ef742c-cebd-4a29-9b8f-ff5d045b342b", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "963a8c64-1775-4d8c-9dbe-7e328942bd3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e69e52c-5b8d-437b-99a1-f96c03cbb06b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfb43890-2b34-4d81-a4f8-fecbe2c3e54d");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "237e9daf-ee9b-4d21-983d-a75bb624dea9", "3f69e4f5-c571-4d73-8252-506f84c114b8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a7be373-4b67-4540-a44b-77b3307de8a5", "f478ed88-3bfa-4ffd-82de-95b8c0983964", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f041d30e-7d01-4eb4-8b00-97f70697bde3", "7fb5dade-9347-4afc-a7dd-2ad2b5dfe63b", "Employee", "EMPLOYEE" });
        }
    }
}
