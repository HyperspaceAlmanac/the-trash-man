using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class OneTimePickupUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33b97c10-4073-4c2b-8e8e-c077994f127e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9091db42-41e3-4e6d-929f-92c06e24b70e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e76c6123-9da1-4794-bfe1-e9d368bb339b");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "e76c6123-9da1-4794-bfe1-e9d368bb339b", "48eac937-e883-4a8c-a84e-ad1cbd6538e3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "33b97c10-4073-4c2b-8e8e-c077994f127e", "c3462fb7-cc6c-4664-8634-abf66c830034", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9091db42-41e3-4e6d-929f-92c06e24b70e", "85f03b30-e0b3-4e2d-a4b6-1d324fe19456", "Employee", "EMPLOYEE" });
        }
    }
}
