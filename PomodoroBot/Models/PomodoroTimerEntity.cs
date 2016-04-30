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
    }
}
