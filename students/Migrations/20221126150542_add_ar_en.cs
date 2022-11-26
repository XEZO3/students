using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace students.Migrations
{
    public partial class add_ar_en : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "title_ar",
                table: "CoursesVideo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "title_en",
                table: "CoursesVideo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_en",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_ar",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_en",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title_ar",
                table: "CoursesVideo");

            migrationBuilder.DropColumn(
                name: "title_en",
                table: "CoursesVideo");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Name_en",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Name_ar",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name_en",
                table: "Categories");
        }
    }
}
