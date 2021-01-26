using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class PhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abe0145e-d7d4-439b-8aeb-1f6ee6c0b01d"));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("31c3cd22-afd1-45f4-830a-84f4b97c2cac"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PhoneNumber",
                table: "Teachers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PhoneNumber",
                table: "Students",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_PhoneNumber",
                table: "Parents",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_PhoneNumber",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_PhoneNumber",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_PhoneNumber",
                table: "Parents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("31c3cd22-afd1-45f4-830a-84f4b97c2cac"));

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Parents");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("abe0145e-d7d4-439b-8aeb-1f6ee6c0b01d"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });
        }
    }
}
