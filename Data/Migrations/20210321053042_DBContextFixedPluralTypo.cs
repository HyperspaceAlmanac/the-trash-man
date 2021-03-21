using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class DBContextFixedPluralTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedPickup_Customers_CustomerId",
                table: "CompletedPickup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedPickup",
                table: "CompletedPickup");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f8e3707-084e-4ede-8a95-e6273719b2b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0c485cc-f034-4bea-84f8-dd2509a6177b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d561d46f-7685-4d5f-b1d0-2ebc2d946242");

            migrationBuilder.RenameTable(
                name: "CompletedPickup",
                newName: "CompletedPickups");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedPickup_CustomerId",
                table: "CompletedPickups",
                newName: "IX_CompletedPickups_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedPickups",
                table: "CompletedPickups",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedPickups_Customers_CustomerId",
                table: "CompletedPickups",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedPickups_Customers_CustomerId",
                table: "CompletedPickups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedPickups",
                table: "CompletedPickups");

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

            migrationBuilder.RenameTable(
                name: "CompletedPickups",
                newName: "CompletedPickup");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedPickups_CustomerId",
                table: "CompletedPickup",
                newName: "IX_CompletedPickup_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedPickup",
                table: "CompletedPickup",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d561d46f-7685-4d5f-b1d0-2ebc2d946242", "61661396-b264-465f-af81-e3d408580062", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f8e3707-084e-4ede-8a95-e6273719b2b5", "e9b49abc-ec79-4907-9b18-ea66d49ea981", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0c485cc-f034-4bea-84f8-dd2509a6177b", "37e2d66a-4841-4236-8f57-642dd2212bcf", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedPickup_Customers_CustomerId",
                table: "CompletedPickup",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
