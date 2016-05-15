﻿using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot.Command
{
    public interface ICommand
    {
        bool DoHandle(Message message);
        Task<Message> Reply(Message message);
    }
}
