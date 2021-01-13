using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class RenameAndMapStudentGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeStudentRelations");

            migrationBuilder.CreateTable(
                name: "StudentGrades",
                columns: table => new
                {
                    BaseGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Grade = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_StudentGrades_Grades_BaseGradeId",
                        column: x => x.BaseGradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGrades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrades_BaseGradeId",
                table: "StudentGrades",
                column: "BaseGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrades_StudentId",
                table: "StudentGrades",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGrades");

            migrationBuilder.CreateTable(
                name: "GradeStudentRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Grade = table.Column<double>(type: "float", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeStudentRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeStudentRelations_Grades_BaseGradeId",
                        column: x => x.BaseGradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradeStudentRelations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeStudentRelations_BaseGradeId",
                table: "GradeStudentRelations",
                column: "BaseGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeStudentRelations_StudentId",
                table: "GradeStudentRelations",
                column: "StudentId");
        }
    }
}
