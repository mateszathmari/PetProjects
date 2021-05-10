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
    class ApiControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client { get; }
        private string _token;

        public ApiControllerTests()
        {
            var fixture = new WebApplicationFactory<Startup>();
            _client = fixture.CreateClient();
        }

        [Test]
        public async Task Test1_Registration_ValidCredential_ShouldReturnOk()
        {
            // Arrange
            string url = "api/registration";
            Address address = new Address("city", "street", 55, "11");
            RegistrationCredential registrationCred = new RegistrationCredential("username", "password","test@test.com", "city",  "street", 11,  "postCode");
            string output = JsonConvert.SerializeObject(registrationCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test21_Login_ValidCredential_ShouldReturnToken()
        {
            // Arrange
            string url = "api/login";
            UserCredential userCred = new UserCredential("username", "password");
            string output = JsonConvert.SerializeObject(userCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            _token = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.NotNull(_token);
        }

        [Test]
        public async Task Test22_Login_NotValidCredential_ShouldNotReturnToken()
        {
            // Arrange
            string url = "api/login";
            UserCredential userCred = new UserCredential("username", "WrongPassword");
            string output = JsonConvert.SerializeObject(userCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);


            var notValidToken = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.AreEqual("", notValidToken);
        }

        [Test]
        public async Task Test31_Logout_NotValidCredential_ShouldNotReturnOk()
        {
            // Arrange
            string url = "api/logout";
            AuthenticationCredential userCred =
                new AuthenticationCredential("username",
                    "wrong Token"); // we should get the token
            string output = JsonConvert.SerializeObject(userCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            // Assert
            Assert.IsFalse(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test32_Logout_ValidCredential_ShouldReturnOk()
        {
            // Arrange
            string url = "api/logout";
            AuthenticationCredential userCred =
                new AuthenticationCredential("username",
                    _token); // we should get the token
            string output = JsonConvert.SerializeObject(userCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test41_Delete_NotValidCredential_ShouldNotReturnOk()
        {
            // Arrange
            string url = "api/delete";
            UserCredential loginCredential =
                new UserCredential("username", "WrongPassword");
            string output = JsonConvert.SerializeObject(loginCredential);
            var req = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            // Assert
            Assert.IsFalse(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async Task Test42_Delete_ValidCredential_ShouldReturnOk()
        {
            // Arrange
            string url = "api/delete";
            UserCredential loginCredential =
                new UserCredential("username", "password");
            string output = JsonConvert.SerializeObject(loginCredential);
            var req = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(output,
                    Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.SendAsync(req);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}