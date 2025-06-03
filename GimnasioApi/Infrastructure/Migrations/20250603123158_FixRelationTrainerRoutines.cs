using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationTrainerRoutines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Users_TrainerId",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_TrainerId",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Routines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Routines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Routines_TrainerId",
                table: "Routines",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Users_TrainerId",
                table: "Routines",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
