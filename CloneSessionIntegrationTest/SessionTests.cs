using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using UI;

namespace CloneSessionIntegrationTest
{
    public class SessionTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _factory;
        public SessionTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.CreateClient();
        }
        [Fact]
        public async Task Call_TestGetSession_Returns_Ok()
        {
            var response = await _factory.GetAsync("api/Test");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Call_Set_And_Get_Session_Returns_Ok()
        {
            string value = Guid.NewGuid().ToString("N");
            string key = Guid.NewGuid().ToString("N");

            await _factory.GetAsync($"api/Test/SetSession?key={key}&value={value}");

            var response = await _factory.GetAsync($"api/Test/GetSession?key={key}");

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(value, responseContent);

        }
    }
}