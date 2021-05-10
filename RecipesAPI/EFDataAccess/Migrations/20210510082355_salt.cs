using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class salt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLabel_Recipes_Recipeid",
                table: "HealthLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipes_Recipeid",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthLabel",
                table: "HealthLabel");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameTable(
                name: "HealthLabel",
                newName: "HealthLabels");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_Recipeid",
                table: "Ingredients",
                newName: "IX_Ingredients_Recipeid");

            migrationBuilder.RenameIndex(
                name: "IX_HealthLabel_Recipeid",
                table: "HealthLabels",
                newName: "IX_HealthLabels_Recipeid");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthLabels",
                table: "HealthLabels",
                column: "Name");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthLabels_Recipes_Recipeid",
                table: "HealthLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_Recipeid",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthLabels",
                table: "HealthLabels");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "HealthLabels",
                newName: "HealthLabel");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_Recipeid",
                table: "Ingredient",
                newName: "IX_Ingredient_Recipeid");

            migrationBuilder.RenameIndex(
                name: "IX_HealthLabels_Recipeid",
                table: "HealthLabel",
                newName: "IX_HealthLabel_Recipeid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthLabel",
                table: "HealthLabel",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthLabel_Recipes_Recipeid",
                table: "HealthLabel",
                column: "Recipeid",
                principalTable: "Recipes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipes_Recipeid",
                table: "Ingredient",
                column: "Recipeid",
                principalTable: "Recipes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
