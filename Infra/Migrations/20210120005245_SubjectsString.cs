using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class SubjectsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46daea7f-f03e-4e07-adb6-b904fb5e34ee"));

            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "Classrooms");

            migrationBuilder.AddColumn<string>(
                name: "SubjectsString",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("b831cb3d-5e4f-4571-92d9-ae9823c2584c"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b831cb3d-5e4f-4571-92d9-ae9823c2584c"));

            migrationBuilder.DropColumn(
                name: "SubjectsString",
                table: "Classrooms");

            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("46daea7f-f03e-4e07-adb6-b904fb5e34ee"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });
        }
    }
}
