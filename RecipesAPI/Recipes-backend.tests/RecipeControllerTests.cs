using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EFDataAccess.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using RecipesAPI;
using RecipesAPI.Models;
using Xunit;

namespace Recipes_backend.tests
{
    class RecipeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client { get; }
        private string _token;
        private int _ingredientId;
        private int _healthLabelId;
        private int _recipeId;
        private int _favRecipeId;

        public RecipeControllerTests()
        {
            var fixture = new WebApplicationFactory<Startup>();
            _client = fixture.CreateClient();
        }

        [OneTimeSetUp]
        public async Task SetupBeforeEachTest()
        {
            // Arrange
            string regUrl = "user/registration";
            Address address = new Address("city", "street", 55, "11");
            RegistrationCredential registrationCred = new RegistrationCredential("recipe", "password",
                "Recipe@test.com", "city", "street", 11, "postCode");
            string regOutput = JsonConvert.SerializeObject(registrationCred);
            var regReq = new HttpRequestMessage(HttpMethod.Post, regUrl)
            {
                Content = new StringContent(regOutput,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var regResponse = await _client.SendAsync(regReq);

            string url = "user/login";
            UserCredential userCred = new UserCredential("recipe", "password");
            string output = JsonConvert.SerializeObject(userCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            _token = response.Content.ReadAsStringAsync().Result;
        }

        [OneTimeTearDown]
        public async Task GlobalTeardown()
        {
            // Arrange
            string url = "user/delete";
            UserCredential loginCredential =
                new UserCredential("recipe", "password");
            string output = JsonConvert.SerializeObject(loginCredential);
            var req = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
        }


        [Test]
        public async Task Test231_AddIngredient_Ingredient_ShouldReturnIngredientId()
        {
            // Arrange
            string url = "recipe/ingredient";
            // We will need authentication for it later
            Ingredient ingredient = new Ingredient("ingredient");
            string output = JsonConvert.SerializeObject(ingredient);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            _ingredientId = Int32.Parse(response.Content.ReadAsStringAsync().Result);


            // Assert
            Assert.NotNull(_ingredientId);
        }


        [Test]
        public async Task Test232_GetIngredient_IngredientId_ShouldReturnOK()
        {
            // Arrange
            string url = $"recipe/ingredient/{_ingredientId}";
            // We will need authentication for it later

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            // Act
            var response = await _client.SendAsync(req);
            var ingredient = response.Content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test2331_DeleteIngredient_IngredientId_ShouldReturnOk()
        {
            // Arrange
            string url = $"recipe/ingredient/{_ingredientId}";
            // We will need authentication for it later

            var req = new HttpRequestMessage(HttpMethod.Delete, url);

            // Act
            var response = await _client.SendAsync(req);
            var ingredient = response.Content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test2332_DeleteIngredient_WrongIngredientId_ShouldNotReturnOk()
        {
            //_ingredientId = 15;
            // Arrange
            string url = $"recipe/ingredient/{_ingredientId}";
            // We will need authentication for it later

            var req = new HttpRequestMessage(HttpMethod.Delete, url);

            // Act
            var response = await _client.SendAsync(req);
            var ingredient = response.Content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsFalse(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test234_AddHealthLabel_HealthLabel_ShouldReturnHealthLAbelId()
        {
            // Arrange
            string url = "recipe/healthLabel-label";
            // We will need authentication for it later
            HealthLabel healthLabel = new HealthLabel("Healthy");
            string output = JsonConvert.SerializeObject(healthLabel);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            _healthLabelId = Int32.Parse(response.Content.ReadAsStringAsync().Result);


            // Assert
            Assert.NotNull(_healthLabelId);
        }

        [Test]
        public async Task Test235_GetHealthLabel_HealthLabelId_ShouldReturnOK()
        {
            // Arrange
            string url = $"recipe/healthLabel-label/{_healthLabelId}";
            // We will need authentication for it later

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            // Act
            var response = await _client.SendAsync(req);
            var healthLabel = response.Content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test236_DeleteHealthLabel_HealthLabelId_ShouldReturnOk()
        {
            // Arrange
            string url = $"recipe/healthLabel-label/{_healthLabelId}";
            // We will need authentication for it later

            var req = new HttpRequestMessage(HttpMethod.Delete, url);

            // Act
            var response = await _client.SendAsync(req);
            var healthLabel = response.Content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test237_DeleteHealthLabel_WrongHealthLabelId_ShouldNotReturnOk()
        {
            // Arrange
            // _healthLabelId should not be anymore in db because I delete it in previous test
            string url = $"recipe/healthLabel-label/{_healthLabelId}";
            // We will need authentication for it later

            var req = new HttpRequestMessage(HttpMethod.Delete, url);

            // Act
            var response = await _client.SendAsync(req);
            var healthLabel = response.Content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsFalse(response.StatusCode == HttpStatusCode.OK);
        }


        [Test]
        public async Task Test238_AddRecipe_ValidCredential_ShouldReturnRecipe()
        {
            //_token = "Gc8IvhMV2Uh5rXkbMLRkTY5E+Z9j0229";

            // Arrange
            string url = "recipe/recipe";
            Ingredient ingredient = new Ingredient("food");
            HealthLabel healthLabel = new HealthLabel("Healthy");
            AuthenticationCredential authCred = new AuthenticationCredential("recipe", _token);

            List<string> ingredientList = new List<string>();
            List<string> healthLabels = new List<string>();

            ingredientList.Add("ingredient1");
            ingredientList.Add("ingredient2");
            healthLabels.Add("healthLabel1");
            healthLabels.Add("healthLabel2");

            Recipe recipe = new Recipe("food", "image", 50, "http:link");
            RecipeCredential recipeCredential = new RecipeCredential(recipe, healthLabels, ingredientList);

            AddRecipeCredential cred =
                new AddRecipeCredential(authCred, recipeCredential);

            string output = JsonConvert.SerializeObject(cred);

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            _recipeId = Int32.Parse(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test241_AddToFavoriteRecipes_ValidCredential_ShouldReturnRecipeId()
        {
            //_token = "/T7itjwV2Uiha8zJJlrBQYUzScbklFtg";
            //_recipeId = 16;

            // Arrange
            string url = "recipe/favorite-recipes";
            AuthenticationCredential authCred = new AuthenticationCredential("recipe", _token);


            AddFavoriteRecipeCredential cred =
                new AddFavoriteRecipeCredential(_recipeId, authCred);

            string output = JsonConvert.SerializeObject(cred);

            var req = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            _favRecipeId = Int32.Parse(response.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test242_GetFavoriteRecipes_ValidCredential_ShouldReturnRecipes()
        {
            //_token = "+9KBySEV2UionSvmxcoQQqC44oISvCQp";
            //_recipeId = 4;

            // Arrange
            string url = "recipe/favorite-recipes";

            AuthenticationCredential cred =
                new AuthenticationCredential("recipe", _token);

            string output = JsonConvert.SerializeObject(cred);

            var req = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            var recipesResult = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.IsFalse(recipesResult == "");
        }

        [Test]
        public async Task Test2421_GetFavoriteRecipes_NotValidCredential_ShouldNotReturnRecipes()
        {
            // Arrange
            string url = "recipe/favorite-recipes";

            AuthenticationCredential cred =
                new AuthenticationCredential("recipe", "not valid token");

            string output = JsonConvert.SerializeObject(cred);

            var req = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            var recipesResult = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.IsTrue(recipesResult == "");
        }

        [Test]
        public async Task Test243_deleteFavoriteRecipes_ValidCredential_ShouldReturnRecipes()
        {
            //_token = "/T7itjwV2Uiha8zJJlrBQYUzScbklFtg";
            //_recipeId = 16;

            // Arrange
            string url = "recipe/favorite-recipes";
            AuthenticationCredential authCred = new AuthenticationCredential("recipe", _token);

            DeleteFavoriteRecipeCredential cred =
                new DeleteFavoriteRecipeCredential(authCred, _favRecipeId);

            string output = JsonConvert.SerializeObject(cred);

            var req = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            var notValidToken = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test244_deleteRecipes_ValidCredential_ShouldReturnOk()
        {
            //_token = "+9KBySEV2UionSvmxcoQQqC44oISvCQp";
            //_recipeId = 15;

            // Arrange
            string url = "recipe/recipe";
            AuthenticationCredential authCred = new AuthenticationCredential("recipe", _token);

            DeleteRecipeCredential cred =
                new DeleteRecipeCredential(authCred, _recipeId);

            string output = JsonConvert.SerializeObject(cred);

            var req = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);
            var notValidToken = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}