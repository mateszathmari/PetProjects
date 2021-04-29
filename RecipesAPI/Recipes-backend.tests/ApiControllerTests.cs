using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public ApiControllerTests()
        {
            var fixture = new WebApplicationFactory<Startup>();
            _client = fixture.CreateClient();
        }

        [Test]
        public async Task Login_ValidCredential_ShouldReturnOk()
        {
            // Arrange
            string url = "api/login";
            UserCred userCred = new UserCred("mateszathmari@gmail.com", "1234");
            string output = JsonConvert.SerializeObject(userCred);
            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new StringContent(output,
                Encoding.UTF8, "application/json") };

            // Act
            var response = await _client.SendAsync(req);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
