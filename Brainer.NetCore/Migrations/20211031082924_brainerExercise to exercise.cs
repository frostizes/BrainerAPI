using Microsoft.EntityFrameworkCore.Migrations;

namespace Brainer.NetCore.Migrations
{
    public partial class brainerExercisetoexercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrainerExercises_ExerciseType_ExerciseTypeId",
                table: "BrainerExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_BrainerExercises_ExerciseId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrainerExercises",
                table: "BrainerExercises");

            migrationBuilder.RenameTable(
                name: "BrainerExercises",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_BrainerExercises_ExerciseTypeId",
                table: "Exercises",
                newName: "IX_Exercises_ExerciseTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseType_ExerciseTypeId",
                table: "Exercises",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Exercises_ExerciseId",
                table: "Question",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseType_ExerciseTypeId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Exercises_ExerciseId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "BrainerExercises");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ExerciseTypeId",
                table: "BrainerExercises",
                newName: "IX_BrainerExercises_ExerciseTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrainerExercises",
                table: "BrainerExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrainerExercises_ExerciseType_ExerciseTypeId",
                table: "BrainerExercises",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_BrainerExercises_ExerciseId",
                table: "Question",
                column: "ExerciseId",
                principalTable: "BrainerExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
