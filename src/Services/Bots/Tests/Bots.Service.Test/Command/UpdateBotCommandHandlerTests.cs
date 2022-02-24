using FakeItEasy;
using FluentAssertions;
using Xunit;
using Bots.Domain.Entities;
using Bots.Data.Repository.v1;
using Bots.Service.Command;

namespace Bots.Service.Test.Command
{
    public class UpdateBotCommandHandlerTests
    {
        private readonly UpdateBotCommandHandler _testee;
        private readonly IBotRepository _botRepository;
        private readonly Bot _bot;

        public UpdateBotCommandHandlerTests()
        {
            _botRepository = A.Fake<IBotRepository>();
            _testee = new UpdateBotCommandHandler(_botRepository);

            _bot = new Bot
            {
                Platform = "Linux"
            };
        }

        [Fact]
        public async void Handle_ShouldCallUpdateAsync()
        {
            await _testee.Handle(new UpdateBotCommand(), default);

            A.CallTo(() => _botRepository.UpdateAsync(A<Bot>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnUpdatedBot()
        {
            A.CallTo(() => _botRepository.UpdateAsync(A<Bot>._)).Returns(_bot);

            Bot result = await _testee.Handle(new UpdateBotCommand(), default);

            result.Should().BeOfType<Bot>();
            result.Platform.Should().Be(_bot.Platform);
        }
    }
}
