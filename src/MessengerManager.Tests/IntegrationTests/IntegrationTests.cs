﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Telegram;
using MessengerManager.Core.Services.TelegramManager;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace MessengerManager.Tests.IntegrationTests
{
    public class IntegrationTests : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private MessengerManagerServiceTestHost _testHost;
        [SetUp]
        public async Task Setup()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets<IntegrationTests>()
                .Build();
            
            _testHost = new MessengerManagerServiceTestHost(config);
            await _testHost.Start(_cancellationTokenSource.Token);
            await Task.Delay(5000);
        }

        [Test]
        public async Task SendSubMessage()
        {
            await Setup();

            var chatThreadRepo = _testHost.ServiceProvider.GetRequiredService<IGenericRepository<ChatThreadEntity>>();
            var manager = _testHost.ServiceProvider.GetRequiredService<ITelegramBotManager>();

            var existChat = chatThreadRepo.GetAll().FirstOrDefault(x => x.ThreadName == "test");

            var request = new ApiTelegramMakeChat(existChat?.ThreadName);
            await manager.MakeChat(request);
        }
        [Test]
        public async Task MakeThread()
        {
            await Setup();

            var manager = _testHost.ServiceProvider.GetRequiredService<ITelegramBotManager>();

            var request = new ApiTelegramMakeChat("Yuri");
            await manager.MakeChat(request);
            
            await Task.Delay(10000);
            
            await manager.MakeChat(request);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}