using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class AddUnmappedDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b244b14-48a5-4f07-bf8a-2970519ed5ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70579549-75a0-43eb-a70f-9745cc9685c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb827c7e-b20e-4380-bf73-0610a15520e6");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "5b244b14-48a5-4f07-bf8a-2970519ed5ed", "670c714e-744d-439d-8d3d-4536cde97e9a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70579549-75a0-43eb-a70f-9745cc9685c5", "4da5667e-72bd-48c6-a70b-28755b01cd79", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb827c7e-b20e-4380-bf73-0610a15520e6", "72500f7b-1aaa-4a1b-9164-4b9d91383154", "Employee", "EMPLOYEE" });
        }
    }
}
