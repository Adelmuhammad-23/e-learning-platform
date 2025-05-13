using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addInfoInInstructorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Quizzes");

            migrationBuilder.AddColumn<string>(
                name: "Certificates",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "instructors",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certificates",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "instructors");

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Quizzes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
