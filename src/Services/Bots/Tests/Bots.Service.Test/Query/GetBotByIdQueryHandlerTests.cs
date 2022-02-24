using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using Bots.Service.Query;

namespace Bots.Service.Test.Query
{
    public class GetBotByIdQueryHandlerTests
    {
        private readonly IBotRepository _botRepository;
        private readonly GetBotByIdQueryHandler _testee;
        private readonly Bot _bot;
        private readonly int _id = 1;

        public GetBotByIdQueryHandlerTests()
        {
            _botRepository = A.Fake<IBotRepository>();
            _testee = new GetBotByIdQueryHandler(_botRepository);

            _bot = new Bot
            {
                Id = _id,
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnBot()
        {
            A.CallTo(() => _botRepository.GetBotByIdAsync(_id, default)).Returns(_bot);

            Bot result = await _testee.Handle(new GetBotByIdQuery { Id = _id }, default);

            A.CallTo(() => _botRepository.GetBotByIdAsync(_id, default)).MustHaveHappenedOnceExactly();
            result.IP.Should().Be("155.223.25.67");
            result.Platform.Should().Be("Linux");
            result.Status.Should().Be("Waiting");
            result.BotnetId.Should().Be(1);
        }
    }
}