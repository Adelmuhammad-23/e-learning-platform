using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModuleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_videos_courses_CourseId",
                table: "videos");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "videos",
                newName: "ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_videos_CourseId",
                table: "videos",
                newName: "IX_videos_ModuleId");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseId",
                table: "Modules",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_videos_Modules_ModuleId",
                table: "videos",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_videos_Modules_ModuleId",
                table: "videos");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "videos",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_videos_ModuleId",
                table: "videos",
                newName: "IX_videos_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_videos_courses_CourseId",
                table: "videos",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
