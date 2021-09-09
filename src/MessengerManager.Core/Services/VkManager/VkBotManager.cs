using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Vk;
using MessengerManager.Core.Models.Messengers.Vk;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace MessengerManager.Core.Services.VkManager
{
    public class VkBotManager : IVkBotManager
    {
        private readonly IVkApi _vkApi;
        private readonly VkConfiguration _vkConfiguration;
        private readonly ILogger<VkBotManager> _logger;

        public VkBotManager(IVkApi vkApi, 
            ILogger<VkBotManager> logger,
            IOptions<VkConfiguration> vkConfiguration)
        {
            _vkApi = vkApi;
            _logger = logger;
            _vkConfiguration = vkConfiguration.Value;
        }

        public async Task<ApiVkChat[]> GetAllChats()
        {
            try
            {
                var listConversations = new List<ApiVkChat>();
                const int limit = 200;
                ulong offset = 0;
                GetConversationsResult result;
                do
                {
                    result = await _vkApi.Messages.GetConversationsAsync(new GetConversationsParams
                    {
                        Count = limit,
                        Offset = offset
                    });

                    foreach (var conversationAndLastMessage in result.Items)
                    {
                        if (conversationAndLastMessage.Conversation.ChatSettings?.Title != null)
                        {
                            var newConversation = new ApiVkChat(conversationAndLastMessage.Conversation.Peer.Id,
                                conversationAndLastMessage.Conversation.ChatSettings.Title);
                            listConversations.Add(newConversation);
                        }
                        else
                        {
                            var usersFromConversations = await _vkApi.Messages.GetConversationMembersAsync(conversationAndLastMessage.Conversation
                                    .Peer.Id);
                            var user = usersFromConversations.Profiles
                                .FirstOrDefault(x => x.ScreenName != _vkConfiguration.MyVkUsername);
                            if (user != null)
                            {
                                var apiUser = new ApiVkUser(user.FirstName, user.LastName, user.ScreenName, user.Id);
                                var newConversation = new ApiVkChat(conversationAndLastMessage.Conversation.Peer.Id,
                                    apiUser.ToString());
                                listConversations.Add(newConversation);
                            }
                        }
                    }

                    offset += limit;
                } while (result.Items.Count == limit);

                return listConversations.ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось получить список чатов");
                throw;
            }
        }

        public async Task<ApiVkMessage[]> GetMessages(long vkPeerId, int count = 200)
        {
            try
            {
                const string messageAlias = "message";
                
                var listMessages = new List<ApiVkMessage>();
                const int limit = 200;
                var offset = 0;
                MessageGetHistoryObject result;
                do
                {
                    result = await _vkApi.Messages.GetHistoryAsync(new MessagesGetHistoryParams
                    {
                        PeerId = vkPeerId,
                        Count = limit,
                        Offset = offset,
                    });

                    var newMessages = result.Messages
                        .Where(x => x.Text != null && x.FromId.HasValue)
                        .Select(x =>
                            new ApiVkMessage(vkPeerId, x.Text, x.FromId, x.Id, x.Date))
                        .ToArray();
                    listMessages.AddRange(newMessages);

                    offset += limit;
                } while (result.Messages.Count() == limit && listMessages.Count < count);

                return listMessages.ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось получить список сообщений по vkPeerId : {0} ", vkPeerId);
                throw;
            }
        }
    }
}