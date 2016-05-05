using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using PomodoroBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace PomodoroBot.Command
{
    public class ChatRequest
    {
        internal void Recive(Message message, HttpRequestMessage request)
        {
            message.SetBotUserData(AccountKey, message.From);
            message.SetBotUserData(BotAccountKey, message.To);
            Host = $"http://{request.RequestUri.Authority}";
        }

        public ChannelAccount GetAccount(IBotDataBag userData)
        {
            return userData.Get<ChannelAccount>(AccountKey);
        }

        public ChannelAccount GetBotAccount(IBotDataBag userData)
        {
            return userData.Get<ChannelAccount>(BotAccountKey);
        }

        private static readonly string AccountKey = "Account";
        private static readonly string BotAccountKey = "BotAccount";
        internal string Host { get; private set; }
        
        public Message CreateReplyMessage(Message message, string text)
        {
            return new Message()
            {
                From = new ChannelAccount() { ChannelId = message.To.ChannelId, Address = message.To.Address, },
                To = new ChannelAccount() { ChannelId = message.From.ChannelId, Address = message.From.Address },
                Text = text,
                Language = message.Language
            };
        }
    }
}