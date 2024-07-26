using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FightForge.Migrations
{
    /// <inheritdoc />
    public partial class Trainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Sports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_TrainerId",
                table: "Sports",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Users_TrainerId",
                table: "Sports",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Users_TrainerId",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Sports_TrainerId",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Sports");
        }
    }
}
