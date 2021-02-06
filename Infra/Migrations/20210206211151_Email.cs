using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("19e07b77-a73f-41df-91d6-0a8226c9cd5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("36bf22ec-7433-482b-864b-74f3d39066a9"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("312788d6-74cd-48e5-a338-31b27d0d2b74"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("edaa149f-cc1b-4dfa-82c9-88205233043d"), "0192023A7BBD73250516F069DF18B500", 1, "Escola" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("312788d6-74cd-48e5-a338-31b27d0d2b74"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("edaa149f-cc1b-4dfa-82c9-88205233043d"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Parents");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("19e07b77-a73f-41df-91d6-0a8226c9cd5c"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("36bf22ec-7433-482b-864b-74f3d39066a9"), "0192023A7BBD73250516F069DF18B500", 1, "Escola Frederico Rei" });
        }
    }
}
