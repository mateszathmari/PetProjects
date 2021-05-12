using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class manyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLabels_Recipes_Recipeid",
                table: "HealthLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_Recipeid",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_Recipeid",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_HealthLabels_Recipeid",
                table: "HealthLabels");

            migrationBuilder.DropColumn(
                name: "Recipeid",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Recipeid",
                table: "HealthLabels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Recipeid",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recipeid",
                table: "HealthLabels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Recipeid",
                table: "Ingredients",
                column: "Recipeid");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLabels_Recipeid",
                table: "HealthLabels",
                column: "Recipeid");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthLabels_Recipes_Recipeid",
                table: "HealthLabels",
                column: "Recipeid",
                principalTable: "Recipes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_Recipeid",
                table: "Ingredients",
                column: "Recipeid",
                principalTable: "Recipes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
