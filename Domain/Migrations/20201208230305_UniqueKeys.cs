using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class UniqueKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Registration",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CPF",
                table: "Users",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CPF",
                table: "Teachers",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CPF",
                table: "Students",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Registration",
                table: "Students",
                column: "Registration",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CPF",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_CPF",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_CPF",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Registration",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Registration",
                table: "Students");
        }
    }
}
