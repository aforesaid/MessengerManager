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
            Func<ITelegramBotClient, Update, CancellationToken, Task> updateAction,
            Func<ITelegramBotClient, Exception, CancellationToken, Task> errorAction,
            CancellationToken cancellationToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] {updateType},
                ThrowPendingUpdates = true
            };
            
            await telegramBotClient.ReceiveAsync(updateAction,errorAction, receiverOptions, cancellationToken);
        }
    }
}