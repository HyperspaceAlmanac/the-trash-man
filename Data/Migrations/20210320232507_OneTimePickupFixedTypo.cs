using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class OneTimePickupFixedTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "740c45cb-77a8-4602-973b-a90982875e52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97c82caa-dede-4681-b0d1-f4dee88cd5f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1ba4399-5f3e-42da-b1f3-07ab3d81f738");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ec34a3b0-46d5-4b96-85e8-7e3b829ad383", "2c41a7bc-2bec-4911-b680-b631fa26f7ae", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41e21dbd-d12d-40ed-843f-a2f0260493d4", "b975d0fd-6dbc-4872-82bb-d72ddb9387ad", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bdb113f-2cb0-48b0-b2a7-00dbf32e4c2c", "cb96ed1e-9d18-4ce5-a9cb-9e88af00068f", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41e21dbd-d12d-40ed-843f-a2f0260493d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bdb113f-2cb0-48b0-b2a7-00dbf32e4c2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec34a3b0-46d5-4b96-85e8-7e3b829ad383");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97c82caa-dede-4681-b0d1-f4dee88cd5f9", "68dff992-6d7f-4ca1-a654-cece31dfdbc1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1ba4399-5f3e-42da-b1f3-07ab3d81f738", "3e0ca9cb-eef6-44bb-a833-2d3e3171dad7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "740c45cb-77a8-4602-973b-a90982875e52", "ab17fbc4-6590-4c49-8789-7dacc8c339c9", "Employee", "EMPLOYEE" });
        }
    }
}
