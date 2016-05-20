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
    public class HelpCommand : ICommand
    {
        public bool DoHandle(Message message)
        {
            return message.Text.ToLower() == "help";
        }
        
        public async Task<Message> Reply(Message message)
        {
            return message.CreateReplyMessage($"visit {Settings.Get("Help")}");
        }
    }
}
