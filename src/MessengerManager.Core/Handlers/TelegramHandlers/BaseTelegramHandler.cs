using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MessengerManager.Core.Handlers.TelegramHandlers
{
    public static class BaseTelegramHandler
    {
        public static async Task StartHandler(ITelegramBotClient telegramBotClient,
            UpdateType updateType,
            Action<ITelegramBotClient, Update, CancellationToken> updateAction,
            Action<ITelegramBotClient, Exception, CancellationToken> errorAction,
            CancellationToken token)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] {updateType},
                ThrowPendingUpdates = true
            };

            await telegramBotClient.ReceiveAsync(updateAction,errorAction, receiverOptions, token);
        }
    }
}