using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class DefaultSchoolUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("31c3cd22-afd1-45f4-830a-84f4b97c2cac"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("d59c1220-3a95-4494-8dac-e98f89d6bb70"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("f8f2e9e5-c459-4a04-9f15-b688a197bc8d"), "0192023A7BBD73250516F069DF18B500", 1, "Escola Frederico Rei" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d59c1220-3a95-4494-8dac-e98f89d6bb70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f8f2e9e5-c459-4a04-9f15-b688a197bc8d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("31c3cd22-afd1-45f4-830a-84f4b97c2cac"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });
        }
    }
}
