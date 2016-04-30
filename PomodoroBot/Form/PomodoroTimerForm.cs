using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot
{
    [Serializable]
    internal class PomodoroTimer
    {
        public string Name { get; set; }

        [Describe("duration minutes")]
        [Numeric(5, 60)]
        public UInt32 Duration { get; set; }

        [Describe("short break minutes")]
        [Numeric(1, 20)]
        public UInt32 ShortBreak { get; set; }

        [Describe("take a long break when spent many times Pomodoro?")]
        [Numeric(2, 10)]
        public UInt32 LongBreakSpan { get; set; }

        [Describe("long break minutes")]
        [Numeric(5, 60)]
        public UInt32 LongBreak { get; set; }

        public PomodoroTimer()
        {
            Name = "";
            Duration = 25;
            ShortBreak = 5;
            LongBreak = 20;
            LongBreakSpan = 4;
        }

        private static IForm<PomodoroTimer> BuildForm()
        {
            return new FormBuilder<PomodoroTimer>()
                    .Message("set your pomodoro timer.")
                    .Build();
        }

        public static IDialog<PomodoroTimer> MakeDialog()
        {
            return Chain.From(() => FormDialog.FromForm(BuildForm));

        }
    }
}
