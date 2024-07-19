using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FightForge.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Gyms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_CreatedById",
                table: "Gyms",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Users_CreatedById",
                table: "Gyms",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Users_CreatedById",
                table: "Gyms");

            migrationBuilder.DropIndex(
                name: "IX_Gyms_CreatedById",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Gyms");
        }
    }
}
