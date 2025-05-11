using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addquizzestomodule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ModuleId",
                table: "Quizzes",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Modules_ModuleId",
                table: "Quizzes",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Modules_ModuleId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_ModuleId",
                table: "Quizzes");
        }
    }
}
