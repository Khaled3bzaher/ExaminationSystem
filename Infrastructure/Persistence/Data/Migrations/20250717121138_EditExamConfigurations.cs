using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditExamConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultsStatus",
                table: "ExamResults");

            migrationBuilder.AddColumn<int>(
                name: "ExamStatus",
                table: "StudentExams",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "ExamConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 60);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamStatus",
                table: "StudentExams");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "ExamConfigurations");

            migrationBuilder.AddColumn<int>(
                name: "ResultsStatus",
                table: "ExamResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
