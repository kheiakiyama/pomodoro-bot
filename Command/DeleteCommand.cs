﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using PomodoroBot.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace PomodoroBot.Command
{
    public class DeleteCommand : ICommand
    {
        private Regex m_Regex = new Regex(@"delete ([0-9a-f]{32})");

        public bool DoHandle(Message message)
        {
            return m_Regex.IsMatch(message.Text.ToLower());
        }

        public async Task<Message> Reply(Message message)
        {
            var id = m_Regex.Match(message.Text.ToLower()).Groups[1].Value;
            var entity = await CommandTool.Instance.Repository.Find(id, message.From.Id);
            if (entity != null)
            {
                PomodoroTimerBackend timer = new PomodoroTimerBackend();
                await timer.Stop(entity.RowKey);
                await CommandTool.Instance.Repository.Delete(entity);
                return message.CreateReplyMessage("timer is deleted.");
            }
            else
                return message.CreateReplyMessage($"timer can't delete. id:{id}");
        }
    }
}
