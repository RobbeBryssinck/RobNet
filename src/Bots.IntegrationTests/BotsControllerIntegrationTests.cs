using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Bots.Domain.Entities;
using Bots.API.Models.v1;

namespace Bots.IntegrationTests
{
    public class BotsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<API.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<API.Startup> _factory;
        private readonly CreateBotModel _createBotModel;

        public BotsControllerIntegrationTests(CustomWebApplicationFactory<API.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _createBotModel = new CreateBotModel
            {
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
        }

        [Fact]
        public async Task GetBotsByBotnetId()
        {
            HttpResponseMessage result = await _client.GetAsync("api/v1/Bots/1");

            result?.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBotsByBotnetId_WhenBotDoesNotExist_NotFound()
        {
            HttpResponseMessage result = await _client.GetAsync("api/v1/Bots/0");

            result?.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostBot_ShouldReturnBot()
        {
            HttpResponseMessage result = await _client.PostAsync("api/v1/Bots/", 
                new StringContent(JsonConvert.SerializeObject(_createBotModel), 
                    Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();

            result?.StatusCode.Should().Be(HttpStatusCode.OK);
            string resultContentString = await result.Content.ReadAsStringAsync();
            Bot resultContentBot = JsonConvert.DeserializeObject<Bot>(resultContentString);
            resultContentBot.Should().BeOfType<Bot>();
        }
    }
}
