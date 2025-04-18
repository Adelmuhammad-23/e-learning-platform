using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addImageInCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "courses");
        }
    }
}
