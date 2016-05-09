using System;
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

namespace PomodoroBot.Command
{
    public class ListCommand : ICommand
    {
        public bool DoHandle(Message message)
        {
            return message.Text.ToLower() == "list";
        }
        
        public async Task<Message> Reply(Message message)
        {
            var entities = await CommandTool.Instance.Repository.List(message.From.Id);
            if (entities.Length > 0)
                return message.CreateReplyMessage(string.Join("\n\n", entities.Select(q => string.Join("\n\n", q.Descript()))));
            else
                return message.CreateReplyMessage($"timer can't be found.");
        }
    }
}
