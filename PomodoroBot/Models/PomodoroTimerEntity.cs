using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroBot.Models
{
    public class PomodoroTimerEntity : TableEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int ShortBreak { get; set; }
        public int LongBreakSpan { get; set; }
        public int LongBreak { get; set; }

        public PomodoroTimerEntity()
        {
        }

        public PomodoroTimerEntity(PomodoroTimer source)
        {
            Name = source.Name;
            Duration = source.Duration;
            ShortBreak = source.ShortBreak;
            LongBreakSpan = source.LongBreakSpan;
            LongBreak = source.LongBreak;
            RowKey = Guid.NewGuid().ToString().Replace("-", "");
        }

        public delegate Task PostMessageDelegate(string message);

        public void StartTimer(PostMessageDelegate postMessage)
        {
            Task.Run(async () => {
                var count = 1;
                while (true)
                {
                    await postMessage($"{count} pomodoro start.");
                    await Task.Delay(MinutesToMillSecond(Duration));
                    if (count % LongBreakSpan == 0)
                    {
                        await postMessage($"{count} pomodoro is end. rest {LongBreak} minutes of long break.");
                        await Task.Delay(MinutesToMillSecond(LongBreak));
                        await postMessage($"long break is end.");
                    }
                    else
                    {
                        await postMessage($"{count} pomodoro is end. rest {ShortBreak} minutes of short break.");
                        await Task.Delay(MinutesToMillSecond(ShortBreak));
                        await postMessage($"short break is end.");
                    }
                    count++;
                }
            });
        }

        private int MinutesToMillSecond(int minutes)
        {
            return minutes * 60 * 1000;
        }
    }
}
