using Microsoft.Bot.Connector;
using PomodoroBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot.Command
{
    public class CommandTool
    {
        public PomodoroTimerRepository Repository { get; private set; }
        public static CommandTool Instance { get; private set; }

        static CommandTool()
        {
            Instance = new CommandTool();
        }

        private CommandTool()
        {
            Repository = new PomodoroTimerRepository();
        }

        internal static readonly string UserIdKey = "UserId";

        internal void SetUserData(Message message)
        {
            message.SetBotUserData(UserIdKey, message.From.Id);
        }
    }
}
