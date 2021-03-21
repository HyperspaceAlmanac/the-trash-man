using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class EmployeeFieldsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "SimulatedDay",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseSimulatedDay",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d561d46f-7685-4d5f-b1d0-2ebc2d946242", "61661396-b264-465f-af81-e3d408580062", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f8e3707-084e-4ede-8a95-e6273719b2b5", "e9b49abc-ec79-4907-9b18-ea66d49ea981", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0c485cc-f034-4bea-84f8-dd2509a6177b", "37e2d66a-4841-4236-8f57-642dd2212bcf", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f8e3707-084e-4ede-8a95-e6273719b2b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0c485cc-f034-4bea-84f8-dd2509a6177b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d561d46f-7685-4d5f-b1d0-2ebc2d946242");

            migrationBuilder.DropColumn(
                name: "SimulatedDay",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UseSimulatedDay",
                table: "Employees");

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
    }
}
