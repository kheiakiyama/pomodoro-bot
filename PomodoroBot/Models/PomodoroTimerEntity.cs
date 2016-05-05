using Microsoft.Bot.Connector;
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

        public string ChannelId { get; set; }
        public string Address { get; set; }
        public string BotAddress { get; set; }

        public PomodoroTimerEntity()
        {
        }

        public PomodoroTimerEntity(PomodoroTimer source, ChannelAccount from, ChannelAccount bot)
        {
            Name = source.Name;
            Duration = source.Duration;
            ShortBreak = source.ShortBreak;
            LongBreakSpan = source.LongBreakSpan;
            LongBreak = source.LongBreak;
            RowKey = Guid.NewGuid().ToString().Replace("-", "");
            PartitionKey = from.Id;
            ChannelId = from.ChannelId;
            Address = from.Address;
            BotAddress = bot.Address;
        }
    }
}
