using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PomodoroBot.Models
{
    public class HerokuDeployHookPayload
    {
        public string app { get; set; }
        public string user { get; set; }
        public string url { get; set; }
        public string head { get; set; }
        public string head_long { get; set; }
        public string git_log { get; set; }
        public string release { get; set; }
    }
}