using System.Collections.Generic;

namespace RecipesAPI.Models
{
    public interface IUserRepository
    {
        User GetUser(string username);
        IEnumerable<User> GetUsers();
        Recipe AddFavoriteRecipeToUser(User user, Recipe recipe);
        List<Recipe> GetUserFavoriteRecipes(User user, Recipe recipe);
        User AddUser(User person);
        User updateUser(User personChanges);
        User DeleteUser(string username);
        string GenerateTokenForUser(string userName);
        string DeleteUserToken(string userName);
    }
}