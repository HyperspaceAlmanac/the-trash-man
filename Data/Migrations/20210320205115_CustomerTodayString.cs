using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class CustomerTodayString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "OneTimePickup",
                table: "CompletedPickup",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTimePickup",
                table: "CompletedPickup");

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
    }
}
