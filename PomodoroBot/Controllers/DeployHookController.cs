using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using PomodoroBot.Command;
using PomodoroBot.Models;

namespace PomodoroBot
{
    public class DeployHookController : ApiController
    {
        /// <summary>
        /// POST: api/DeployHook
        /// </summary>
        public async Task<string> Post([FromBody]HerokuDeployHookPayload payload)
        {
            var records = await CommandTool.Instance.Repository.ListIsRunning();
            foreach(var item in records)
            {
                var response = CommandTool.Instance.Request.CreateReplyMessage(item, "this service was deployed.\n\nPlease restart.");
                await CommandTool.Instance.Client.Messages.SendMessageAsync(response);
            }
            return "done";
        }
    }
}