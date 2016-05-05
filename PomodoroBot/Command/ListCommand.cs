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
            {
                foreach (var entity in entities)
                {
                    var response = CommandTool.Instance.Request.CreateReplyMessage(message, $"name: {entity.Name} id: {entity.RowKey}");
                    await CommandTool.Instance.Client.Messages.SendMessageAsync(response);
                }
                return message.CreateReplyMessage("");
            }
            else
                return message.CreateReplyMessage($"timer can't be found.");
        }
    }
}
