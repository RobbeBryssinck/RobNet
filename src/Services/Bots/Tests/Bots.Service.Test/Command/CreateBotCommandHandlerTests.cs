using FakeItEasy;
using FluentAssertions;
using Xunit;
using Bots.Domain.Entities;
using Bots.Data.Repository.v1;
using Bots.Service.Command;

namespace Bots.Service.Test.Command
{
    public class CreateBotCommandHandlerTests
    {
        private readonly CreateBotCommandHandler _testee;
        private readonly IBotRepository _botRepository;
        private readonly Bot _bot;

        public CreateBotCommandHandlerTests()
        {
            _botRepository = A.Fake<IBotRepository>();
            _testee = new CreateBotCommandHandler(_botRepository);

            _bot = new Bot
            {
                Platform = "Linux"
            };
        }

        [Fact]
        public async void Handle_ShouldCallAddAsync()
        {
            await _testee.Handle(new CreateBotCommand(), default);

            A.CallTo(() => _botRepository.AddAsync(A<Bot>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedBot()
        {
            A.CallTo(() => _botRepository.AddAsync(A<Bot>._)).Returns(new Bot
            {
                Platform = "Linux"
            });

            var result = await _testee.Handle(new CreateBotCommand(), default);

            result.Should().BeOfType<Bot>();
            result.Platform.Should().Be(_bot.Platform);
        }
    }
}
