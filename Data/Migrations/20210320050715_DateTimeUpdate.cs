using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class DateTimeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OneTimePickups",
                table: "OneTimePickups");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OneTimePickups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CompletedPickup",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OneTimePickups",
                table: "OneTimePickups",
                columns: new[] { "CustomerId", "Date" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd0b24fe-19a2-41ff-a3e1-8980f00a726c", "87347ee9-6d86-4330-9dc2-503be09f7537", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4e75fab-21dc-4ec3-abf5-06146899a8d6", "399f1978-a19b-4870-ad1a-a1a527e1039c", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ae4ab01-0ff7-4159-90ab-8040f7ef1f39", "a7957835-4e32-475e-8581-254c2dcc8100", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OneTimePickups",
                table: "OneTimePickups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ae4ab01-0ff7-4159-90ab-8040f7ef1f39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e75fab-21dc-4ec3-abf5-06146899a8d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd0b24fe-19a2-41ff-a3e1-8980f00a726c");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "OneTimePickups");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CompletedPickup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OneTimePickups",
                table: "OneTimePickups",
                column: "CustomerId");

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
    }
}
