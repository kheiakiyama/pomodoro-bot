using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PomodoroBot.Models
{
    public class PomodoroTimerBackend
    {
        public PomodoroTimerBackend()
        {
            m_Host = new Uri(Settings.Get("backendhost"));
            m_ApiKey = Settings.Get("backendapikey");
        }

        private Uri m_Host;
        private string m_ApiKey;

        public async Task<bool> Start(PomodoroTimerEntity entity)
        {
            var data = new NameValueCollection();
            data.Add("id", entity.RowKey);
            data.Add("channelId", entity.ChannelId);
            data.Add("from", entity.BotAddress);
            data.Add("to", entity.Address);
            data.Add("language", "ja");
            data.Add("duration", entity.Duration.ToString());
            data.Add("shortbreak", entity.ShortBreak.ToString());
            data.Add("longbreak", entity.LongBreak.ToString());
            data.Add("longbreakspan", entity.LongBreakSpan.ToString());
            return await Request("", data);
        }

        public async Task<bool> Stop(string id)
        {
            var data = new NameValueCollection();
            data.Add("id", id);
            return await Request("stop", data);
        }

        private async Task<bool> Request(string relativeUri, NameValueCollection data)
        {
            using (WebClient client = new WebClient())
            {
                data.Add("apikey", m_ApiKey);
                try
                {
                    byte[] response = await client.UploadValuesTaskAsync(new Uri(m_Host, relativeUri), data);
                    Trace.WriteLine(Encoding.UTF8.GetString(response));
                    return true;
                }
                catch(WebException e)
                {
                    Trace.WriteLine($"message:{e.Message} code:{e.Status.ToString()} stacktrace:{e.StackTrace}");
                    return false;
                }
            }
        }
    }
}