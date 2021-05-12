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
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserContext _context;
        private readonly SQLUserRepository _sqlUserHandler;
        private readonly SQLRecipeRepository _sqlRecipeHandler;

        public RecipeController(ILogger<UserController> logger, UserContext context)
        {
            _logger = logger;
            _sqlUserHandler = new SQLUserRepository(context);
            _sqlRecipeHandler = new SQLRecipeRepository(context, _sqlUserHandler);
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