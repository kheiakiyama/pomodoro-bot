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
        public UInt32 Duration { get; set; }
        public UInt32 ShortBreak { get; set; }
        public UInt32 LongBreak { get; set; }
        public UInt32 LongBreakSpan { get; set; }

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
                    .Message("Set your pomodoro timer.")
                    .Build();
        }

        public static IDialog<PomodoroTimer> MakeDialog()
        {
            return Chain.From(() => FormDialog.FromForm(BuildForm));

        }
    }
}
