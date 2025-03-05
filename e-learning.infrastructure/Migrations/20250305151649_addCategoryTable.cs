using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courses_CategoryId",
                table: "courses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_Category_CategoryId",
                table: "courses",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_Category_CategoryId",
                table: "courses");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_courses_CategoryId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "courses");
        }
    }
}
