using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class RecipeIngredients
    {
        [Key] public int Id { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public RecipeIngredients()
        {
        }

        public RecipeIngredients(Ingredient ingredient, Recipe recipe)
        {
            IngredientId = ingredient.Id;
            Ingredient = ingredient;
            RecipeId = recipe.id;
            Recipe = recipe;
        }
    }
}