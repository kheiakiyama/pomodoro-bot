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
        private Dictionary<string, string> m_Adresses = new Dictionary<string, string>();

        public void Recive(Message message)
        {
            if (!m_Adresses.ContainsKey(message.To.ChannelId))
                m_Adresses.Add(message.To.ChannelId, message.To.Address);
            message.SetBotUserData(AccountKey, message.From);
        }

        public ChannelAccount GetAccount(IBotDataBag userData)
        {
            return userData.Get<ChannelAccount>(AccountKey);
        }

        public Message CreateMessage(PomodoroTimerEntity entity, string text)
        {
            if (!m_Adresses.ContainsKey(entity.ChannelId))
                throw new NotSupportedException();

            return new Message()
            {
                From = new ChannelAccount() { ChannelId = entity.ChannelId, Address = m_Adresses[entity.ChannelId], },
                To = new ChannelAccount() { ChannelId = entity.ChannelId, Address = entity.Address },
                Text = text,
                Language = "ja"
            };
        }

        private static readonly string AccountKey = "Account";
    }
}