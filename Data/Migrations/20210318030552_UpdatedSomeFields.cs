using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class UpdatedSomeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e88269d-a40b-4b58-bce5-3f1985b81e11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3537fbb9-2ffc-4ae9-8861-1fd272b2f522");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffd7c1a3-0f91-4fc4-bdaa-1ddc1f83f5fa");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "Employees",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3c20973-39c1-462a-8661-42713187e235", "59ebb60c-cea0-4963-a910-6fab7bed56d2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a314637b-7763-4030-a76f-963d50b3489c", "d6fdd712-be58-4b34-bc73-46be694bc7b6", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7959a799-7845-44bc-85c2-b09a1f3c890e", "f1199c0c-7f7f-4a46-9ccf-b3c6d7dea8c9", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7959a799-7845-44bc-85c2-b09a1f3c890e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a314637b-7763-4030-a76f-963d50b3489c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3c20973-39c1-462a-8661-42713187e235");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Employees",
                newName: "Zipcode");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ffd7c1a3-0f91-4fc4-bdaa-1ddc1f83f5fa", "f74c9748-954f-4451-9d1e-f8b3ceb760a8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3537fbb9-2ffc-4ae9-8861-1fd272b2f522", "076bc36b-de63-42d1-adf3-e73ee4de2738", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e88269d-a40b-4b58-bce5-3f1985b81e11", "534bcdc0-289a-4215-8d5e-d04705da927e", "Employee", "EMPLOYEE" });
        }
    }
}
