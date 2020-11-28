using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentAssertions;
using Xunit;
using Bots.Domain.Entities;
using Bots.API.Models.v1;
using Bots.API.Infrastructure.AutoMapper;

namespace Bots.API.Test.Infrastructure.AutoMapper
{
    public class MappingProfileTests
    {
        private readonly CreateBotModel _createBotModel;
        private readonly UpdateBotModel _updateBotModel;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _createBotModel = new CreateBotModel
            {
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
            _updateBotModel = new UpdateBotModel
            {
                Id = 1,
                IP = "155.223.25.67",
                Platform = "Linux",
                Status = "Waiting",
                BotnetId = 1
            };
        }

        [Fact]
        public void Map_Bot_CreateBotModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Bot, CreateBotModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Bot_UpdateBotModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Bot, UpdateBotModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Bot_Bot_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Bot, Bot>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateBotModel_Bot()
        {
            var customer = _mapper.Map<Bot>(_createBotModel);

            customer.IP.Should().Be(_createBotModel.IP);
            customer.Platform.Should().Be(_createBotModel.Platform);
            customer.Status.Should().Be(_createBotModel.Status);
            customer.BotnetId.Should().Be(_createBotModel.BotnetId);
        }

        [Fact]
        public void Map_UpdateCustomerModel_Customer()
        {
            var customer = _mapper.Map<Bot>(_updateBotModel);

            customer.Id.Should().Be(_updateBotModel.Id);
            customer.IP.Should().Be(_createBotModel.IP);
            customer.Platform.Should().Be(_createBotModel.Platform);
            customer.Status.Should().Be(_createBotModel.Status);
            customer.BotnetId.Should().Be(_createBotModel.BotnetId);
        }
    }
}
