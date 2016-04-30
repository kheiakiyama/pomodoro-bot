using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot
{
    [Serializable]
    public class PomodoroTimer
    {
        public string Name { get; set; }

        [Describe("duration minutes")]
        [Numeric(5, 60)]
        public int Duration { get; set; }

        [Describe("short break minutes")]
        [Numeric(1, 20)]
        public int ShortBreak { get; set; }

        [Describe("take a long break when spent many times Pomodoro?")]
        [Numeric(2, 10)]
        public int LongBreakSpan { get; set; }

        [Describe("long break minutes")]
        [Numeric(5, 60)]
        public int LongBreak { get; set; }

        public PomodoroTimer()
        {
            Name = "";
            Duration = 25;
            ShortBreak = 5;
            LongBreak = 20;
            LongBreakSpan = 4;
        }
    }
}
