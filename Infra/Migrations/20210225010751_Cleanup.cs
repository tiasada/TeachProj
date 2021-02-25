using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Cleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33537837-6880-43f7-9c7f-40d7f69d1503"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("998c900f-83ae-4ac6-8418-e6a5c1abeb4c"));

            migrationBuilder.DropColumn(
                name: "SubjectsString",
                table: "Classrooms");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("c0369eba-962d-45f6-bcf7-c036525db992"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("59d5a81c-467a-4ab5-8a09-3f09972ec9ac"), "0192023A7BBD73250516F069DF18B500", 1, "Escola" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("59d5a81c-467a-4ab5-8a09-3f09972ec9ac"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c0369eba-962d-45f6-bcf7-c036525db992"));

            migrationBuilder.AddColumn<string>(
                name: "SubjectsString",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("998c900f-83ae-4ac6-8418-e6a5c1abeb4c"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("33537837-6880-43f7-9c7f-40d7f69d1503"), "0192023A7BBD73250516F069DF18B500", 1, "Escola" });
        }
    }
}
