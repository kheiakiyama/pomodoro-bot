using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using PomodoroBot.Models;

namespace PomodoroBot.Command
{
    public class CreateCommand : ICommand
    {
        public bool DoHandle(Message message)
        {
            return message.Text.ToLower() == "create" ||
                   message.GetBotPerUserInConversationData<bool>(CreateTag);
        }

        private static IForm<PomodoroTimer> BuildForm()
        {
            return new FormBuilder<PomodoroTimer>()
                    .Message("set your pomodoro timer.")
                    .OnCompletionAsync(async (context, state) => {
                        var entity = new PomodoroTimerEntity(state) { PartitionKey = context.UserData.Get<string>(CommandTool.UserIdKey) };
                        await CommandTool.Instance.Repository.Add(entity);
                        context.PerUserInConversationData.SetValue<bool>(CreateTag, false);
                        await context.PostAsync("timer is created.");
                        entity.StartTimer(async (message) => {
                            System.Diagnostics.Trace.WriteLine(message);
                            //await context.PostAsync(message);
                        });
                    })
                    .Build();
        }

        private static readonly string CreateTag = "create";

        public static IDialog<PomodoroTimer> MakeDialog()
        {
            return Chain.From(() => FormDialog.FromForm(BuildForm))
                .Do(async (context, task) =>
                {
                    try
                    {
                        await task;
                    }
                    catch (FormCanceledException<PomodoroTimer>)
                    {
                        context.PerUserInConversationData.SetValue<bool>(CreateTag, false);
                        await context.PostAsync("create command is canceled");
                    }
                });
        }

        public async Task<Message> Reply(Message message)
        {
            message.SetBotPerUserInConversationData(CreateTag, true);
            return await Conversation.SendAsync(message, MakeDialog);
        }
    }
}
