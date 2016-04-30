using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace PomodoroBot.Command
{
    public class CreateCommand : ICommand
    {
        public bool DoHandle(Message message)
        {
            return message.Text.ToLower() == "create";
        }

        public async Task<Message> Reply(Message message)
        {
            return await Conversation.SendAsync(message, PomodoroTimer.MakeDialog);
        }
    }
}
