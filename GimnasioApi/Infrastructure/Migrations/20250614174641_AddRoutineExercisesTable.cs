using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoutineExercisesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExercise_Exercises_ExerciseId",
                table: "RoutineExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExercise_Routines_RoutineId",
                table: "RoutineExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExercise",
                table: "RoutineExercise");

            migrationBuilder.RenameTable(
                name: "RoutineExercise",
                newName: "RoutineExercises");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExercise_RoutineId",
                table: "RoutineExercises",
                newName: "IX_RoutineExercises_RoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExercise_ExerciseId",
                table: "RoutineExercises",
                newName: "IX_RoutineExercises_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercises_Exercises_ExerciseId",
                table: "RoutineExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercises_Routines_RoutineId",
                table: "RoutineExercises",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExercises_Exercises_ExerciseId",
                table: "RoutineExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExercises_Routines_RoutineId",
                table: "RoutineExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises");

            migrationBuilder.RenameTable(
                name: "RoutineExercises",
                newName: "RoutineExercise");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExercises_RoutineId",
                table: "RoutineExercise",
                newName: "IX_RoutineExercise_RoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineExercises_ExerciseId",
                table: "RoutineExercise",
                newName: "IX_RoutineExercise_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExercise",
                table: "RoutineExercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercise_Exercises_ExerciseId",
                table: "RoutineExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercise_Routines_RoutineId",
                table: "RoutineExercise",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
