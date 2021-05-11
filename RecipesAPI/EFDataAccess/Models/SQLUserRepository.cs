using System.Collections.Generic;
using System.Linq;
using EFDataAccess.DataAccess;

namespace RecipesAPI.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public SQLUserRepository(UserContext context)
        {
            _context = context;
        }

        public User GetUser(string username)
        {
            return _context.Users.Find(username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public Recipe AddFavoriteRecipeToUser(string username, int recipeId)
        {
            Recipe recipe = GetRecipe(recipeId);
            User user = GetUser(username);
            if (user == null || recipe == null)
            {
                return null;
            }

            UserRecipes userRecipes = new UserRecipes(user, recipe);
            user.UserRecipes.Add(userRecipes);
            recipe.UserRecipes.Add(userRecipes);
            _context.SaveChanges();
            return recipe;
        }

        public Recipe DeleteFavoriteRecipeToUser(string username, int recipeId)
        {
            Recipe recipe = GetRecipe(recipeId);
            User user = GetUser(username);
            if (user == null || recipe == null)
            {
                return null;
            }

            var userRecipe = _context.UserRecipes.FirstOrDefault(x => x.Recipe == recipe && x.UserName == username);
            if (userRecipe != null)
            {
                _context.UserRecipes.Remove(userRecipe);
                _context.SaveChanges();
            }

            return recipe;
        }

        public Recipe DeleteRecipe(int recipeId)
        {
            Recipe recipe = GetRecipe(recipeId);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }

            return recipe;
        }

        public Recipe AddRecipe(RecipeCredential recipeCred)
        {
            Recipe recipe = recipeCred.Recipe;
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            foreach (string healthLabelString in recipeCred.RecipeHealthLabels)
            {
                HealthLabel healthLabel = _context.HealthLabels.Find(healthLabelString);
                if (healthLabel == null)
                {
                     healthLabel = AddHealthLabel(healthLabelString);
                }

                RecipeHealthLabels recipeHealthLabel = new RecipeHealthLabels(healthLabel, recipe);
                recipe.RecipeHealthLabels.Add(recipeHealthLabel);
                healthLabel.RecipeHealthLabels.Add(recipeHealthLabel);
            }

            foreach (string ingredientString in recipeCred.RecipeIngredients)
            {
                Ingredient ingredient = _context.Ingredients.Find(ingredientString);
                if (ingredient == null)
                {
                    ingredient = AddIngredient(ingredientString);
                }

                RecipeIngredients recipeHealthLabel = new RecipeIngredients(ingredient, recipe);
                recipe.RecipeIngredients.Add(recipeHealthLabel);
                ingredient.RecipeIngredients.Add(recipeHealthLabel);
            }

            _context.SaveChanges();
            return recipe;
        }

        public Recipe GetRecipe(int recipeId)
        {
            return _context.Recipes.Find(recipeId);
        }

        public Recipe UpdateRecipe(Recipe recipeChanges)
        {
            var recipe = _context.Recipes.Attach(recipeChanges);
            recipe.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return recipeChanges;
        }

        public IEnumerable<Recipe> GetUserFavoriteRecipes(string username)
        {
            User user = GetUser(username);
            if (user == null)
            {
                return null;
            }

            return _context.UserRecipes.Where((x) => x.UserName == user.UserName).Select(x => x.Recipe).ToList();
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateUser(User userChanges)
        {
            var user = _context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return userChanges;
        }

        public User DeleteUser(string username)
        {
            User user = _context.Users.Find(username);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return user;
        }

        public string GenerateTokenForUser(string userName)
        {
            User user = _context.Users.Find(userName);
            if (user != null)
            {
                user.GenerateToken();
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return user.Token;
        }

        public string DeleteUserToken(string userName)
        {
            User user = _context.Users.Find(userName);
            if (user != null)
            {
                user.DeleteToken();
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return user.Token;
        }

        public Ingredient AddIngredient(string ingredient)
        {
            Ingredient newIngredient = new Ingredient(ingredient);
            _context.Ingredients.Add(newIngredient);
            _context.SaveChanges();
            return newIngredient;
        }

        public HealthLabel AddHealthLabel(string healthLabel)
        {
            HealthLabel newHealthLabel = new HealthLabel(healthLabel);
            _context.HealthLabels.Add(newHealthLabel);
            _context.SaveChanges();
            return newHealthLabel;
        }

        public Ingredient DeleteIngredient(int ingredientId)
        {
            Ingredient ingredient = GetIngredient(ingredientId);
            if (ingredient == null)
            {
                return null;
            }

            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();
            return ingredient;
        }

        public HealthLabel DeleteHealthLabel(int healthLabelId)
        {
            HealthLabel healthLabel = GetHealthLabel(healthLabelId);
            if (healthLabel == null)
            {
                return null;
            }

            _context.HealthLabels.Remove(healthLabel);
            _context.SaveChanges();
            return healthLabel;
        }

        public Ingredient GetIngredient(int ingredientId)
        {
            return _context.Ingredients.Find(ingredientId);
        }

        public HealthLabel GetHealthLabel(int healthLabelId)
        {
            return _context.HealthLabels.Find(healthLabelId);
        }
    }
}