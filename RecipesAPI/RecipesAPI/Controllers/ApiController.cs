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
        private readonly SQLRecipeRepository _sqlRecipeHandler;

        public ApiController(ILogger<ApiController> logger, UserContext context)
        {
            _logger = logger;
            _sqlUserHandler = new SQLUserRepository(context);
            _sqlRecipeHandler = new SQLRecipeRepository(context, _sqlUserHandler);
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
                _sqlRecipeHandler.DeleteAllFavoriteUserRecipe(userCred.Username);
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

            if (!loginningUser.IsValidToken(authenticationCredential.Token))
            {
                return null;
            }

            return _sqlRecipeHandler.GetUserFavoriteRecipes(authenticationCredential.Username).ToList();
        }

        [HttpPut("favorite-recipes")]
        public IActionResult AddFavoriteRecipes(AddFavoriteRecipeCredential favoriteRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(favoriteRecipeCredential.AuthenticationCredential.Username);
            if (user == null)
            {
                return BadRequest("Wrong username or token");
            }

            if (!user.IsValidToken(favoriteRecipeCredential.AuthenticationCredential.Token))
            {
                return BadRequest("Wrong username or token");
            }

            Recipe recipe = _sqlRecipeHandler.AddFavoriteRecipeToUser(user.UserName, favoriteRecipeCredential.RecipeId);
            return Ok(recipe.id);
        }

        [HttpDelete("favorite-recipes")]
        public IActionResult DeleteFavoriteRecipes(DeleteFavoriteRecipeCredential deleteFavoriteRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(deleteFavoriteRecipeCredential.AuthenticationCredential.Username);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.IsValidToken(deleteFavoriteRecipeCredential.AuthenticationCredential.Token))
            {
                _sqlRecipeHandler.DeleteFavoriteRecipeToUser(user.UserName, deleteFavoriteRecipeCredential.RecipeId);
                return Ok();
                //return _sqlUserHandler.GetUser(deleteFavoriteRecipeCredential.AuthenticationCredential.Username).Recipes;
            }

            return BadRequest();
        }

        [HttpPost("healthLabel-label")]
        public IActionResult AddHealthLabel(HealthLabel healthLabel)
        {
            HealthLabel newHealthLabel = _sqlRecipeHandler.AddHealthLabel(healthLabel.Name);
            if (newHealthLabel != null)
            {
                return Ok(newHealthLabel.Id);
            }

            return BadRequest();
        }


        [HttpGet("healthLabel-label/{healthLabelId:int}")]
        public IActionResult GetHealthLabel(int healthLabelId)
        {
            HealthLabel healthLabel = _sqlRecipeHandler.GetHealthLabel(healthLabelId);
            if (healthLabel != null)
            {
                return Ok(healthLabel);
            }

            return BadRequest();
        }

        [HttpPost("ingredient")]
        public IActionResult AddIngredient(Ingredient ingredient)
        {
            Ingredient newIngredient = _sqlRecipeHandler.AddIngredient(ingredient.Name);
            if (newIngredient != null)
            {
                return Ok(newIngredient.Id);
            }

            return BadRequest();
        }

        [HttpGet("ingredient/{ingredientId:int}")]
        public IActionResult GetIngredient(int ingredientId)
        {
            Ingredient ingredient = _sqlRecipeHandler.GetIngredient(ingredientId);
            if (ingredient != null)
            {
                return Ok(ingredient);
            }

            return BadRequest();
        }

        [HttpDelete("ingredient/{ingredientId:int}")]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            Ingredient ingredient = _sqlRecipeHandler.DeleteIngredient(ingredientId);
            if (ingredient != null)
            {
                return Ok(ingredient);
            }

            return BadRequest();
        }

        [HttpDelete("healthLabel-label/{healthLabelId:int}")]
        public IActionResult DeleteHealthLabel(int healthLabelId)
        {
            HealthLabel healthLabel = _sqlRecipeHandler.DeleteHealthLabel(healthLabelId);
            if (healthLabel != null)
            {
                return Ok(healthLabel);
            }

            return BadRequest();
        }

        [HttpPost("recipe")]
        public IActionResult AddRecipe(AddRecipeCredential addRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(addRecipeCredential.AuthenticationCredential.Username);
            if (user == null)
            {
                return BadRequest("Wrong username or token");
            }

            if (!user.IsValidToken(addRecipeCredential.AuthenticationCredential.Token))
            {
                return BadRequest("Wrong username or token");
            }

            Recipe recipe = _sqlRecipeHandler.AddRecipe(addRecipeCredential.RecipeCredential);
            if (recipe != null)
            {
                return Ok(recipe.id);
            }

            return BadRequest();
        }

        [HttpDelete("recipe")]
        public IActionResult DeleteRecipe(DeleteRecipeCredential deleteRecipeCredential)
        {
            User user = _sqlUserHandler.GetUser(deleteRecipeCredential.AuthenticationCredential.Username);
            if (user == null)
            {
                return BadRequest("Wrong username or token");
            }

            if (!user.IsValidToken(deleteRecipeCredential.AuthenticationCredential.Token))
            {
                return BadRequest("Wrong username or token");
            }

            _sqlRecipeHandler.DeleteRecipe(deleteRecipeCredential.RecipeId);
            return Ok();
        }
    }
}