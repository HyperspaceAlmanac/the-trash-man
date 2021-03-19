using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class AdditionalCustomerServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09b0379c-b957-4640-a2f9-8986f86e13ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "971c040a-1107-47c9-a656-f926a689600f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bda7899a-0ba9-4fb5-a225-2a34bf14ca50");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "971c040a-1107-47c9-a656-f926a689600f", "c0f07e21-adb4-46db-a4cd-8218cdb2f0a1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bda7899a-0ba9-4fb5-a225-2a34bf14ca50", "030f6f5e-6a26-4e90-94f4-a15fe4f04a52", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09b0379c-b957-4640-a2f9-8986f86e13ce", "ed9d61c7-8753-456f-a536-5f6ac339ecc7", "Employee", "EMPLOYEE" });
        }
    }
}
