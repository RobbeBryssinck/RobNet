using System;
using System.Net;
using AutoMapper;
using Bots.API.Controllers;
using Bots.API.Models.v1;
using Bots.Domain.Entities;
using Bots.Service.Command;
using MediatR;
using FakeItEasy;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Bots.API.Test.Controllers
{
    public class BotsControllerTests
    {
        private readonly IMediator _mediator;
        private readonly BotsController _testee;
        private readonly CreateBotModel _createBotModel;
        private readonly UpdateBotModel _updateBotModel;
        private readonly int _id = 1;

        public BotsControllerTests()
        {
            var mapper = A.Fake<IMapper>();
            _mediator = A.Fake<IMediator>();
            _testee = new BotsController(mapper, _mediator);

            _createBotModel = new CreateBotModel
            {
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
            _updateBotModel = new UpdateBotModel
            {
                Id = _id,
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
            var bot = new Bot
            {
                Id = _id,
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };

            A.CallTo(() => mapper.Map<Bot>(A<Bot>._)).Returns(bot);
            A.CallTo(() => _mediator.Send(A<CreateBotCommand>._, default)).Returns(bot);
            A.CallTo(() => _mediator.Send(A<UpdateBotCommand>._, default)).Returns(bot);
        }

        [Theory]
        [InlineData("CreateBotAsync: bot is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateBotCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.PostBot(_createBotModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("UpdateBotAsync: bot is null")]
        public async void Put_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<UpdateBotCommand>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.PutBot(_updateBotModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Put_WhenBotDoesNotExist_ShouldReturnNotFound()
        {
            var updateBotModel = new UpdateBotModel { Id = 0 };
            var result = await _testee.PutBot(updateBotModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async void Post_ShouldReturnBot()
        {
            var result = await _testee.PostBot(_createBotModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Bot>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Put_ShouldReturnBot()
        {
            var result = await _testee.PutBot(_updateBotModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Bot>();
            result.Value.Id.Should().Be(_id);
        }
    }
}
