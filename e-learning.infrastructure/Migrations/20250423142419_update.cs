using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_learning.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_instructors_UserId",
                table: "instructors",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_AspNetUsers_UserId",
                table: "instructors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_AspNetUsers_UserId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_UserId",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "instructors");
        }
    }
}
