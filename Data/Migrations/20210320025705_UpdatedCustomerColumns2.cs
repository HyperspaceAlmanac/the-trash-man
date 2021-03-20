using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class UpdatedCustomerColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "963a8c64-1775-4d8c-9dbe-7e328942bd3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e69e52c-5b8d-437b-99a1-f96c03cbb06b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfb43890-2b34-4d81-a4f8-fecbe2c3e54d");

            migrationBuilder.CreateTable(
                name: "CompletedPickup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedPickup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedPickup_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneTimePickups",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimePickups", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_OneTimePickups_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5698eb35-a281-41a2-a471-2fdc50d3786c", "88a9a4cd-6bbb-46e0-b624-9474ea35929d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae441b70-8a09-4ebd-a8d3-888d306f4b87", "a630e7f2-1f8c-4ec9-a146-709347d0ab77", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9094786-3154-4978-8db0-6b28663ddb69", "5f6720b1-6ddb-4ef4-bd08-45f61d188a69", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedPickup_CustomerId",
                table: "CompletedPickup",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedPickup");

            migrationBuilder.DropTable(
                name: "OneTimePickups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5698eb35-a281-41a2-a471-2fdc50d3786c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae441b70-8a09-4ebd-a8d3-888d306f4b87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9094786-3154-4978-8db0-6b28663ddb69");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "963a8c64-1775-4d8c-9dbe-7e328942bd3c", "5efaf794-a65b-456d-b8ef-bab0de22ceb3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e69e52c-5b8d-437b-99a1-f96c03cbb06b", "d8036dfb-d69f-42cb-80e4-b12b8aaef0a0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dfb43890-2b34-4d81-a4f8-fecbe2c3e54d", "54ef742c-cebd-4a29-9b8f-ff5d045b342b", "Employee", "EMPLOYEE" });
        }
    }
}
