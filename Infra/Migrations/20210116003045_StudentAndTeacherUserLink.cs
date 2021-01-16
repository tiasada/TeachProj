using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class StudentAndTeacherUserLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45cb8879-04eb-4975-832f-c9947fbe92f2"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Teachers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("46daea7f-f03e-4e07-adb6-b904fb5e34ee"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentId",
                table: "Students",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_ParentId",
                table: "Students",
                column: "ParentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_ParentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_ParentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46daea7f-f03e-4e07-adb6-b904fb5e34ee"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("45cb8879-04eb-4975-832f-c9947fbe92f2"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });
        }
    }
}
