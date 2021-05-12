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

            UserRecipe userRecipes = new UserRecipe(user, recipe);
            user.UserRecipes.Add(userRecipes);
            recipe.UserRecipes.Add(userRecipes);
            _context.SaveChanges();
            return recipe;
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

        public Recipe DeleteFavoriteRecipeToUser(string username, int recipeId)
        {
            Recipe recipe = GetRecipe(recipeId);
            User user = GetUser(username);
            if (user == null || recipe == null)
            {
                return null;
            }

            var userRecipe = _context.UserRecipes.FirstOrDefault(x => x.Recipe.id == recipe.id && x.UserName == username);
            if (userRecipe != null)
            {
                _context.UserRecipes.Remove(userRecipe);
                _context.SaveChanges();
            }

            return recipe;
        }

        public IEnumerable<UserRecipe> GetUserRecipes(int recipeId)
        {
           return _context.UserRecipes.Where(x => x.RecipeId == recipeId);
        }

        public Recipe DeleteRecipe(int recipeId)
        {
            Recipe recipe = GetRecipe(recipeId);
            if (recipe != null)
            {
                var userRecipes = GetUserRecipes(recipeId);
                foreach (UserRecipe userRecipe in userRecipes)
                {
                    DeleteUserRecipes(userRecipe.Id);
                }
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }

            return recipe;
        }

        private void DeleteUserRecipes(int userRecipeId)
        {
            _context.UserRecipes.Remove(GetUserRecipe(userRecipeId));
            _context.SaveChanges();
        }

        private UserRecipe GetUserRecipe(int userRecipeId)
        {
            return _context.UserRecipes.Find(userRecipeId);
        }


        public Recipe AddRecipe(RecipeCredential recipeCred)
        {
            Recipe recipe = recipeCred.Recipe;
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            List<HealthLabel> healthLabels = _context.HealthLabels.ToList();
            foreach (string healthLabelString in recipeCred.RecipeHealthLabels)
            {
                
                HealthLabel healthLabel = healthLabels.FirstOrDefault(x => x.Name == healthLabelString);
                if (healthLabel == null)
                {
                     healthLabel = AddHealthLabel(healthLabelString);
                }

                RecipeHealthLabels recipeHealthLabel = new RecipeHealthLabels(healthLabel, recipe);
                recipe.RecipeHealthLabels.Add(recipeHealthLabel);
                healthLabel.RecipeHealthLabels.Add(recipeHealthLabel);
            }

            List<Ingredient> ingredients = _context.Ingredients.ToList();
            foreach (string ingredientString in recipeCred.RecipeIngredients)
            {
                Ingredient ingredient = ingredients.FirstOrDefault(x => x.Name == ingredientString);
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
                var favRecipes = GetUserFavoriteRecipes(user.UserName);
                foreach (Recipe favRecipe in favRecipes)
                {
                    DeleteFavoriteRecipeToUser(user.UserName, favRecipe.id);
                }
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