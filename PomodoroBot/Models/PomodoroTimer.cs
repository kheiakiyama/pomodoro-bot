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
        public const string DescName = "name";
        public const string DescDuration = "duration minutes";
        public const string DescShortBreak = "short break minutes";
        public const string DescLongBreakSpan = "long break span";
        public const string DescLongBreak = "long break minutes";

        [Describe(DescName)]
        public string Name { get; set; }

        [Describe(DescDuration)]
        [Numeric(5, 60)]
        public int Duration { get; set; }

        [Describe(DescShortBreak)]
        [Numeric(1, 20)]
        public int ShortBreak { get; set; }

        [Describe(DescLongBreakSpan)]
        [Numeric(2, 10)]
        public int LongBreakSpan { get; set; }

        [Describe(DescLongBreak)]
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
