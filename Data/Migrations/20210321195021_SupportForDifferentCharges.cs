using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class SupportForDifferentCharges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07b836b7-01fa-4998-a801-373d0b67945c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eb73319-e6fe-40f9-92e4-9932f75c9ffd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c230718b-de27-47c4-b3a6-99b9c065f1ef");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "CompletedPickups",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "CompletedPickups");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07b836b7-01fa-4998-a801-373d0b67945c", "f8e76a4c-e028-4fe1-8761-9e04fb702780", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c230718b-de27-47c4-b3a6-99b9c065f1ef", "943bfff9-f1b2-47f1-bb40-5f75b7757e2d", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6eb73319-e6fe-40f9-92e4-9932f75c9ffd", "dbd8de81-a623-4511-bd4a-10450d4cc321", "Employee", "EMPLOYEE" });
        }
    }
}
