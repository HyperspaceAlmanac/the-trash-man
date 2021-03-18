using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class GeneralSetupTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PickupDay",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ae0078a-2955-49f3-9952-d43df72e99cc", "c7341b03-ec04-4198-91ed-95c46b7b1463", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60ba006b-bc54-4132-874c-4cb44a19ebfe", "39f4cbe0-efa5-4dc5-90de-318020696895", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d93f66e8-6369-4d0e-aee9-e302a2c88188", "817268ee-b60b-4e5a-a87b-d2aad1a3ab21", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ae0078a-2955-49f3-9952-d43df72e99cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60ba006b-bc54-4132-874c-4cb44a19ebfe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d93f66e8-6369-4d0e-aee9-e302a2c88188");

            migrationBuilder.DropColumn(
                name: "PickupDay",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "PikcupDay",
                table: "Customers",
                type: "int",
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
    }
}
