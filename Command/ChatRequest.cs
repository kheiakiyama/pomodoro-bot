﻿using Microsoft.Bot.Builder.Dialogs;
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
        
        public Message CreateReplyMessage(PomodoroTimerEntity entity, string text)
        {
            return new Message()
            {
                From = new ChannelAccount() { ChannelId = entity.ChannelId, Address = entity.BotAddress, },
                To = new ChannelAccount() { ChannelId = entity.ChannelId, Address = entity.Address },
                Text = text,
                Language = "ja"
            };
        }
    }
}