using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using PomodoroBot.Command;

namespace PomodoroBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public enum SandwichOptions
        {
            BLT, BlackForestHam, BuffaloChicken, ChickenAndBaconRanchMelt, ColdCutCombo, MeatballMarinara,
            OverRoastedChicken, RoastBeef, RotisserieStyleChicken, SpicyItalian, SteakAndCheese, SweetOnionTeriyaki, Tuna,
            TurkeyBreast, Veggie
        };
        public enum LengthOptions { SixInch, FootLong };
        public enum BreadOptions { NineGrainWheat, NineGrainHoneyOat, Italian, ItalianHerbsAndCheese, Flatbread };
        public enum CheeseOptions { American, MontereyCheddar, Pepperjack };
        public enum ToppingOptions
        {
            // This starts at 1 because 0 is the "no value" value
            //[Terms("except", "but", "not", "no", "all", "everything")]
            Everything = 1,
            Avocado, BananaPeppers, Cucumbers, GreenBellPeppers, Jalapenos,
            Lettuce, Olives, Pickles, RedOnion, Spinach, Tomatoes
        };
        public enum SauceOptions
        {
            ChipotleSouthwest, HoneyMustard, LightMayonnaise, RegularMayonnaise,
            Mustard, Oil, Pepper, Ranch, SweetOnion, Vinegar
        };
        [Serializable]
        [Template(TemplateUsage.EnumSelectOne, "What kind of {&} would you like on your sandwich? {||}", ChoiceStyle = ChoiceStyleOptions.PerLine)]
        internal class SandwichOrder
        {
            public SandwichOptions? Sandwich;
            public LengthOptions? Length;
            public BreadOptions? Bread;
            [Optional]
            public CheeseOptions? Cheese;
            public List<ToppingOptions> Toppings;
            public List<SauceOptions> Sauces;
            [Optional]
            [Template(TemplateUsage.NoPreference, "None")]
            public string Specials;

            public string DeliveryAddress;
            [Optional]
            public DateTime? DeliveryTime;
            [Numeric(1, 5)]
            [Optional]
            [Describe("your experience today")]
            public double? Rating;

            public static IForm<SandwichOrder> BuildForm()
            {
                OnCompletionAsyncDelegate<SandwichOrder> processOrder = async (context, state) =>
                {
                    await context.PostAsync("We are currently processing your sandwich. We will message you the status.");
                };
                return new FormBuilder<SandwichOrder>()
                            .Message("Welcome to the sandwich order bot!")
                            .Field(nameof(Sandwich))
                            .Field(nameof(Length))
                            .Field(nameof(Bread))
                            .Field(nameof(Cheese))
                            .Field(nameof(Toppings))
                            .Message("For sandwich toppings you have selected {Toppings}.")
                            .Field(nameof(SandwichOrder.Sauces))
                            .Field(new FieldReflector<SandwichOrder>(nameof(Specials))
                                .SetType(null)
                                .SetActive((state) => state.Length == LengthOptions.FootLong)
                                .SetDefine(async (state, field) =>
                                {
                                    field
                                    .AddDescription("cookie", "Free cookie")
                                    .AddTerms("cookie", "cookie", "free cookie")
                                    .AddDescription("drink", "Free large drink")
                                    .AddTerms("drink", "drink", "free drink");
                                    return true;
                                }))
                            .Confirm(async (state) =>
                            {
                                var cost = 0.0;
                                switch (state.Length)
                                {
                                    case LengthOptions.SixInch: cost = 5.0; break;
                                    case LengthOptions.FootLong: cost = 6.50; break;
                                }
                                return new PromptAttribute($"Total for your sandwich is ${cost:F2} is that ok?");
                            })
                            .Field(nameof(SandwichOrder.DeliveryAddress),
                                validate: async (state, response) =>
                                {
                                    var result = new ValidateResult { IsValid = true };
                                    var address = (response as string).Trim();
                                    if (address.Length > 0 && address[0] < '0' || address[0] > '9')
                                    {
                                        result.Feedback = "Address must start with a number.";
                                        result.IsValid = false;
                                    }
                                    return result;
                                })
                            .Field(nameof(SandwichOrder.DeliveryTime), "What time do you want your sandwich delivered? {||}")
                            .Confirm("Do you want to order your {Length} {Sandwich} on {Bread} {&Bread} with {[{Cheese} {Toppings} {Sauces}]} to be sent to {DeliveryAddress} {?at {DeliveryTime:t}}?")
                            .AddRemainingFields()
                            .Message("Thanks for ordering a sandwich!")
                            .OnCompletionAsync(processOrder)
                            .Build();
            }
        };

        internal static IDialog<SandwichOrder> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(SandwichOrder.BuildForm));
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                var command = Commands.GetItems()
                    .FirstOrDefault(q => q.DoHandle(message));
                if (command != null)
                    return await command.Reply(message);
                return await Conversation.SendAsync(message, MakeRootDialog);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}