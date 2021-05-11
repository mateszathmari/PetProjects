using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly UserContext _context;
        private readonly SQLUserRepository _sqlUserHandler;

        public ApiController(ILogger<ApiController> logger, UserContext context)
        {
            _logger = logger;
            _sqlUserHandler = new SQLUserRepository(context);
        }


        [HttpPost("login")]
        public string Login(UserCredential userCred)
        {
            User loginningUser = _sqlUserHandler.GetUser(userCred.Username);
            if (loginningUser == null)
            {
                return null;
            }

            if (loginningUser.IsValidPassword(userCred.Password))
            {
                string token = _sqlUserHandler.GenerateTokenForUser(userCred.Username);
                return token;
            }

            return null;
        }

        [HttpPost("logout")]
        public IActionResult Logout(AuthenticationCredential authentication)
        {
            User user = _sqlUserHandler.GetUser(authentication.Username);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.IsValidToken(authentication.Token))
            {
                _sqlUserHandler.DeleteUserToken(authentication.Username);
                return Ok("successfully logged out");
            }

            return BadRequest();
        }

        [HttpPost("registration")]
        public IActionResult Registration(RegistrationCredential registrationCred)
        {
            User loginningUserName = _sqlUserHandler.GetUser(registrationCred.UserName);
            bool isLoginningUserEmailTaken = _sqlUserHandler.GetUsers().Any(x => x.Email == registrationCred.Email);
            if (loginningUserName != null || isLoginningUserEmailTaken)
            {
                return BadRequest("username or email already taken");
            }

            Address userAddress = new Address(registrationCred.City, registrationCred.Street,
                registrationCred.HouseNumber, registrationCred.PostCode);
            User user = new User(registrationCred.UserName, registrationCred.Email, userAddress,
                registrationCred.Password); // we should validate the password as well if it strong enough
            _sqlUserHandler.AddUser(user);
            return Ok("successful registration");
        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser(UserCredential userCred)
        {
            User loginningUser = _sqlUserHandler.GetUser(userCred.Username);
            if (loginningUser == null)
            {
                return BadRequest();
            }

            if (loginningUser.IsValidPassword(userCred.Password))
            {
                _sqlUserHandler.DeleteUser(userCred.Username);
                return Ok("user successfully deleted");
            }

            return BadRequest();
        }

        [HttpGet("favorite-recipes")]
        public List<Recipe> GetFavoriteRecipes(AuthenticationCredential authenticationCredential)
        {
            User loginningUser = _sqlUserHandler.GetUser(authenticationCredential.Username);
            if (loginningUser == null)
            {
                return null;
            }

            if (loginningUser.IsValidToken(authenticationCredential.Token))
            {
                //return _sqlUserHandler.GetUser(authenticationCredential.Username).Recipes;
            }

            return null;
        }

        [HttpPut("favorite-recipes")]
        public List<Recipe> AddFavoriteRecipes(AddFavoriteRecipeCredential favoriteRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(favoriteRecipeCredential.Username);
            if (user == null)
            {
                return null;
            }

            if (user.IsValidToken(favoriteRecipeCredential.AuthenticationCredential.Token))
            {
                _sqlUserHandler.AddFavoriteRecipeToUser(user.UserName, favoriteRecipeCredential.Recipe.id);
                //return _sqlUserHandler.GetUser(favoriteRecipeCredential.Username).Recipes;
            }

            return null;
        }

        [HttpDelete("favorite-recipes")]
        public List<Recipe> DeleteFavoriteRecipes(DeleteFavoriteRecipeCredential deleteFavoriteRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(deleteFavoriteRecipeCredential.AuthenticationCredential.Username);
            if (user == null)
            {
                return null;
            }

            if (user.IsValidToken(deleteFavoriteRecipeCredential.AuthenticationCredential.Token))
            {
                _sqlUserHandler.DeleteFavoriteRecipeToUser(user.UserName, deleteFavoriteRecipeCredential.RecipeId);
                //return _sqlUserHandler.GetUser(deleteFavoriteRecipeCredential.AuthenticationCredential.Username).Recipes;
            }

            return null;
        }

        [HttpPost("healthLabel-label")]
        public int AddHealthLabel(HealthLabel healthLabel)
        {
            return _sqlUserHandler.AddHealthLabel(healthLabel.Name).Id;
        }

        [HttpGet("ingredient/{ingredientId:int}")]
        public Ingredient GetIngredient(int ingredientId)
        {
            return _sqlUserHandler.GetIngredient(ingredientId);
        }

        [HttpGet("healthLabel-label/{healthLabelId:int}")]
        public HealthLabel GetHealthLabel(int healthLabelId)
        {
            return _sqlUserHandler.GetHealthLabel(healthLabelId);
        }

        [HttpPost("ingredient")]
        public int AddIngredient(Ingredient ingredient)
        {
            return _sqlUserHandler.AddIngredient(ingredient.Name).Id;
        }

        [HttpDelete("ingredient/{ingredientId:int}")]
        public Ingredient DeleteIngredient(int ingredientId)
        {
            return _sqlUserHandler.DeleteIngredient(ingredientId);
        }

        [HttpDelete("healthLabel-label/{healthLabelId:int}")]
        public HealthLabel DeleteHealthLabel(int healthLabelId)
        {
            return _sqlUserHandler.DeleteHealthLabel(healthLabelId);
        }

        [HttpPost("recipe")]
        public Recipe AddRecipe(AddRecipeCredential addRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(addRecipeCredential.AuthenticationCredential.Username);
            if (user == null)
            {
                return null;
            }

            if (!user.IsValidToken(addRecipeCredential.AuthenticationCredential.Token))
            {
                return null;
            }
            _sqlUserHandler.DeleteUserToken(addRecipeCredential.AuthenticationCredential.Username);
            return _sqlUserHandler.AddRecipe(addRecipeCredential.RecipeCredential);
        }

        [HttpDelete("recipe")]
        public Recipe DeleteRecipe(int recipeId)
        {
            return _sqlUserHandler.DeleteRecipe(recipeId);
        }
    }
}