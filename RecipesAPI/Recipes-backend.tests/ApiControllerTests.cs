using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using RecipesAPI;
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
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            // Dummy Gmail account credentials
            keyValuePairs.Add(new KeyValuePair<string, string>("username", "mateszathmari@gmail.com"));
            keyValuePairs.Add(new KeyValuePair<string, string>("password", "1234"));
            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(keyValuePairs) };

            // Act
            var response = await _client.SendAsync(req);
            Console.WriteLine(response.Content);

            // Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
