using System;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using Bots.Data.Database;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using Bots.Data.Test.Infrastructure;

namespace Bots.Data.Test.Repository.v1
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly BotsContext _botsContext;
        private readonly Repository<Bot> _testee;
        private readonly Repository<Bot> _testeeFake;
        private readonly Bot _newBot;

        public RepositoryTests()
        {
            _botsContext = A.Fake<BotsContext>();
            _testeeFake = new Repository<Bot>(_botsContext);
            _testee = new Repository<Bot>(_context);
            _newBot = new Bot
            {
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
        }

        [Theory]
        [InlineData("1.2.3.4")]
        public async void UpdateBotAsync_WhenBotIsNotNull_ShouldReturnBot(string ip)
        {
            Bot bot = _context.Bots.First();
            bot.IP = ip;

            Bot result = await _testee.UpdateAsync(bot);

            result.Should().BeOfType<Bot>();
            result.IP.Should().Be(ip);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _botsContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new Bot())).Should().Throw<Exception>().WithMessage("entity could not be saved Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateBotAsync_WhenBotIsNotNull_ShouldReturnBot()
        {
            Bot result = await _testee.AddAsync(_newBot);

            result.Should().BeOfType<Bot>();
        }

        [Fact]
        public async void CreateBotAsync_WhenBotIsNotNull_ShouldAddBot()
        {
            int botCount = _context.Bots.Count();

            await _testee.AddAsync(_newBot);

            _context.Bots.Count().Should().Be(botCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _botsContext.Set<Bot>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _botsContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new Bot())).Should().Throw<Exception>().WithMessage("entity could not be updated Exception of type 'System.Exception' was thrown.");
        }
    }
}
