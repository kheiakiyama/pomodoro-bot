using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using PomodoroBot.Models;
using PomodoroBot.Command;
using Microsoft.Bot.Connector;

namespace PomodoroTimerJob
{
    public class Functions
    {
        public static async Task PomodoroTimer([QueueTrigger(PomodoroNotification.QueueName)] PomodoroNotification info)
        {
            var entity = await CommandTool.Instance.Repository.Find(info.Key);
            entity.StartTimer(async (message) => {
                System.Diagnostics.Trace.WriteLine(message);
                var msg = CommandTool.Instance.Request.CreateMessage(entity, message);
                await CommandTool.Instance.Client.Messages.SendMessageAsync(msg);
            });
        }
    }
}
