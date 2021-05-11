using System.Collections.Generic;

namespace RecipesAPI.Models
{
    public interface IUserRepository
    {
        User GetUser(string username);
        IEnumerable<User> GetUsers();
        Recipe AddFavoriteRecipeToUser(string username, int recipeId);
        Recipe DeleteFavoriteRecipeToUser(string username, int recipeId);
        Recipe DeleteRecipe(int recipeId);
        Recipe AddRecipe(RecipeCredential recipeCred);
        Recipe GetRecipe(int recipeId);
        Recipe UpdateRecipe(Recipe recipe);
        IEnumerable<Recipe> GetUserFavoriteRecipes(string username);
        User AddUser(User person);
        User UpdateUser(User personChanges);
        User DeleteUser(string username);
        string GenerateTokenForUser(string userName);
        string DeleteUserToken(string userName);
        Ingredient AddIngredient(string ingredient);
        HealthLabel AddHealthLabel(string healthLabel);
        Ingredient DeleteIngredient(int ingredientId);
        HealthLabel DeleteHealthLabel(int healthLabelId);
        Ingredient GetIngredient(int ingredientId);
        HealthLabel GetHealthLabel(int healthLabelId);
    }
}