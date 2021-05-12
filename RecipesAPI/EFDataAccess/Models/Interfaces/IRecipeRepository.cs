using System.Collections.Generic;

namespace RecipesAPI.Models
{
    public interface IRecipeRepository
    {
        Recipe AddFavoriteRecipeToUser(string username, int recipeId);
        Recipe DeleteFavoriteRecipeToUser(string username, int recipeId);
        void DeleteAllFavoriteUserRecipe(string username);
        Recipe DeleteRecipe(int recipeId);
        Recipe AddRecipe(RecipeCredential recipeCred);
        Recipe GetRecipe(int recipeId);
        Recipe UpdateRecipe(Recipe recipe);
        IEnumerable<Recipe> GetUserFavoriteRecipes(string username);
        Ingredient AddIngredient(string ingredient);
        HealthLabel AddHealthLabel(string healthLabel);
        Ingredient DeleteIngredient(int ingredientId);
        HealthLabel DeleteHealthLabel(int healthLabelId);
        Ingredient GetIngredient(int ingredientId);
        HealthLabel GetHealthLabel(int healthLabelId);
    }
}