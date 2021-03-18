using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class GeneralSetupOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PikcupDay",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40a5ec97-2060-4b56-8e41-18ef1e52bde3", "daaca4c2-cb12-427c-8549-ff6b448e0a5c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee6b5cd8-3eb6-4cfd-9bd1-46d59966f443", "7cd5edb3-2925-4100-91ec-2610a24d2256", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54d011b0-1a2f-4c62-8b55-405044660e67", "a4c83382-4499-4843-bea9-0c3e444fc812", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40a5ec97-2060-4b56-8e41-18ef1e52bde3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54d011b0-1a2f-4c62-8b55-405044660e67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee6b5cd8-3eb6-4cfd-9bd1-46d59966f443");

            migrationBuilder.DropColumn(
                name: "PikcupDay",
                table: "Customers");

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
    }
}
