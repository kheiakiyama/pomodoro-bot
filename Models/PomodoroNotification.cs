using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroBot.Models
{
    public class PomodoroNotification
    {
        public const string QueueName = "timer";

        public string Key { get; set; }
    }
}