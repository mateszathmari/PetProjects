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
        private readonly UserContext context;
        private SQLUserRepository _sqlUserHandler;

        public ApiController(ILogger<ApiController> logger, UserContext context)
        {
            _logger = logger;
            _sqlUserHandler = new SQLUserRepository(context);
        }


        [HttpPost("login")]
        public string Login(UserCred userCred)
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
        public IActionResult Logout(AuthenticationCred authentication)
        {
            User user = _sqlUserHandler.GetUser(authentication.UserName);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.IsValidToken(authentication.Token))
            {
                _sqlUserHandler.DeleteUserToken(authentication.UserName);
                return Ok("successfully logged out");
            }

            return BadRequest();
        }

        [HttpPost("registration")]
        public IActionResult Registration(RegistrationCred registrationCred)
        {
            User loginningUserName = _sqlUserHandler.GetUser(registrationCred.UserName);
            bool isLoginningUserEmailTaken = _sqlUserHandler.GetUsers().Any(x => x.Email == registrationCred.Email);
            if (loginningUserName != null || isLoginningUserEmailTaken)
            {
                return BadRequest("username or email already taken");
            }

            Address userAddress = new Address(registrationCred.City, registrationCred.Street, registrationCred.HouseNumber, registrationCred.PostCode);
            User user = new User(registrationCred.UserName, registrationCred.Email, userAddress, registrationCred.Password); // we should validate the password as well if it strong enough
            _sqlUserHandler.AddUser(user);
            return Ok("successful registration");
        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser([FromForm]string userName, [FromForm] string password)
        {
            User loginningUser = _sqlUserHandler.GetUser(userName);
            if (loginningUser == null)
            {
                return BadRequest();
            }

            if (loginningUser.IsValidPassword(password))
            {
                _sqlUserHandler.DeleteUser(userName);
                return Ok("user successfully deleted");
            }

            return BadRequest();
        }

        [HttpGet("favorite-recipes")]
        public List<Recipe> GetFavoriteRecipes(string userName, string Token)
        {
            return _sqlUserHandler.GetUser(userName).Recipes;
        }
    }
}