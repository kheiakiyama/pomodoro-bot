using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using PomodoroBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroBot.Command
{
    public class ChatRequest
    {
        public void Recive(Message message)
        {
            message.SetBotUserData(AccountKey, message.From);
            message.SetBotUserData(BotAccountKey, message.To);
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