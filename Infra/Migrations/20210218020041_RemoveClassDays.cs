using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class RemoveClassDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPresences_ClassDays_ClassDayId",
                table: "StudentPresences");

            migrationBuilder.DropTable(
                name: "ClassDays");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69be23a6-d494-4a8c-8ec8-f9aa8f43ebfa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c7e1c11-b8c6-4c4c-a7e7-ae2ef232120b"));

            migrationBuilder.RenameColumn(
                name: "ClassDayId",
                table: "StudentPresences",
                newName: "ClassroomId");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "StudentPresences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("998c900f-83ae-4ac6-8418-e6a5c1abeb4c"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("33537837-6880-43f7-9c7f-40d7f69d1503"), "0192023A7BBD73250516F069DF18B500", 1, "Escola" });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPresences_Classrooms_ClassroomId",
                table: "StudentPresences",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPresences_Classrooms_ClassroomId",
                table: "StudentPresences");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33537837-6880-43f7-9c7f-40d7f69d1503"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("998c900f-83ae-4ac6-8418-e6a5c1abeb4c"));

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "StudentPresences",
                newName: "ClassDayId");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "StudentPresences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClassDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassDays_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("8c7e1c11-b8c6-4c4c-a7e7-ae2ef232120b"), "0192023A7BBD73250516F069DF18B500", 0, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Profile", "Username" },
                values: new object[] { new Guid("69be23a6-d494-4a8c-8ec8-f9aa8f43ebfa"), "0192023A7BBD73250516F069DF18B500", 1, "Escola" });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassDays_ClassroomId",
                table: "ClassDays",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPresences_ClassDays_ClassDayId",
                table: "StudentPresences",
                column: "ClassDayId",
                principalTable: "ClassDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
