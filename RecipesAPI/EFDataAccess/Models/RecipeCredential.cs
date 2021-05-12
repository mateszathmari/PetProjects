using System.Collections.Generic;

namespace RecipesAPI.Models
{
    public class RecipeCredential
    {
        public Recipe Recipe { get; set; }
        public ICollection<string> RecipeHealthLabels { get; set; }
        public ICollection<string> RecipeIngredients { get; set; }

        public RecipeCredential()
        {
        }

        public RecipeCredential(Recipe recipe,
            ICollection<string> recipeHealthLabels, ICollection<string> recipeIngredients)
        {
            Recipe = recipe;
            RecipeHealthLabels = recipeHealthLabels;
            RecipeIngredients = recipeIngredients;
        }
    }
}