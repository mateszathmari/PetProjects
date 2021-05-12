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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserContext _context;
        private readonly SQLUserRepository _sqlUserHandler;
        private readonly SQLRecipeRepository _sqlRecipeHandler;

        public UserController(ILogger<UserController> logger, UserContext context)
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


    }
}