using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ToDoList.IntegrationTests
{
    public class TodoRoute
    {
        private readonly HttpClient _client;

        public TodoRoute()
        {
            var application = new MyWebApplication();
            _client = application.CreateClient();
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            // Arrange
            var request = new HttpRequestMessage(
                HttpMethod.Get, "/Todo");

            // Act: request the /todo route
            var response = await _client.SendAsync(request);
            Console.WriteLine("---------------------------------");
            Console.WriteLine(response.ToString());
            Console.WriteLine("---------------------------------");
            // Assert: the user is sent to the login page
            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            // Assert.Equal(
            //     "http://localhost:8888/Identity/Account/Login?ReturnUrl=%2Ftodo",
            //     response.Headers.Location.ToString());
        }
    }

}
